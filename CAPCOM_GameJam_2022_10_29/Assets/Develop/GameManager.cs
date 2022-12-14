using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject _player;

    PlayerController _controller;

    [SerializeField]
    Transform _playerTransformPos;

    [SerializeField]
    bool _isGameEnd;

    [SerializeField]
    TimeManager _timeManager;

    [SerializeField]
    ParticleSystem[] _particleSystem;
    // ----------------------------------- 変数 ----------------------------------
    static public float mPlayerMoveLength = 0.0f; // プレイヤーの移動距離 
    private float mGameSeconds = 0.0f; // ゲームのタイマー

    public float mPlayerSpeed {  get; private set; } // プレイヤーの速度
    private bool mIsSetPlayerMoveLength = false; // ゲームマネージャーに値の代入が終了したかどうか
    // ----------------------------------- 定数 ----------------------------------
    [SerializeField] private float mMaxGameSeconds = 60.0f;

    void Start()
    {

        // -------------------------------- 初期化 --------------------------------
        mPlayerMoveLength = 0;
        mGameSeconds = 0.0f;
        _player = Instantiate(_player);
        _player.transform.position = _playerTransformPos.transform.position;

        //_timeManager = _player.gameObject.GetComponent<TimeManager>();
        _controller = _player.gameObject.GetComponent<PlayerController>();

        mIsSetPlayerMoveLength = false; // 値をセットしたフラグを初期化する
    }

    void Update()
    {
        _isGameEnd = _player.gameObject.GetComponent<PlayerController>()._isDeath;

        mPlayerSpeed = _controller.mCurrentSpeed;

        mGameSeconds += Time.deltaTime; // タイマーを加算
        if (!mIsSetPlayerMoveLength &&(_isGameEnd)) // プレイヤーが死ぬか一定時間たつと
        {
            GameOver();
        }
    }

    void GameOver()
    {
        // --------------------------- プレイヤーの情報を受け取る ---------------------------
        mPlayerMoveLength = _controller.mRunLength;

        // 値をセットしたフラグを切り替えておく
        mIsSetPlayerMoveLength = true;
        _timeManager.SlowDown();
        for(int i = 0; i < _particleSystem.Length; i++)
        {
            _particleSystem[i].Play();
        }

        //SceneManager.LoadScene("Result");
    }

    public bool IsEndGame()    // ゲーム終了フラグ
    {
        return mMaxGameSeconds <= mGameSeconds;
    }
}
