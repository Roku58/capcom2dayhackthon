using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float speed = 1.0f;

    public float startLine;//背景のスクロールを開始する位置
    public float deadLine; //背景のスクロールが終了する位置

    public List<GameObject> lanes = new List<GameObject>();

    Vector3 cameraRectMin;

    void Update()
    {
        //下へスクロール
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        //判定線を超えたら上へ戻す
        if (transform.position.z  < deadLine)
        {
            transform.position = new Vector3(0,0 , startLine);
        }
    }

    //レーン長さの初期設定
    public void setup(float length, float startPos)
    {
        this.startLine = length;
        this.deadLine = -length;
        transform.localPosition = new Vector3(transform.position.x, transform.position.y, startPos);
        for(int i = 0; i < lanes.Count ;i++)
        {
            lanes[i].transform.localScale = new Vector3(lanes[i].transform.localScale.x, lanes[i].transform.localScale.y, length);
        }
    }
}
