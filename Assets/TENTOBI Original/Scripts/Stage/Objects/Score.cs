using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    #region private変数定義
    /// <summary>
    /// GameDirectorコンポーネント
    /// </summary>
    private GameDirector game;
    #endregion

    [Header("獲得スコア")]
    /// <summary>
    /// このアイテムを獲得時に得られるスコア
    /// </summary>
    public int score = 5;

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
            Debug.Log("スコア(+" + score.ToString() + ")を獲得しました");
            game.scoreInStage += score;

            Destroy(this.gameObject);
        }
    }
}
