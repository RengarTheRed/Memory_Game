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
    private static readonly int Color1 = Shader.PropertyToID("_Color");

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

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(.001f);
        _parentGrid = this.GetComponentInParent<GenerateGrid>();
        SetText(" ");
    }

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
        //SetImage();
    }

    /*Function that updates image to reflect if flipped or not
    private void SetImage()
    {
        if (_parentGrid)
        {
            if (_bIsFlipped)
            {
                this.GetComponentInChildren<Image>().sprite = _parentGrid._tileImages[0];
            }
            else
            {
                this.GetComponentInChildren<Image>().sprite = _parentGrid._tileImages[_tileID];
            }
        }
    }
    */
}
