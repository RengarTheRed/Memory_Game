using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Script is on each tile and tileID is set via GenerateGrid
public class TileScript : MonoBehaviour
{
    private int _tileID;
    private bool _bIsFlipped = false;
    private GenerateGrid _parentGrid;

    #region Setup via GenerateGrid
    public void SetTileID(int inID)
    {
        _tileID = inID;
    }

    public int GetTileID()
    {
        return _tileID;
    }
    #endregion'
    
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(FlipTile);
        StartCoroutine(LateStart());
    }

    //Function used to get parent object script since ref'd multiple times
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(.001f);
        _parentGrid = this.GetComponentInParent<GenerateGrid>();
        SetText(" ");
    }

    //Function for flipping tile
    private void FlipTile()
    {
        if (_bIsFlipped)
        {
            ResetTile();
            _parentGrid.RemoveTile();
        }
        else
        {
            SetText(_tileID.ToString());
            _bIsFlipped = true;
            _parentGrid.CheckFlippedTile(_tileID, this);
        }
    }

    public void ResetTile()
    {
        SetText(" ");
        _bIsFlipped = false;
    }

    public void CompleteTile()
    {
        this.GetComponent<Button>().onClick.RemoveAllListeners();
        // This changes all prefabs color instead of just instance
        this.gameObject.GetComponent<Image>().color = (Color.green);
    }
    
    private void SetText(string toSet)
    {
        this.GetComponentInChildren<TMP_Text>().SetText(toSet);
    }
}
