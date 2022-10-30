using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using TMPro;

/// <summary>
/// �����̈ړ��ƃv���C���[�Ƃ̏Փ˂̃N���X
/// </summary>
public class ObjectMove : MonoBehaviour
{
    [SerializeField]
    int _level;

    [SerializeField]
    int _exp;

    float _moveSpeed;

    [SerializeField]
    int _damage;

    CinemachineImpulseSource _impulseSource = default;

    GlitchFx _glitchFx;

    [SerializeField]
    TextMeshProUGUI  _levelText;

    GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _moveSpeed = _gameManager.mPlayerSpeed;

        _levelText.text = ($"Lv:{_level}");
        _impulseSource = GetComponent<CinemachineImpulseSource>();

        _glitchFx = GameObject.Find("Main Camera").GetComponent<GlitchFx>();
        _glitchFx.Intensity = 0;
    }

    private void FixedUpdate()
    {
        Move();
    }

    //private void Update()
    //{
    //    //Move();
    //}

    /// <summary>
    /// �I�u�W�F�N�g�̈ړ�����
    /// </summary>
    private void Move()
    {

        Vector3 pos = transform.position;
        pos.z -= 1f * _moveSpeed * Time.deltaTime;
        transform.position = pos;

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {

            var player = other.gameObject.GetComponent<PlayerController>();
            int playerLevl = player.mCurrentLv;

            if (_level <= playerLevl)
            {
                Debug.Log("�Ռ�");

                _impulseSource.GenerateImpulse(new Vector3(0, 0, -1));

                player.GetResource(_exp);

                Destroy(gameObject);
            }
            else
            {
                Debug.Log("�_���[�W");
                player.Damaged(_damage);
                StartCoroutine(ScreenDamageEffect());

                Destroy(gameObject);
            }
        }
        else if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);

        }
    }

    IEnumerator ScreenDamageEffect()
    {
        //duration�b�����ĔC�ӂ̕ϐ��̒l�� currentValue ���� endValue �ɂȂ�܂�
        //_glitchFx.Intensity = 0;

        // ���b�����邩
        float duration = 0.3f;

        // �ŏI�l�ω���
        float endValue = 0.3f;

        // ���݂̒l�i�ω�����l�j
        float currentValue = 0;

        // Tween�̐���
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
