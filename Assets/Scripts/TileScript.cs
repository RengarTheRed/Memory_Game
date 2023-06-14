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
    private static readonly int Color1 = Shader.PropertyToID("_Color");

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
        SetText(" ");
    }

    private void FlipTile()
    {
        if (_bIsFlipped)
        {
            ResetTile();
            this.GetComponentInParent<GenerateGrid>().RemoveTile();
        }
        else
        {
            SetText(_tileID.ToString());
            _bIsFlipped = true;
            this.GetComponentInParent<GenerateGrid>().CheckFlippedTile(_tileID, this);
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
        //this.gameObject.GetComponent<Image>().material.SetColor(Color1, Color.blue);
    }

    private void SetText(string toSet)
    {
        this.GetComponentInChildren<TMP_Text>().SetText(toSet);
    }
}
