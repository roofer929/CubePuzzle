using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TileData", menuName = "Scriptable Object/TileData", order = int.MaxValue)]
public class TileData : ScriptableObject
{
    public Material originMat;  // 비교할때 쓰는 메테리얼
    public Material answerMat; // 비교당할때 쓰는 메테리얼
    public int answerValue; // 정답을 비교할 인덱스    

}
