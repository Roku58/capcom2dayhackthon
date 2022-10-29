using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject _player;

    // ----------------------------------- 変数 ----------------------------------
    public float mPlayerMoveLength = 0.0f; // プレイヤーの移動距離 
    private float mGameSeconds = 0.0f; // ゲームのタイマー

    // ----------------------------------- 定数 ----------------------------------
    [SerializeField] private float mMaxGameSeconds = 60.0f;

    void Start()
    {
        // -------------------------------- 初期化 --------------------------------
        mPlayerMoveLength = 0;
        mGameSeconds = 0.0f; 
    }

    void Update()
    {
        mGameSeconds += Time.deltaTime; // タイマーを加算
    }

    void GameOver()
    {

    }

    public bool IsEndGame()    // ゲーム終了フラグ
    {
        return mMaxGameSeconds <= mGameSeconds;
    }
}
