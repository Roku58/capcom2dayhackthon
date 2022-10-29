using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float speed = 1.0f;

    public float startLine;//�w�i�̃X�N���[�����J�n����ʒu
    public float deadLine; //�w�i�̃X�N���[�����I������ʒu

    public List<GameObject> lanes = new List<GameObject>();

    Vector3 cameraRectMin;

    void Update()
    {
        //���փX�N���[��
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        //������𒴂������֖߂�
        if (transform.position.z  < deadLine)
        {
            transform.position = new Vector3(0,0 , startLine);
        }
    }

    //���[�������̏����ݒ�
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
