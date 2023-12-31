using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

public class GenerateGrid : MonoBehaviour
{
    [Range(1,10)]
    public int pairCount;

    public List<Sprite> tileImages = new List<Sprite>();

    private List<int> _selectedTiles = new List<int>();
    private List<TileScript> _selectedTileScripts = new List<TileScript>();

    private int _pairsCompleted = 0;
    
    private int _playerTurn = 1;
    private List<int> _playerScores = new List<int>{0,0};
    public List<TMP_Text> playerScoreText;
    public List<Image> playerScoreBorder;

    public GameObject tilePrefab;

    private void Start()
    {
        GenerateTiles();
        ShuffleTiles();
        foreach (TMP_Text toSet in playerScoreText)
        {
            toSet.SetText("0");
        }
        ChangePlayerTurn();
    }
    
    //Function that generates tiles based on pair count, 2x since pair for each
    private void GenerateTiles()
    {
        for (int c = 0; c < 2; c++)
        {
            for (int i=1; i < pairCount+1; i++)
            {
                //Spawns new tile from prefab
                GameObject newTile = Instantiate(tilePrefab);

                //Check if tile spawn right otherwise show error
                if (newTile != null)
                {
                    //Attaches new tile to grid object
                    newTile.transform.SetParent(this.transform);
                    
                    //Sets Tile ID in it's script
                    newTile.GetComponent<TileScript>().SetTileID(i);
                }
                else
                {
                    Debug.Log("Issue with tile spawn");
                }

            }
        }
    }

    //Function that receives tileID from TileScript and checks for match
    public void CheckFlippedTile(int inID, TileScript inScript)
    {
        _selectedTiles.Add(inID);
        _selectedTileScripts.Add(inScript);

        if (_selectedTiles.Count == 2)
        {
            if (_selectedTiles[0] == _selectedTiles[1])
            {
                foreach (TileScript tS in _selectedTileScripts)
                {
                    tS.CompleteTile();
                }
                UpdatePlayerScores(3);
                
                //Increments pairs complete and checks if victory
                _pairsCompleted++;
                if (_pairsCompleted == pairCount)
                {
                    GetComponentInParent<UIScript>().GameOver(true);
                }
            }
            else
            {
                foreach (var tS in _selectedTileScripts)
                {
                    StartCoroutine(WaitForReset(tS));
                }
                UpdatePlayerScores(-2);
            }
            _selectedTiles.Clear();
            _selectedTileScripts.Clear();
            
            ChangePlayerTurn();
        }
    }

    private void ChangePlayerTurn()
    {
        playerScoreBorder[_playerTurn].color = (Color.white);
        //Changes player turn on move commit
        if (_playerTurn == 0)
        {
            _playerTurn = 1;
        }
        else
        {
            _playerTurn = 0;
        }
        playerScoreBorder[_playerTurn].color = (Color.yellow);
    }
    private void UpdatePlayerScores(int toAdd)
    {
        _playerScores[_playerTurn] +=toAdd;
        playerScoreText[_playerTurn].SetText(_playerScores[_playerTurn].ToString());
    }

    IEnumerator WaitForReset(TileScript toReset)
    {
        yield return new WaitForSeconds(.25f);
        toReset.ResetTile();
    }

    //Function that removes tile from list, currently set to just clear since it's only pair matching
    // If matching 3 of a kind then a different approach is needed
    public void RemoveTile()
    {
        _selectedTiles.Clear();
        _selectedTileScripts.Clear();
    }
    
    //Function for shuffling all the tiles around
    private void ShuffleTiles()
    {
        Random r = new Random();
        foreach (Transform child in this.transform)
        {
            int newIndex = r.Next(0, pairCount);
            child.SetSiblingIndex(newIndex);
        }
    }
}