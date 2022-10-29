using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ----------------------------------- �萔(���̒l�͒����Ɋ܂܂Ȃ�) ----------------------------------
    private const int LaneWidth = 2; //�@���[���̕�

    // ----------------------------------- �ϐ� ----------------------------------
    private int mCurrentExp = 0; // ���݂̌o���l
    public int mCurrentLv { get; private set; } // ���݂̃��x��
    private int mHp = 0;

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
    }

    // Update is called once per frame
    void Update()
    {

    }


    void InputMove() // ����ɉ����Ĉʒu��ύX����
    {

    }


    // ----------------------------- �O������Ăяo���Ă��炤�֐� ----------------------------
    public void Damaged() // �_���[�W���󂯂�֐�
    {
        mHp--; // �̗͂����炷
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
