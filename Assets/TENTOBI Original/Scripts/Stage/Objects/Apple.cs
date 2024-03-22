using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    #region private変数定義
    /// <summary>
    /// GameDirectorコンポーネント
    /// </summary>
    private GameDirector game;
    #endregion

    [Header("CP(操作力)の回復量")]
    /// <summary>
    /// このアイテムを獲得時に回復するCP(操作力)
    /// </summary>
    public int CP = 50;

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
            Debug.Log("CPを回復(+" + CP.ToString() + ")しました");
            game.CP += CP;

            Destroy(this.gameObject);
        }
    }
}
