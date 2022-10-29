using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float speed = 1.0f;

    [SerializeField] private float startLine;//�w�i�̃X�N���[�����J�n����ʒu
    [SerializeField] private float deadLine; //�w�i�̃X�N���[�����I������ʒu

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
