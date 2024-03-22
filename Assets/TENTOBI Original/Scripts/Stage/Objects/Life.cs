using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    #region private変数定義
    /// <summary>
    /// GameDirectorコンポーネント
    /// </summary>
    private GameDirector game;
    #endregion

    [Header("ライフ(残機)の回復量")]
    /// <summary>
    /// このアイテムを獲得時に回復するライフ(残機)
    /// </summary>
    public int life = 2;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameDirector>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("ライフを回復(+" + life.ToString() + ")しました");
            game.life += life;

            Destroy(this.gameObject);
        }
    }
}
