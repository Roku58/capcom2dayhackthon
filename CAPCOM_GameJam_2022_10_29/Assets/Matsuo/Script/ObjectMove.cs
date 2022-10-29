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

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            //var playerLevel = GetComponent<Player>().;
            //if (_level <= playerLevel)
            //{

            //    Destroy(gameObject);
            //}
            //else
            //{

            //}
        }
    }
}
