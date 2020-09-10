// 引用URL: https://uni.gas.mixh.jp/unity/ball_run.html
// 引用URL: https://qiita.com/zeffy1014/items/6c677f80b0b95ded5e21

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigitbody;
    public float power = 20.0f, gravity = 0.25f, forceThreshold = 0.05f, drag = 0.15f;
    private float angle;

    public Image gravityIcon;
    
    // Start is called before the first frame update
    void Start()
    {
        rigitbody = GetComponent<Rigidbody2D>();
        angle = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsMobile()) Debug.Log("Mobile Connected!");
    }

    private void FixedUpdate()
    {
        Vector2 dir = Vector2.zero;

        // ターゲット端末の縦横の表示に合わせてremapする
        dir.x = Input.acceleration.x;
        dir.y = Input.acceleration.y;

        // 加速度が操作するには十分に大きい場合は重力方向を再計算、それ以外は重力方向を下向きに
        if (dir.sqrMagnitude > forceThreshold)
        {
            // 正規化
            dir.Normalize();
            // 加速度方向と下向きの座標単位ベクトルとの角変位を出して重力方向を再計算
            angle = Mathf.Deg2Rad * Vector2.SignedAngle(new Vector2(0.0f, -1.0f), dir);
        }
        else angle = 0.0f;
        // 重力方向をUIで表示
        gravityIcon.rectTransform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f + Mathf.Rad2Deg * angle);

        // 重力ベクトルを算出
        Vector2 addGravity = new Vector2(Mathf.Sin(angle), -Mathf.Cos(angle)).normalized * gravity;
        // 重力ベクトルに垂直な単位ベクトルと速度の単位ベクトルから抵抗ベクトルを算出
        Vector2 addDrag = new Vector2(-Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
        float dot = Vector2.Dot(addDrag, rigitbody.velocity.normalized); // 内積で印加方向と印加率を計算
        addDrag *= dot * drag;
        // 重力と抵抗を合算（この際ベクトルdirは印加用変数として再利用）
        dir = addGravity - addDrag;

        // 加速
        rigitbody.AddForce(dir * power, ForceMode2D.Force);
    }

    static readonly bool isAndroid = Application.platform == RuntimePlatform.Android;
    static readonly bool isIOS = Application.platform == RuntimePlatform.IPhonePlayer;

    public static bool IsMobile()
    {
        // AndroidかiOSか、あるいはUnity RemoteだったらMobile扱いとする
#if UNITY_EDITOR
        bool ret = UnityEditor.EditorApplication.isRemoteConnected;
#else
        bool ret = isAndroid || isIOS;
#endif
        return ret;
    }
}
