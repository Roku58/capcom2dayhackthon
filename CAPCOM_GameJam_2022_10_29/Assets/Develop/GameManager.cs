using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject _player;

    [SerializeField]
    Transform _playerTransformPos;

    [SerializeField]
    bool _isGameEnd;
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
        _player = Instantiate(_player);
        _player.transform.position = _playerTransformPos.transform.position;

    }

    void Update()
    {
        _isGameEnd = _player.gameObject.GetComponent<PlayerController>()._isDeath;

        mGameSeconds += Time.deltaTime; // タイマーを加算
        if(_isGameEnd)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("ゲームオーバー");
    }

    public bool IsEndGame()    // ゲーム終了フラグ
    {
        return mMaxGameSeconds <= mGameSeconds;
    }
}
