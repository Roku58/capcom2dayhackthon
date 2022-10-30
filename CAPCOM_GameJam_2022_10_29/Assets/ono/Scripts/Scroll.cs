using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
     float speed = 1.0f;

    public float startLine;//�w�i�̃X�N���[�����J�n����ʒu
    public float deadLine; //�w�i�̃X�N���[�����I������ʒu

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

        //���փX�N���[��
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        //������𒴂������֖߂�
        if (transform.position.z  < deadLine)
        {
            transform.position = new Vector3(0,0 , startLine);
        }
    }

    //���[�������̏����ݒ�
    public void setup(float start, float dead)
    {
        this.startLine = start;
        this.deadLine = dead;
    }
}
