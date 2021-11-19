using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile : MonoBehaviour
{

    #region tile Data
    [SerializeField] private TileData tileData;
    Material originMat;
    Material answerMat;
    public int answerValue; // 비교할 값
    public int value; // 현재 값

    #endregion

    private MeshRenderer meshRenderer;
    bool isChanged;
    public bool IsChanged => isChanged;
    public Action OnChangeMat;
    TileManager tileManager;




    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        tileManager = GetComponentInParent<TileManager>();
        isChanged = false;

        OnChangeMat += tileManager.ProcessCheck;
        Init();
        meshRenderer.material = originMat;
    }

    public void Init()
    {
        originMat = tileData.originMat;
        answerMat = tileData.answerMat;
        answerValue = tileData.answerValue;
    }


    public void ChangeColor(Material cubeMat)
    {       
        meshRenderer.material = cubeMat; // 플레이어 큐브의 색상으로 메테리얼 변경
      
        OnChangeMat();
    }
}
