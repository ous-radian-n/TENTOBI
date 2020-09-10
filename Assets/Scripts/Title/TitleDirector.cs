using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleDirector : MonoBehaviour
{
    private bool isPlessed;
    public Text versionText;

    // Start is called before the first frame update
    void Start()
    {
        versionText.text = "version: " + Application.version;
        isPlessed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ニューゲームボタンを押した
    public void PlessNewGameButton()
    {
        if (!isPlessed)
        {
            // ニューゲーム開始処理

            isPlessed = true;
        }
    }

    // コンティニューボタンを押した
    public void PlessContinueButton()
    {
        if (!isPlessed)
        {
            // コンティニュー処理

            isPlessed = true;
        }
    }

    // 終了ボタンを押した
    public void PlessExitButton()
    {
        if (!isPlessed)
        {
            // ゲーム終了処理
            Application.Quit();

            isPlessed = true;
        }
    }
}
