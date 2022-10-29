using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject _player;

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
    }

    void Update()
    {
        mGameSeconds += Time.deltaTime; // �^�C�}�[�����Z
    }

    void GameOver()
    {

    }

    public bool IsEndGame()    // �Q�[���I���t���O
    {
        return mMaxGameSeconds <= mGameSeconds;
    }
}