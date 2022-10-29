using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject _player;

    [SerializeField]
    Transform _playerTransformPos;

    [SerializeField]
    bool _isGameEnd;
    // ----------------------------------- �ϐ� ----------------------------------
    public float mPlayerMoveLength = 0.0f; // �v���C���[�̈ړ����� 
    private float mGameSeconds = 0.0f; // �Q�[���̃^�C�}�[

    // ----------------------------------- �萔 ----------------------------------
    [SerializeField] private float mMaxGameSeconds = 60.0f;

    void Start()
    {
        // -------------------------------- ������ --------------------------------
        mPlayerMoveLength = 0;
        mGameSeconds = 0.0f;
        _player = Instantiate(_player);
        _player.transform.position = _playerTransformPos.transform.position;

    }

    void Update()
    {
        _isGameEnd = _player.gameObject.GetComponent<PlayerController>()._isDeath;

        mGameSeconds += Time.deltaTime; // �^�C�}�[�����Z
        if(_isGameEnd)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("�Q�[���I�[�o�[");
    }

    public bool IsEndGame()    // �Q�[���I���t���O
    {
        return mMaxGameSeconds <= mGameSeconds;
    }
}
