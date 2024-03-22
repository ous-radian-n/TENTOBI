using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualKeyboard : MonoBehaviour
{
    #region Prefab用変数定義
    [Header("テキストのデフォルト表示(Text)")]
    /// <summary>
    /// 文字列が入力されていない時に表示するTextコンポーネントです
    /// </summary>
    public Text placeDefault;
    [Header("入力内容表示用のText")]
    /// <summary>
    /// 入力された文字列を表示するTextコンポーネントです
    /// </summary>
    public Text input;

    /// <summary>
    /// 入力された文字列
    /// </summary>
    private string text;

    [Header("入力文字列の最長長さ")]
    /// <summary>
    /// 入力できる文字列の最長の文字数です
    /// </summary>
    public int maxLength = 64;
    [Header("入力可能かどうか")]
    /// <summary>
    /// 入力可能な時にtrueになります
    /// </summary>
    public bool isActive = true;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        input.text = text = "";
        input.gameObject.SetActive(false);
        placeDefault.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (text.Length > 0)
        {
            placeDefault.gameObject.SetActive(false);
            input.gameObject.SetActive(true);
            input.text = text;
        }
        else
        {
            input.text = text = "";
            input.gameObject.SetActive(false);
            placeDefault.gameObject.SetActive(true);
        }
    }

    #region ボタンの処理
    public void OnClickBS()
    {
        if(isActive && text.Length > 0) text = text.Substring(0, text.Length - 1);
    }

    public void OnClick01()
    {
        if(isActive && text.Length < maxLength) text += "A";
    }

    public void OnClick02()
    {
        if (isActive && text.Length < maxLength) text += "B";
    }

    public void OnClick03()
    {
        if (isActive && text.Length < maxLength) text += "C";
    }

    public void OnClick04()
    {
        if (isActive && text.Length < maxLength) text += "D";
    }

    public void OnClick05()
    {
        if (isActive && text.Length < maxLength) text += "E";
    }

    public void OnClick06()
    {
        if (isActive && text.Length < maxLength) text += "F";
    }

    public void OnClick07()
    {
        if (isActive && text.Length < maxLength) text += "G";
    }

    public void OnClick08()
    {
        if (isActive && text.Length < maxLength) text += "H";
    }

    public void OnClick09()
    {
        if (isActive && text.Length < maxLength) text += "I";
    }

    public void OnClick10()
    {
        if (isActive && text.Length < maxLength) text += "J";
    }

    public void OnClick11()
    {
        if (isActive && text.Length < maxLength) text += "K";
    }

    public void OnClick12()
    {
        if (isActive && text.Length < maxLength) text += "L";
    }

    public void OnClick13()
    {
        if (isActive && text.Length < maxLength) text += "M";
    }

    public void OnClick14()
    {
        if (isActive && text.Length < maxLength) text += "N";
    }

    public void OnClick15()
    {
        if (isActive && text.Length < maxLength) text += "O";
    }

    public void OnClick16()
    {
        if (isActive && text.Length < maxLength) text += "P";
    }

    public void OnClick17()
    {
        if (isActive && text.Length < maxLength) text += "Q";
    }

    public void OnClick18()
    {
        if (isActive && text.Length < maxLength) text += "R";
    }

    public void OnClick19()
    {
        if (isActive && text.Length < maxLength) text += "S";
    }

    public void OnClick20()
    {
        if (isActive && text.Length < maxLength) text += "T";
    }

    public void OnClick21()
    {
        if (isActive && text.Length < maxLength) text += "U";
    }

    public void OnClick22()
    {
        if (isActive && text.Length < maxLength) text += "V";
    }

    public void OnClick23()
    {
        if (isActive && text.Length < maxLength) text += "W";
    }

    public void OnClick24()
    {
        if (isActive && text.Length < maxLength) text += "X";
    }

    public void OnClick25()
    {
        if (isActive && text.Length < maxLength) text += "Y";
    }

    public void OnClick26()
    {
        if (isActive && text.Length < maxLength) text += "Z";
    }

    public void OnClick27()
    {
        if (isActive && text.Length < maxLength) text += "_";
    }

    public void OnClick28()
    {
        if (isActive && text.Length < maxLength) text += "0";
    }

    public void OnClick29()
    {
        if (isActive && text.Length < maxLength) text += "1";
    }

    public void OnClick30()
    {
        if (isActive && text.Length < maxLength) text += "2";
    }

    public void OnClick31()
    {
        if (isActive && text.Length < maxLength) text += "3";
    }

    public void OnClick32()
    {
        if (isActive && text.Length < maxLength) text += "4";
    }

    public void OnClick33()
    {
        if (isActive && text.Length < maxLength) text += "5";
    }

    public void OnClick34()
    {
        if (isActive && text.Length < maxLength) text += "6";
    }

    public void OnClick35()
    {
        if (isActive && text.Length < maxLength) text += "7";
    }

    public void OnClick36()
    {
        if (isActive && text.Length < maxLength) text += "8";
    }

    public void OnClick37()
    {
        if (isActive && text.Length < maxLength) text += "9";
    }
    #endregion ここまで
}
