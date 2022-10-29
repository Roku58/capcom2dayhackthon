using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ----------------------------------- �萔(���̒l�͒����Ɋ܂܂Ȃ�) ----------------------------------
    private const int LaneWidth = 2; //�@���[���̕�
    private const int mMaxLaneCounts = 4; // �ő僌�[����
    private const float mPosY = 0.5f;     // �n�ʂ̍���
    private const int mGameOverPos = -5;  //�@�Q�[���I�[�o�[�ɂȂ�ʒu
    private const float mMaxInvTime = 1.0f;  //�@�Q�[���I�[�o�[�ɂȂ�ʒu

    // ----------------------------------- �ϐ� ----------------------------------
    private int mCurrentExp = 0; // ���݂̌o���l
    public int mCurrentLv { get; private set; } // ���݂̃��x��
    private int mHp = 0;
    private int mCurrentLane = 0; // ���݂̃��[��
    private int mPreLane=0; // �ЂƂO�̃��[��
    private float mLaneThreshold = 0;// ���`��Ԃ̊���
    public float mCurrentSpeed { get; private set; } // �v���C���[�̈ړ����x
    public bool _isDeath { get; private set; } // ��������


    // ------------------------------- �������ɐݒ肷��ϐ� ------------------------------
    [SerializeField] private int mMaxExp = 0;
    [SerializeField] private int mMaxExpAddition = 3; // ���x���A�b�v���Ƃ̌o���l�̏㏸��
    [SerializeField] private int mMaxHp = 3;    // �ő�HP
    [SerializeField] private float mHorizontalMoveSpeed = 5.0f; // �������̈ړ����x
    [SerializeField] private float mBaseSpeed = 10.0f; // ��{�̈ړ����x
    [SerializeField] private float mAcceleration = 10.0f; // �����x
    

    // Start is called before the first frame update
    void Start()
    {
        // ------------------------------- �ϐ��������� ------------------------------
        mCurrentExp = 0;
        mCurrentLv = 1;
        mHp = mMaxHp; // �̗͂�������
        mCurrentLane = 0;
        mLaneThreshold = 1.0f; // ��ԏI����Ԃɂ��Ă���
        mCurrentSpeed = mBaseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        InputMoveHorizontal(); // ���͎�t
        UpdateMoveHorizontal(); // �ʒu�X�V

        MoveVertical(); // �c�Ɉړ�����

        // �ړ����x���X�V����
        UpdateSpeed();
    }


    void InputMoveHorizontal() // ����ɉ����Ĉʒu��ύX����
    {
        // �L�[�{�[�h������͂��󂯎��
        if (mLaneThreshold >= 1.0f)�@// �ړ�����
        {
            if (Input.GetKeyDown(KeyCode.D)) // �E����
            {
                mPreLane = mCurrentLane;
                mCurrentLane++;
                mLaneThreshold = 0.0f;
            }
            else if (Input.GetKeyDown(KeyCode.A)) // ������
            {
                mPreLane = mCurrentLane;
                mCurrentLane--;
                mLaneThreshold = 0.0f;
            }
        }
        // ���݂̃��[�������[���̍ő�l�𒴂����狸������
        mCurrentLane = Math.Clamp(mCurrentLane, 0, mMaxLaneCounts - 1);
    }

    void MoveVertical() // �̗͂ɉ����ďc�Ɉړ�����
    {
        // ���݂̗̑͂ɉ����Ĉʒu���X�V����
        var posZ = mGameOverPos + mHp;
        transform.position = new Vector3(transform.position.x, transform.position.y, posZ);
    }

    void UpdateMoveHorizontal() //�@���[���̔ԍ�����ړ�����֐�
    {
        // ���[���̎n�_
        var minLanePoint = 1 + -(mMaxLaneCounts);
        var posX = minLanePoint + (mCurrentLane * LaneWidth);  // X���W���Z�o
        // �ЂƂO�̃��[���ʒu���Z�o
        var prePosX = minLanePoint + (mPreLane * LaneWidth);  // X���W���Z�o

        mLaneThreshold += Time.deltaTime * mHorizontalMoveSpeed;
        mLaneThreshold = Math.Clamp(mLaneThreshold, 0.0f, 1.0f);
        // ���`��Ԃňړ�
        var pos=Vector3.Lerp(new Vector3(prePosX, mPosY, 0.0f), new Vector3(posX, mPosY, 0.0f), mLaneThreshold);

        transform.position = new Vector3(pos.x, pos.y, pos.z);
    }

    void UpdateSpeed()
    {
        // ���x���X�V����
        mCurrentSpeed += Time.deltaTime * mAcceleration;
    }

    // ----------------------------- �O������Ăяo���Ă��炤�֐� ----------------------------
    public void Damaged(int Damage_) // �_���[�W���󂯂�֐�
    {
        mHp -= Damage_; // �̗͂����炷

        // ���x��������
        mCurrentSpeed = mBaseSpeed;
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


    // -------------------------------- �����蔻��̊֐� -------------------------------
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            _isDeath = true;
        }
    }
}
