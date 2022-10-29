using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ----------------------------------- �萔(���̒l�͒����Ɋ܂܂Ȃ�) ----------------------------------
    private const int LaneWidth = 2; //�@���[���̕�
    private int mMaxLaneCounts = 3; // �ő僌�[����
    private float mPosY = 0.5f;     // �n�ʂ̍���
    // ----------------------------------- �ϐ� ----------------------------------
    private int mCurrentExp = 0; // ���݂̌o���l
    public int mCurrentLv { get; private set; } // ���݂̃��x��
    private int mHp = 0;
    private int mCurrentLane = 0; // ���݂̃��[��

    // ------------------------------- �������ɐݒ肷��ϐ� ------------------------------
    [SerializeField] private int mMaxExp = 0;
    [SerializeField] private int mMaxExpAddition = 3; // ���x���A�b�v���Ƃ̌o���l�̏㏸��
    [SerializeField] private int mMaxHp = 3;    // �ő�HP
    

// Start is called before the first frame update
void Start()
    {
        // ------------------------------- �ϐ��������� ------------------------------
        mCurrentExp = 0;
        mCurrentLv = 1;
        mHp = mMaxHp; // �̗͂�������
        mCurrentLane = 0;
    }

    // Update is called once per frame
    void Update()
    {
        InputMove(); // ���͎�t
        UpdateMove(); // �ʒu�X�V

    }


    void InputMove() // ����ɉ����Ĉʒu��ύX����
    {
        // �L�[�{�[�h������͂��󂯎��
        var inputX= Input.GetAxisRaw("Horizontal"); // ���E���͂��擾����
        if (inputX > 0.5f) // �E����
        {
            mCurrentLane++;

        }
        else if (inputX < -0.5f) // ������
        {
            mCurrentLane--;
        }
        // ���݂̃��[�������[���̍ő�l�𒴂����狸������
        mCurrentLane = Math.Clamp(mCurrentLane, 0, mMaxLaneCounts);
    }

    void UpdateMove() //�@���[���̔ԍ�����ړ�����֐�
    {
        var posX = mCurrentLane * LaneWidth;  // X���W���Z�o
        transform.position = new Vector3(posX, mPosY, 0.0f);
    }

    // ----------------------------- �O������Ăяo���Ă��炤�֐� ----------------------------
    public void Damaged(int Damage_) // �_���[�W���󂯂�֐�
    {
        mHp -= Damage_; // �̗͂����炷
    }

    public void GetResource(int Exp_) // �������擾����֐�(���� ���Z����o���l)
    {
        // �o���l�����Z����
        mCurrentExp += Exp_; 
        // �ő�l���I�[�o�[�����烌�x�����㏸�����Čo���l��������
        if (mCurrentExp >= mMaxExp)
        {
            mCurrentLv++; // ���x�������Z
            mCurrentExp-=mMaxExp; // �o���l�����̐��������Ƃ�
            mMaxExp += mMaxExpAddition; // �ő�o���l�����Z����
        }
    }
}
