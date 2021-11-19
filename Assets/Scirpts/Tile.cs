using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile : MonoBehaviour
{
    public TileData tileData = new TileData();
    MeshRenderer meshRenderer;

    bool isChanged;
    public bool IsChanged => isChanged;

    public Action OnChangeColor;
    TileManager tileManager;


    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        tileManager = GetComponentInParent<TileManager>();
        isChanged = false;
        tileData.originColor = meshRenderer.material.color;

        OnChangeColor += tileManager.ProcessCheck;
    }


    public void ChangeColor()
    {
        isChanged = !isChanged;

        if (isChanged)
        {
            meshRenderer.material.color = tileData.changeColor;
        }
        else
        {
            meshRenderer.material.color = tileData.originColor;
        }

        OnChangeColor();
    }
}
