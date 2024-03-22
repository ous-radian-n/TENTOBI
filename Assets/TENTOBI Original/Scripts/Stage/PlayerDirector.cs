// 引用URL: https://dkrevel.com/makegame-beginner/make-2d-action-ground/ (2020.09.12)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirector : MonoBehaviour
{
    #region private変数定義
    /// <summary>
    /// GroundChecker(接地判定)クラス
    /// </summary>
    private GroundChecker groundChecker;
    #endregion

    [Header("地面に接地しているか")]
    /// <summary>
    /// 地面に接地しているときtrueになります
    /// </summary>
    public bool isGround;

    [Header("ダメージエリアに接地しているか")]
    /// <summary>
    /// ダメージエリアに接地しているときtrueになります
    /// </summary>
    public bool isDamaged;

    [Header("即死した(HPが尽きた)か")]
    /// <summary>
    /// 即死した（またはHPが尽きた）ときtrueになります
    /// </summary>
    public bool isKilled;

    // Start is called before the first frame update
    void Start()
    {
        groundChecker = GameObject.FindGameObjectWithTag("GroundChecker").GetComponent<GroundChecker>();

        isGround = false;
        isDamaged = false;
        isKilled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    /// <summary>
    /// GroundCheckerコンポーネントから接地判定をします
    /// </summary>
    private void CheckGround()
    {
        isGround = groundChecker.IsGround();
        isDamaged = groundChecker.IsDamaged();
    }
}
