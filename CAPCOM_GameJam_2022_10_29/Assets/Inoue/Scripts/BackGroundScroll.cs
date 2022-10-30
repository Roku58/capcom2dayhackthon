using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class BackGroundScroll : MonoBehaviour
{
    [Header("�w�i�ƂȂ�I�u�W�F�N�g")]
    public GameObject BackGroundObj;   //�w�i�ƂȂ�I�u�W�F�N�g
    [Header("�X�N���[���̃X�s�[�h(�{�Ԃł�GameManager���玩���Ŏ擾���܂�)")]
    public float ScrollSpeed;           //�X�N���[���̃X�s�[�h(�{�Ԃł�GameManager���玩���Ŏ擾���܂�)
    [Header("�X�N���[���̕���")]
    public Vector3 ScrollVector;        //�X�N���[���̕���
    [Header("���Ŏ���")]
    public float DisappearTime;         //���Ŏ���
    [Header("GameManager(�X�N���[���X�s�[�h�擾�p)")]
    public GameManager _gameManager;//GameManager�̃X�N���[���X�s�[�h���擾�p
    [Header("BackGroundObj���X�|�[������ꏊ")]
    public Transform Spawn;             //BackGroundObj���X�|�[������ꏊ
    [Header("�����p�x�̃J�E���g")]
    private float t;                    //�����p�x�̃J�E���g
    [Header("Obj�̃X�N���[�������ɑ΂��镨���I�Ȓ���(���ۂ��͂��ɒZ���ݒ�𐄏�)")]
    public float objLength;             //�I�u�W�F�N�g�̃X�N���[���X�s�[�h�ɑ΂��镨���I�Ȓ���
    [Header("�����p�x�����߂�ϐ�")]
    public float CreateTime;            //�����p�x�����߂�ϐ�
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
