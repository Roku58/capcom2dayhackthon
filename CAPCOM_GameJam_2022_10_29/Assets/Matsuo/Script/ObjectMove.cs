using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

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

    CinemachineImpulseSource _impulseSource = default;

    GlitchFx _glitchFx;

    void Start()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        _glitchFx = GameObject.Find("Main Camera").GetComponent<GlitchFx>();
        _glitchFx.Intensity = 0;
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

            var player = other.gameObject.GetComponent<PlayerController>();
            int playerLevl = player.CurrentLv;

            if (_level <= playerLevl)
            {
                Debug.Log("衝撃");

                _impulseSource.GenerateImpulse(new Vector3(0, 0, -1));

                player.GetResource(_exp);

                Destroy(gameObject);
            }
            else
            {
                Debug.Log("ダメージ");
                player.Damaged(_damage);
                StartCoroutine(ScreenDamageEffect());
            }
        }
    }

    IEnumerator ScreenDamageEffect()
    {
        //duration秒かけて任意の変数の値が currentValue から endValue になります
        //_glitchFx.Intensity = 0;

        // 何秒かかるか
        float duration = 1f;

        // 最終値変化量
        float endValue = 1;

        // 現在の値（変化する値）
        float currentValue = 0;

        // Tweenの生成
        DOTween.To
            (
                () => currentValue,
                value => currentValue = value,
                endValue,
                duration
            )
            .OnUpdate(() => _glitchFx.Intensity = currentValue);
        yield return null;
        DOTween.To
    (
        () => currentValue,
        value => currentValue = value,
        0,
        duration
    )
    .OnUpdate(() => _glitchFx.Intensity = currentValue);
    }
}
