using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hopper : MonoBehaviour
{
    #region private変数定義
    /// <summary>
    /// GameDirectorコンポーネント
    /// </summary>
    private GameDirector game;

    /// <summary>
    /// Z回転位置
    /// 反発力の印加方向を計算する時に使用します
    /// </summary>
    private float zAngle;
    #endregion

    [Header("反発力")]
    /// <summary>
    /// このホッパーに触れたときプレイヤーに印加される反発力の大きさです
    /// </summary>
    public float addPower = 35.0f;


    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameDirector>();
        zAngle = this.gameObject.transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //＊何らかのアニメーション

            Rigidbody2D rigitbody = collision.GetComponent<Rigidbody2D>();

            // 反発力の印加（反発力(Vector2)はCalcurateForceAndAngle関数で計算される）
            rigitbody.AddForce(CalcurateForceAndAngle(), ForceMode2D.Impulse);

            Debug.Log("ホッパーで加速(Z回転角:"+zAngle.ToString() +"度, 付与した力:+" + addPower.ToString() + ")しました");
        }
    }

    #region 反発力計算
    /// <summary>
    /// このホッパーが持つ反発力のベクトルを返します
    /// </summary>
    /// <returns>印加される反発力</returns>
    private Vector2 CalcurateForceAndAngle()
    {
        return addPower * new Vector2(-Mathf.Sin(Mathf.Deg2Rad * zAngle), Mathf.Cos(Mathf.Deg2Rad * zAngle));
    }
    #endregion
}
