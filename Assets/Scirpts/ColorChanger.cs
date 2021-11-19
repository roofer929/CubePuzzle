using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Color changeColor;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        changeColor = meshRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            var player = other.GetComponentInParent<Player>();
            player.ChangePlayerColor(changeColor);
        }
    }


}
