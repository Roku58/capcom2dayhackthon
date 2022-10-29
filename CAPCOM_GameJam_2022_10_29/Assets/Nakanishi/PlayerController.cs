using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ----------------------------------- 定数(この値は調整に含まない) ----------------------------------
    private const int LaneWidth = 2; //　レーンの幅

    // ----------------------------------- 変数 ----------------------------------
    private int mCurrentExp = 0; // 現在の経験値
    public int mCurrentLv { get; private set; } // 現在のレベル
    private int mHp = 0;

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
    }

    // Update is called once per frame
    void Update()
    {

    }


    void InputMove() // 操作に応じて位置を変更する
    {

    }


    // ----------------------------- 外部から呼び出してもらう関数 ----------------------------
    public void Damaged() // ダメージを受ける関数
    {
        mHp--; // 体力を減らす
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
