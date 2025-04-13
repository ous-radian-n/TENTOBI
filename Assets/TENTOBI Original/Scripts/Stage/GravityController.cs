// 引用URL: https://uni.gas.mixh.jp/unity/ball_run.html (2020.09.06)
// 引用URL: https://qiita.com/zeffy1014/items/6c677f80b0b95ded5e21 (2020.09.07)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
	#region private変数定義 1
	/// <summary>
	/// GameDirectorコンポーネント
	/// </summary>
	private GameDirector game;

	private Rigidbody2D rigitbody;
	#endregion

	#region 重力計算用変数
	[Header("<重力計算用定数>")]
	[Header("power:全体強度, gravity:重力係数, forceThrethold:力の閾値, drag:摩擦係数")]
	/// <summary>
	/// 重力計算のための定数
	/// power:全体強度
	/// </summary>
	public float power = 20.0f;
	/// <summary>
	/// 重力計算のための定数
	/// gravity:重力係数
	/// </summary>
	public float gravity = 0.25f;
	/// <summary>
	/// 重力計算のための定数
	/// forceThrethold:力の閾値
	/// </summary>
	public float forceThreshold = 0.05f;
	/// <summary>
	/// 重力計算のための定数
	/// drag:摩擦係数
	/// </summary>
	public float drag = 0.15f;

	[Header("angle:角度位置(ポーズ直前), angularVelocity:角速度(ポーズ直前)")]
	/// <summary>
	/// 角度計算のための変数
	/// angle:角度位置
	/// </summary>
	public float angle;
	/// <summary>
	/// 角度計算のための変数
	/// angularVelocity:角速度
	/// </summary>
	public float angularVelocity;

	[Header("velocity:速度ベクトル(ポーズ直前)")]
	/// <summary>
	/// 位置計算のための速度ベクトル
	/// </summary>
	public Vector2 velocity;
	#endregion

	#region private変数定義 2
	/// <summary>
	/// ポーズ中かどうか
	/// </summary>
	private bool isPausing;
	#endregion

	// Start is called before the first frame update
	void Start()
	{
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameDirector>();
		rigitbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
		angle = angularVelocity = 0.0f; velocity = Vector2.zero;

		isPausing = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (IsMobile()) Debug.Log("現在スマートフォンで稼働しています");
	}

	private void FixedUpdate()
	{
		// 加速
		if (!game.ReturnPauseFlag())
		{
			// ポーズ中だった（ポーズを終了する）場合
			if (isPausing)
			{
				// 時間の進行を再開する（＝別変数で確保された速度をこのオブジェクトの速度に再付加する）
				rigitbody.linearVelocity = this.velocity;
				rigitbody.angularVelocity = this.angularVelocity;

				isPausing = false;
			}
			// 重力を印可（重力(Vector2)はCalcurateForceAndAngle関数で計算される）
			rigitbody.AddForce(CalcurateForceAndAngle(), ForceMode2D.Force);
		}
		// ポーズを開始した場合
		else if (!isPausing)
		{
			// いったん時間を止める（＝このオブジェクトの速度を別変数で確保し0にする）
			this.velocity = rigitbody.linearVelocity;
			this.angularVelocity = rigitbody.angularVelocity;
			rigitbody.linearVelocity = Vector2.zero;
			rigitbody.angularVelocity = 0.0f;

			isPausing = true;
		}
	}

	#region 重力計算
	/// <summary>
	/// 重力方向を更新し、重力と抵抗の計算結果から最終的に印加する力を返します
	/// </summary>
	/// <returns>最終的に印加する力</returns>
	private Vector2 CalcurateForceAndAngle()
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

		// 重力ベクトルを算出
		Vector2 addGravity = new Vector2(Mathf.Sin(angle), -Mathf.Cos(angle)).normalized * gravity;
		// 重力ベクトルに垂直な単位ベクトルと速度の単位ベクトルから抵抗ベクトルを算出
		Vector2 addDrag = new Vector2(-Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
		float dot = Vector2.Dot(addDrag, rigitbody.linearVelocity.normalized); // 内積で印加方向と印加率を計算
		addDrag *= dot * drag;
		// 重力と抵抗を合算（この際ベクトルdirは印加用変数として再利用）
		dir = addGravity - addDrag;

		return dir * power;
	}
	#endregion

	#region  以下、端末別のジャイロセンサ対応処理

	/// <summary>
	/// Android端末かどうか
	/// </summary>
	static readonly bool isAndroid = Application.platform == RuntimePlatform.Android;
	/// <summary>
	/// iOS端末かどうか
	/// </summary>
	static readonly bool isIOS = Application.platform == RuntimePlatform.IPhonePlayer;

	/// <summary>
	/// 端末がスマートフォン（AndroidかiOS、あるいはUnity Remote）かどうかを判別
	/// </summary>
	/// <returns>スマートフォンで再生しているか</returns>
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
	#endregion
}
