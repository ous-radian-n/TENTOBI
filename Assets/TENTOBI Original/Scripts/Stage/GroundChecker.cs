using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    #region private変数定義
    /// <summary>
    /// GameDirectorコンポーネント
    /// </summary>
    private GameDirector game;

    /// <summary>
    /// タイルマップ識別用タグ名
    /// </summary>
    private string groundTag = "Ground", killTrapTag = "Kill Trap", damageTrapTag = "Damage Trap";

    /// <summary>
    /// 接地判定用フラグ(PlayerDirectorとほぼ同様)
    /// </summary>
    private bool isGround, isDamaged;
    /// <summary>
    /// 接地判定の更新用フラグ
    /// Enter:着陸したとき, Stay:接地し続けているとき, Exit:離陸したとき
    /// </summary>
    private bool isGroundEnter, isGroundStay, isGroundExit;
    /// <summary>
    /// トラップ(ダメージエリア)との当たり判定の更新用フラグ
    /// Enter:触れ始めたとき, Stay:接し続けているとき, Exit:離れたとき
    /// </summary>
    private bool isDamageTrapEnter, isDamageTrapStay, isDamageTrapExit;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        isGround = false;
        isDamaged = false;
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameDirector>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 地形(Ground)と接地しているかを返します
    /// </summary>
    public bool IsGround()
    {
        if (isGroundEnter || isGroundStay) isGround = true;
        else if (isGroundExit) isGround = false;

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        return isGround;
    }

    /// <summary>
    /// ダメージエリア(DamageTrap)と接地しているかを返します
    /// </summary>
    public bool IsDamaged()
    {
        if (isDamageTrapEnter || isDamageTrapStay) isDamaged = true;
        else if (isDamageTrapExit) isDamaged = false;

        isDamageTrapEnter = false;
        isDamageTrapStay = false;
        isDamageTrapExit = false;
        return isDamaged;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == killTrapTag)
        {
            Debug.Log("即死ダメージが入りました");
            game.DeathPlayerAtKilled();
        }
        if (collision.tag == damageTrapTag)
        {
            isDamageTrapEnter = true;
        }
        if (collision.tag == groundTag)
        {
            isGroundEnter = true;
            Debug.Log("着地しました");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == damageTrapTag)
        {
            if (game.HP > 0.0f) Debug.Log("継続ダメージが入っています(耐久中)");
            else Debug.Log("継続ダメージが入っています(致死量)");
            isDamageTrapStay = true;
        }
        if (collision.tag == groundTag)
        {
            isGroundStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == damageTrapTag)
        {
            isDamageTrapExit = true;
        }
        if (collision.tag == groundTag)
        {
            isGroundExit = true;
            Debug.Log("離陸しました");
        }
    }
}