using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject MainUI;
    public Text scoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    

    public void SetScoreText(string result)
    {
        scoreText.text = " 현재 남은 타일의 개수 : " + result;
    }
    
    public void ClearText()
    {
        scoreText.text = " 클리어 하였습니다!!! ";
    }
}
