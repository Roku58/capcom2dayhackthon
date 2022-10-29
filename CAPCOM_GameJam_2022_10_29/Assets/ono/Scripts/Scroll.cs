using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float speed = 1.0f;

    [SerializeField] private float startLine;//背景のスクロールを開始する位置
    [SerializeField] private float deadLine; //背景のスクロールが終了する位置

    Vector3 cameraRectMin;

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (transform.position.z  < deadLine)
        {
            transform.position = new Vector3(0,0 , startLine);
        }
    }
}
