using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<Tile> tileArray = new List<Tile>();

    private void Start()
    {
        UIManager.instance.SetScoreText((tileArray.Count).ToString());
    }


    ///<Summary>
    /// 진행도 체크()
    ///</Summary>
    public void ProcessCheck()
    {
        int i = 0;
        foreach (var tile in tileArray)
        {
            if (tile.tileData.answerColor == tile.meshRenderer.material.color)
            {
                i++;
            }
        }

        if (i >= tileArray.Count)
        {
            UIManager.instance.ClearText();
        }
        else
        {
            UIManager.instance.SetScoreText((tileArray.Count - i).ToString());
        }
    }


}
