using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infoboard : MonoBehaviour
{
    #region private変数定義
    /// <summary>
    /// UIDirectorコンポーネント
    /// </summary>
    private UIDirector ui;

    /// <summary>
    /// アクセスされたかどうか
    /// trueで再度表示されないようにします
    /// </summary>
    private bool isAccessed;
    #endregion

    [Header("表示するインフォメッセージ")]
    /// <summary>
    /// この看板に触れたときに表示するメッセージ
    /// </summary>
    public string text; // 

    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIDirector>();
        isAccessed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isAccessed)
        {
            ui.ActivateInfoText(text);
            isAccessed = true;
        }
    }
}
