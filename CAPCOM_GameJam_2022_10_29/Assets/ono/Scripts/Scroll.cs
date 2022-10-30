using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
     float speed = 1.0f;

    public float startLine;//背景のスクロールを開始する位置
    public float deadLine; //背景のスクロールが終了する位置

    public List<GameObject> lanes = new List<GameObject>();

    Vector3 cameraRectMin;

    GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    void Update()
    {
        speed = _gameManager.mPlayerSpeed;

        //下へスクロール
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        //判定線を超えたら上へ戻す
        if (transform.position.z  < deadLine)
        {
            transform.position = new Vector3(0,0 , startLine);
        }
    }

    //レーン長さの初期設定
    public void setup(float start, float dead)
    {
        this.startLine = start;
        this.deadLine = dead;
    }
}
