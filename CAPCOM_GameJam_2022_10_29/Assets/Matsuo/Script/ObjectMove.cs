using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����̈ړ��ƃv���C���[�Ƃ̏Փ˂̃N���X
/// </summary>
public class ObjectMove : MonoBehaviour
{
    [SerializeField]
    int _level;
    [SerializeField]
    float _moveSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    /// <summary>
    /// �I�u�W�F�N�g�̈ړ�����
    /// </summary>
    private void Move()
    {

        Vector3 pos = transform.position;
        pos.z -= 0.01f * _moveSpeed;
        transform.position = pos;

    }

    

    private void OnCollisionEnter(Collision collision)
    {
        //if (collider.gameObject.GetComponent<Player>())
        //{
        //    playerLevel = GetComponent<Player>().
        //    if(_revel <= playerLevel)
        //    {

        //        Destroy(gameObject);
        //    }
        //}
    }
}
