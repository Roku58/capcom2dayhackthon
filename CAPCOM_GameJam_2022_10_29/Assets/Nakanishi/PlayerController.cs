using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ----------------------------------- 定数(この値は調整に含まない) ----------------------------------
    private const int LaneWidth = 2; //　レーンの幅
    private const int mMaxLaneCounts = 4; // 最大レーン数
    private const float mPosY = 0.5f; // 地面の高さ
    private const int mGameOverPos = -5; //　ゲームオーバーになる位置
    private const int mMaxLv = 3; // 最大レベル
    // ----------------------------------- 変数 ----------------------------------
    public int mCurrentExp { get; private set; } // 現在の経験値
    public int mCurrentLv { get; private set; } // 現在のレベル
    public int mHp { set; private get; } // 現在の体力

    private int mCurrentLane = 0; // 現在のレーン
    private int mPreLane = 0; // ひとつ前のレーン
    private float mLaneThreshold = 0; // 線形補間の割合
    public float mCurrentSpeed { get; private set; } // プレイヤーの移動速度
    public bool _isDeath { get; private set; } // 生存判定
    public float mRunLength { get; private set; } // 移動距離
    private bool mIsChargeFiver = false; // フィーバーゲージを加算できる状態にあるかどうか  
    private float mFiverGauge = 0.0f; // フィーバーゲージの量
    public bool mIsFiver { private set; get; } // フィーバー中かどうか

    // ------------------------------- 調整時に設定する変数 ------------------------------
    [SerializeField] public int mMaxExp { get; private set; }
    [SerializeField] private int mMaxExpAddition = 3; // レベルアップごとの経験値の上昇幅
    [SerializeField] private int mMaxHp = 3; // 最大HP
    [SerializeField] private float mHorizontalMoveSpeed = 5.0f; // 横方向の移動速度
    [SerializeField] private float mBaseSpeed = 10.0f; // 基本の移動速度
    [SerializeField] private float mSpeed = 10.0f; // 加速度
    [SerializeField] private const float mMaxSpeed = 50.0f; //　最大速度
    [SerializeField] private float mFiverGaugeSpeed = 10.0f; // フィーバーゲージの上昇量
    [SerializeField] private float mMaxFiverGauge = 100.0f; // フィーバーゲージの最大値
    [SerializeField] private float mFiverGaugeReduceSpeed = 20.0f; // フィーバーゲージが減る量
    [SerializeField] private GameObject[] mMesh = new GameObject[3]; // ゲームオブジェクトの配列
    [SerializeField] private CamEffect mCamEffect;
    // Start is called before the first frame update
    void Start()
    {
        // ------------------------------- 変数を初期化 ------------------------------
        mCurrentExp = 0;
        mCurrentLv = 1;
        mHp = mMaxHp; // 体力を初期化
        mCurrentLane = 0;
        mLaneThreshold = 1.0f; // 補間終了状態にしておく
        mCurrentSpeed = mBaseSpeed; // 速度を初期化
        mFiverGauge = 0.0f; // ゲージを初期化
        fChangeMesh(); //メッシュを切り替える
    }

    // Update is called once per frame
    void Update()
    {
        InputMoveHorizontal(); // 入力受付
        UpdateMoveHorizontal(); // 位置更新

        MoveVertical(); // 縦に移動する
        UpdateSpeed(); // 移動速度を更新する
        UpdateFiver(); // フィーバーを更新する
    }


    void InputMoveHorizontal() // 操作に応じて位置を変更する
    {
        // キーボードから入力を受け取る
        if (mLaneThreshold >= 1.0f) // 移動完了
        {
            if (Input.GetKeyDown(KeyCode.D)) // 右入力
            {
                mPreLane = mCurrentLane;
                mCurrentLane++;
                mLaneThreshold = 0.0f;
            }
            else if (Input.GetKeyDown(KeyCode.A)) // 左入力
            {
                mPreLane = mCurrentLane;
                mCurrentLane--;
                mLaneThreshold = 0.0f;
            }
        }
        // 現在のレーンがレーンの最大値を超えたら矯正する
        mCurrentLane = Math.Clamp(mCurrentLane, 0, mMaxLaneCounts - 1);
    }

    void MoveVertical() // 体力に応じて縦に移動する
    {
        // 現在の体力に応じて位置を更新する
        var posZ = mGameOverPos + mHp;
        transform.position = new Vector3(transform.position.x, transform.position.y, posZ);
    }

    void UpdateMoveHorizontal() //　レーンの番号から移動する関数
    {
        // レーンの始点
        var minLanePoint = 1 + -(mMaxLaneCounts);
        var posX = minLanePoint + (mCurrentLane * LaneWidth); // X座標を算出
        // ひとつ前のレーン位置を算出
        var prePosX = minLanePoint + (mPreLane * LaneWidth); // X座標を算出

        mLaneThreshold += Time.deltaTime * mHorizontalMoveSpeed;
        mLaneThreshold = Math.Clamp(mLaneThreshold, 0.0f, 1.0f);
        // 線形補間で移動
        var pos = Vector3.Lerp(new Vector3(prePosX, mPosY, 0.0f), new Vector3(posX, mPosY, 0.0f), mLaneThreshold);

        transform.position = new Vector3(pos.x, pos.y, pos.z);
    }

    void UpdateSpeed() // スピードと距離を加算する
    {
        // 速度を更新する
        mCurrentSpeed += Time.deltaTime * mSpeed;
        // 速度をクランプする
        if (mCurrentSpeed >= mMaxSpeed)
        {
            mCurrentSpeed = mMaxSpeed;
            mIsChargeFiver = true; // フィーバーゲージをかさんするかどうか
        }
        else // 最大速度
        {
            mIsChargeFiver = false;
        }

        // 距離を加算
        mRunLength += mCurrentSpeed * Time.deltaTime;
    }

    void Fiver() // フィーバー中の更新関数 
    {
        mFiverGauge -= Time.deltaTime * mFiverGaugeReduceSpeed;
        // なくなったらフィーバー終わり
        if (mFiverGauge <= 0.0f)
        {
            mIsFiver = false;
            // 速度を規定値に戻す
            mCurrentSpeed = mBaseSpeed;
        }
    }

    void UnFiver() // フィーバーでないときの更新関数
    {
        if (!mIsChargeFiver)
        {
            return;
        }

        // フィーバーゲージを加算する
        mFiverGauge += Time.deltaTime * mFiverGaugeSpeed;

        // ゲージが最大値を超えたらフィーバーに突入する
        if (mFiverGauge >= mMaxFiverGauge)
        {
            mIsFiver = true;
        }
    }

    void UpdateFiver() // フィーバーゲージを更新する
    {
        if (!mIsFiver) // フィーバー時以外の処理
        {
            UnFiver();
        }
        else
        {
            Fiver();
        }

        mCamEffect.feverMode = mIsFiver;
        // フィーバーゲージをクランプする
        mFiverGauge = Math.Clamp(mFiverGauge, 0.0f, mMaxFiverGauge);
    }

    // ----------------------------- 外部から呼び出してもらう関数 ----------------------------
    public void Damaged(int Damage_) // ダメージを受ける関数
    {
        mHp -= Damage_; // 体力を減らす
        mCurrentSpeed = mBaseSpeed; // 速度を下げる
    }

    public void GetResource(int Exp_) // 資源を取得する関数(引数 加算する経験値)
    {
        // 経験値を加算する
        mCurrentExp += Exp_;
        // 最大値をオーバーしたらレベルを上昇させて経験値を下げる
        if (mCurrentExp >= mMaxExp)
        {
            mCurrentLv++; // レベルを加算
            // 最大レベルでクランプ
            mCurrentLv = Math.Clamp(mCurrentLv, 0, mMaxLv); 
            mCurrentExp -= mMaxExp; // 経験値差分の整合性をとる
            mMaxExp += mMaxExpAddition; // 最大経験値を加算する
            fChangeMesh(); //メッシュを切り替える
        }
    }


    // -------------------------------- 当たり判定の関数 -------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            _isDeath = true;
        }
    }

    // -------------------------------- 表示切り替え関数 -------------------------------
    void fChangeMesh()
    {
        foreach (var mesh in mMesh)
        {
            mesh.SetActive(false);
        }
        // レベルに応じたメッシュ表示を切り替え
        if (mMesh.Length >= mCurrentLv)
        {
            mMesh[mCurrentLv - 1].SetActive(true);
        }
    }
}
