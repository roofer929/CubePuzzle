using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///<Summary>
/// 플레이어가 색상을 바꿀수 있도록 해주는 타일
///</Summary>
public class ColorChanger : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Material answerMat;


    #region tile Data
    [SerializeField] private TileData tileData;
    public Material originMat;
    int answerValue; // 정답을 비교할 인덱스    

    #endregion

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        Init();

        meshRenderer.material = answerMat;  //현재 보이는 색상과 동일한 색상으로 변경

    }
    public void Init()
    {
        originMat = tileData.originMat;
        answerMat = tileData.answerMat;
        answerValue = tileData.answerValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponentInParent<Player>();
            player.ChangePlayerMat(answerMat, answerValue);
        }
    }
}
