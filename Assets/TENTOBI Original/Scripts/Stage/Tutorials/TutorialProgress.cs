using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialProgress : MonoBehaviour
{
    #region private変数定義
    /// <summary>
    /// GameDirectorコンポーネント
    /// </summary>
    private GameDirector game;

    /// <summary>
    /// UIDirectorコンポーネント
    /// </summary>
    private UIDirector ui;
    #endregion

    [Header("獲得時の追加進捗")]
    /// <summary>
    /// このオブジェクト獲得時に得られる進捗です[単位:%]
    /// </summary>
    public int progress;

    [Header("獲得時のインフォメッセージ")]
    /// <summary>
    /// このオブジェクト獲得時に表示するインフォメッセージです
    /// 未入力の場合、獲得時にインフォメッセージを表示しません
    /// </summary>
    public string info;
    
    [Header("インフォメッセージ表示のみかどうか")]
    /// <summary>
    /// このオブジェクトの獲得を問わずインフォメッセージを表示するためだけのものであればtrueを指定ください
    /// trueの場合、このコンポーネントが呼び出された（isAccessed = true）時に当たり判定を問わずインフォメッセージを表示します
    /// falseの場合にこのコンポーネントが呼び出された（isAccessed = true）時は、プレイヤーが接触するまで処理を実行しません
    /// </summary>
    public bool isInfoMessageOnly = false;

    [Header("次の中継ポイントとして呼び出されたか")]
    /// <summary>
    /// trueになると、インフォメッセージの表示または進捗の更新などの処理を実行します
    /// falseである時は、trueになるまで処理を実行しません
    /// </summary>
    public bool isAccessed = false;

    [Header("処理の実行後、次の中継ポイントとして呼び出される中継ポイント")]
    /// <summary>
    /// このオブジェクトの獲得などによりこのコンポーネントの処理が実行された場合に呼び出す、次のオブジェクトのコンポーネントです
    /// このオブジェクトの次の中継ポイントで、かつこのコンポーネントを有しているPrefabを参照してください
    /// </summary>
    public TutorialProgress next;

    [Header("処理の実行後に出現するオブジェクト")]
    /// <summary>
    /// このオブジェクトの獲得などによりこのコンポーネントの処理が実行された場合にアクティブ化するオブジェクトです
    /// </summary>
    public GameObject[] activateObject;
    [Header("処理の実行後に消失するオブジェクト")]
    /// <summary>
    /// このオブジェクトの獲得などによりこのコンポーネントの処理が実行された場合にディアクティブ化するオブジェクトです
    /// </summary>
    public GameObject[] deactivateObject;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameDirector>();
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIDirector>();
        if (!isAccessed) this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInfoMessageOnly && isAccessed && game.IsSwitchingNextMessage())
        {
            if (info != "") ui.ActivateInfoText(info);
            StepNextMessage();

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAccessed && collision.tag == "Player" && !isInfoMessageOnly)
        {
            Debug.Log("チュートリアル進捗(+" + progress.ToString() + "[%])を獲得しました");
            game.scoreInStage += progress;
            if (info != "") ui.ActivateInfoText(info);
            StepNextMessage();

            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 次のインフォメッセージへ進みます
    /// </summary>
    private void StepNextMessage()
    {
        if (next != null)
        {
            if (next.isInfoMessageOnly)
            {
                game.SwitchMessageContinuedFlag(true);
            }
            else
            {
                game.SwitchMessageContinuedFlag(false);
            }
            next.gameObject.SetActive(true);
            next.isAccessed = true;
        }
        else
        {
            game.SwitchMessageContinuedFlag(false);
        }

        // アクティブ化するオブジェクトがあればアクティブ化する
        int obj;
        for (obj = 0; obj < activateObject.Length && activateObject.Length > 0; ++obj)
        {
            activateObject[obj].SetActive(true);
        }
        // ディアクティブ化するオブジェクトがあればディアクティブ化する
        for (obj = 0; obj < deactivateObject.Length && deactivateObject.Length > 0; ++obj)
        {
            deactivateObject[obj].SetActive(false);
        }
    }
}
