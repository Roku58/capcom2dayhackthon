using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 資源の移動とプレイヤーとの衝突のクラス
/// </summary>
public class ObjectMove : MonoBehaviour
{
    [SerializeField]
    int _level;

    [SerializeField]
    int _exp;

    [SerializeField]
    float _moveSpeed;

    [SerializeField]
    int _damage;


    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    /// <summary>
    /// オブジェクトの移動処理
    /// </summary>
    private void Move()
    {

        Vector3 pos = transform.position;
        pos.z -= 0.01f * _moveSpeed;
        transform.position = pos;

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            Debug.Log("ダメージ");
            var player = GetComponent<PlayerController>();
            if (_level <= player.mCurrentLv)
            {
                player.GetResource(_exp);

                Destroy(gameObject);
            }
            else
            {
                player.Damaged(_damage);
            }
        }
    }
}
