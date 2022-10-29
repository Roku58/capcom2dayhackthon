using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �X�e�[�W�ɃI�u�W�F�N�g�𐶐�����N���X
/// </summary>
public class ObjectGeneration : MonoBehaviour
{
    [SerializeField, Header("�����p�x")]
    float _intervalTime;

    [SerializeField, Header("�o�ߎ��ԁ@�m�F�p�Ȃ̂Őݒ�s�v")]
    float _elapsedTime;

    [SerializeField, Header("�����|�C���g ��̃I�u�W�F�N�g���h���b�O")]
    Transform[] _generationPoints;

    [SerializeField, Header("��������I�u�W�F�N�g")]
    GameObject[] _generationObjects;

    void Start()
    {
        
    }

    void Update()
    {
        _elapsedTime�@+= Time.deltaTime;
        ObjectSpown(_intervalTime);
    }

    void ObjectSpown(float second)
    {
        if(_elapsedTime > _intervalTime)
        {
            var rdmPos = Random.Range(0, _generationPoints.Length);
            var rdmObj = Random.Range(0, _generationObjects.Length);

            var obj = Instantiate(_generationObjects[rdmObj]);
            obj.gameObject.transform.position = _generationPoints[rdmPos].position;
            _elapsedTime = 0;
        }
    }
}
