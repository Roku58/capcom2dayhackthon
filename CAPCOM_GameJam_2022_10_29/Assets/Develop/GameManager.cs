using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject _player;

    PlayerController _controller;

    [SerializeField]
    Transform _playerTransformPos;

    [SerializeField]
    bool _isGameEnd;
    // ----------------------------------- �ϐ� ----------------------------------
    static public float mPlayerMoveLength = 0.0f; // �v���C���[�̈ړ����� 
    private float mGameSeconds = 0.0f; // �Q�[���̃^�C�}�[

    public float mPlayerSpeed {  get; private set; } // �v���C���[�̑��x
    private bool mIsSetPlayerMoveLength = false; // �Q�[���}�l�[�W���[�ɒl�̑�����I���������ǂ���
    // ----------------------------------- �萔 ----------------------------------
    [SerializeField] private float mMaxGameSeconds = 60.0f;

    void Start()
    {
        // -------------------------------- ������ --------------------------------
        mPlayerMoveLength = 0;
        mGameSeconds = 0.0f;
        _player = Instantiate(_player);
        _player.transform.position = _playerTransformPos.transform.position;

        _controller = _player.gameObject.GetComponent<PlayerController>();

        mIsSetPlayerMoveLength = false; // �l���Z�b�g�����t���O������������
    }

    void Update()
    {
        _isGameEnd = _player.gameObject.GetComponent<PlayerController>()._isDeath;

        mPlayerSpeed = _controller.mCurrentSpeed;

        mGameSeconds += Time.deltaTime; // �^�C�}�[�����Z
        if (!mIsSetPlayerMoveLength &&(_isGameEnd || IsEndGame())) // �v���C���[�����ʂ���莞�Ԃ���
        {
            GameOver();
        }
    }

    void GameOver()
    {
        // --------------------------- �v���C���[�̏����󂯎�� ---------------------------
        mPlayerMoveLength = _controller.mRunLength;

        // �l���Z�b�g�����t���O��؂�ւ��Ă���
        mIsSetPlayerMoveLength = true;
    }

    public bool IsEndGame()    // �Q�[���I���t���O
    {
        return mMaxGameSeconds <= mGameSeconds;
    }
}
