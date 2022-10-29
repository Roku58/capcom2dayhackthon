using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ----------------------------------- 定数(この値は調整に含まない) ----------------------------------
    private const int LaneWidth = 2; //　レーンの幅
    private int mMaxLaneCounts = 3; // 最大レーン数
    private float mPosY = 0.5f;     // 地面の高さ
    // ----------------------------------- 変数 ----------------------------------
    private int mCurrentExp = 0; // 現在の経験値
    public int mCurrentLv { get; private set; } // 現在のレベル
    private int mHp = 0;
    private int mCurrentLane = 0; // 現在のレーン

    // ------------------------------- 調整時に設定する変数 ------------------------------
    [SerializeField] private int mMaxExp = 0;
    [SerializeField] private int mMaxExpAddition = 3; // レベルアップごとの経験値の上昇幅
    [SerializeField] private int mMaxHp = 3;    // 最大HP
    

// Start is called before the first frame update
void Start()
    {
        // ------------------------------- 変数を初期化 ------------------------------
        mCurrentExp = 0;
        mCurrentLv = 1;
        mHp = mMaxHp; // 体力を初期化
        mCurrentLane = 0;
    }

    // Update is called once per frame
    void Update()
    {
        InputMove(); // 入力受付
        UpdateMove(); // 位置更新

    }


    void InputMove() // 操作に応じて位置を変更する
    {
        // キーボードから入力を受け取る
        var inputX= Input.GetAxisRaw("Horizontal"); // 左右入力を取得する
        if (inputX > 0.5f) // 右入力
        {
            mCurrentLane++;

        }
        else if (inputX < -0.5f) // 左入力
        {
            mCurrentLane--;
        }
        // 現在のレーンがレーンの最大値を超えたら矯正する
        mCurrentLane = Math.Clamp(mCurrentLane, 0, mMaxLaneCounts);
    }

    void UpdateMove() //　レーンの番号から移動する関数
    {
        var posX = mCurrentLane * LaneWidth;  // X座標を算出
        transform.position = new Vector3(posX, mPosY, 0.0f);
    }

    // ----------------------------- 外部から呼び出してもらう関数 ----------------------------
    public void Damaged(int Damage_) // ダメージを受ける関数
    {
        mHp -= Damage_; // 体力を減らす
    }

    public void GetResource(int Exp_) // 資源を取得する関数(引数 加算する経験値)
    {
        // 経験値を加算する
        mCurrentExp += Exp_; 
        // 最大値をオーバーしたらレベルを上昇させて経験値を下げる
        if (mCurrentExp >= mMaxExp)
        {
            mCurrentLv++; // レベルを加算
            mCurrentExp-=mMaxExp; // 経験値差分の整合性をとる
            mMaxExp += mMaxExpAddition; // 最大経験値を加算する
        }
    }
}
