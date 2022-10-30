using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class BackGroundScroll : MonoBehaviour
{
    [Header("背景となるオブジェクト")]
    public GameObject BackGroundObj;   //背景となるオブジェクト
    [Header("スクロールのスピード(本番ではGameManagerから自動で取得します)")]
    public float ScrollSpeed;           //スクロールのスピード(本番ではGameManagerから自動で取得します)
    [Header("スクロールの方向")]
    public Vector3 ScrollVector;        //スクロールの方向
    [Header("消滅時間")]
    public float DisappearTime;         //消滅時間
    [Header("GameManager(スクロールスピード取得用)")]
    public GameManager _gameManager;//GameManagerのスクロールスピードを取得用
    [Header("BackGroundObjがスポーンする場所")]
    public Transform Spawn;             //BackGroundObjがスポーンする場所
    [Header("生成頻度のカウント")]
    private float t;                    //生成頻度のカウント
    [Header("Objのスクロール方向に対する物理的な長さ(実際より僅かに短く設定を推奨)")]
    public float objLength;             //オブジェクトのスクロールスピードに対する物理的な長さ
    [Header("生成頻度を求める変数")]
    public float CreateTime;            //生成頻度を求める変数
    // Start is called before the first frame update
    void Start()
    {
       _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(_gameManager)
        ScrollSpeed = _gameManager.mPlayerSpeed;
        CreateTime = objLength / ScrollSpeed;
        if (t >= CreateTime)
        {
            Create();
            t = 0;
        }

        
    }
    void Create()
    {
        GameObject Obj = GameObject.Instantiate(BackGroundObj);
        Obj.transform.position = Spawn.transform.position;
        Obj.transform.rotation = Spawn.transform.rotation;
        Obj.AddComponent<MoveStraight>();
        Obj.GetComponent<MoveStraight>().MoveVector = ScrollVector;
        Obj.GetComponent<MoveStraight>().Speed = ScrollSpeed;
        Destroy(Obj, DisappearTime);

    }
}
