using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ----------------------------------- �萔(���̒l�͒����Ɋ܂܂Ȃ�) ----------------------------------
    private const int LaneWidth = 2; //�@���[���̕�
    private const int mMaxLaneCounts = 4; // �ő僌�[����
    private const float mPosY = 0.5f; // �n�ʂ̍���
    private const int mGameOverPos = -5; //�@�Q�[���I�[�o�[�ɂȂ�ʒu
    private const int mMaxLv = 3; // �ő僌�x��
    // ----------------------------------- �ϐ� ----------------------------------
    public int mCurrentExp { get; private set; } // ���݂̌o���l
    public int mCurrentLv { get; private set; } // ���݂̃��x��
    public int mHp { set; private get; } // ���݂̗̑�

    private int mCurrentLane = 0; // ���݂̃��[��
    private int mPreLane = 0; // �ЂƂO�̃��[��
    private float mLaneThreshold = 0; // ���`��Ԃ̊���
    public float mCurrentSpeed { get; private set; } // �v���C���[�̈ړ����x
    public bool _isDeath { get; private set; } // ��������
    public float mRunLength { get; private set; } // �ړ�����
    private bool mIsChargeFiver = false; // �t�B�[�o�[�Q�[�W�����Z�ł����Ԃɂ��邩�ǂ���  
    private float mFiverGauge = 0.0f; // �t�B�[�o�[�Q�[�W�̗�
    public bool mIsFiver { private set; get; } // �t�B�[�o�[�����ǂ���

    // ------------------------------- �������ɐݒ肷��ϐ� ------------------------------
    [SerializeField] public int mMaxExp { get; private set; }
    [SerializeField] private int mMaxExpAddition = 3; // ���x���A�b�v���Ƃ̌o���l�̏㏸��
    [SerializeField] private int mMaxHp = 3; // �ő�HP
    [SerializeField] private float mHorizontalMoveSpeed = 5.0f; // �������̈ړ����x
    [SerializeField] private float mBaseSpeed = 10.0f; // ��{�̈ړ����x
    [SerializeField] private float mSpeed = 10.0f; // �����x
    [SerializeField] private const float mMaxSpeed = 50.0f; //�@�ő呬�x
    [SerializeField] private float mFiverGaugeSpeed = 10.0f; // �t�B�[�o�[�Q�[�W�̏㏸��
    [SerializeField] private float mMaxFiverGauge = 100.0f; // �t�B�[�o�[�Q�[�W�̍ő�l
    [SerializeField] private float mFiverGaugeReduceSpeed = 20.0f; // �t�B�[�o�[�Q�[�W�������
    [SerializeField] private GameObject[] mMesh = new GameObject[3]; // �Q�[���I�u�W�F�N�g�̔z��
    [SerializeField] private CamEffect mCamEffect;
    // Start is called before the first frame update
    void Start()
    {
        // ------------------------------- �ϐ��������� ------------------------------
        mCurrentExp = 0;
        mCurrentLv = 1;
        mHp = mMaxHp; // �̗͂�������
        mCurrentLane = 0;
        mLaneThreshold = 1.0f; // ��ԏI����Ԃɂ��Ă���
        mCurrentSpeed = mBaseSpeed; // ���x��������
        mFiverGauge = 0.0f; // �Q�[�W��������
        fChangeMesh(); //���b�V����؂�ւ���
    }

    // Update is called once per frame
    void Update()
    {
        InputMoveHorizontal(); // ���͎�t
        UpdateMoveHorizontal(); // �ʒu�X�V

        MoveVertical(); // �c�Ɉړ�����
        UpdateSpeed(); // �ړ����x���X�V����
        UpdateFiver(); // �t�B�[�o�[���X�V����
    }


    void InputMoveHorizontal() // ����ɉ����Ĉʒu��ύX����
    {
        // �L�[�{�[�h������͂��󂯎��
        if (mLaneThreshold >= 1.0f) // �ړ�����
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
        var posX = minLanePoint + (mCurrentLane * LaneWidth); // X���W���Z�o
        // �ЂƂO�̃��[���ʒu���Z�o
        var prePosX = minLanePoint + (mPreLane * LaneWidth); // X���W���Z�o

        mLaneThreshold += Time.deltaTime * mHorizontalMoveSpeed;
        mLaneThreshold = Math.Clamp(mLaneThreshold, 0.0f, 1.0f);
        // ���`��Ԃňړ�
        var pos = Vector3.Lerp(new Vector3(prePosX, mPosY, 0.0f), new Vector3(posX, mPosY, 0.0f), mLaneThreshold);

        transform.position = new Vector3(pos.x, pos.y, pos.z);
    }

    void UpdateSpeed() // �X�s�[�h�Ƌ��������Z����
    {
        // ���x���X�V����
        mCurrentSpeed += Time.deltaTime * mSpeed;
        // ���x���N�����v����
        if (mCurrentSpeed >= mMaxSpeed)
        {
            mCurrentSpeed = mMaxSpeed;
            mIsChargeFiver = true; // �t�B�[�o�[�Q�[�W�������񂷂邩�ǂ���
        }
        else // �ő呬�x
        {
            mIsChargeFiver = false;
        }

        // ���������Z
        mRunLength += mCurrentSpeed * Time.deltaTime;
    }

    void Fiver() // �t�B�[�o�[���̍X�V�֐� 
    {
        mFiverGauge -= Time.deltaTime * mFiverGaugeReduceSpeed;
        // �Ȃ��Ȃ�����t�B�[�o�[�I���
        if (mFiverGauge <= 0.0f)
        {
            mIsFiver = false;
            // ���x���K��l�ɖ߂�
            mCurrentSpeed = mBaseSpeed;
        }
    }

    void UnFiver() // �t�B�[�o�[�łȂ��Ƃ��̍X�V�֐�
    {
        if (!mIsChargeFiver)
        {
            return;
        }

        // �t�B�[�o�[�Q�[�W�����Z����
        mFiverGauge += Time.deltaTime * mFiverGaugeSpeed;

        // �Q�[�W���ő�l�𒴂�����t�B�[�o�[�ɓ˓�����
        if (mFiverGauge >= mMaxFiverGauge)
        {
            mIsFiver = true;
        }
    }

    void UpdateFiver() // �t�B�[�o�[�Q�[�W���X�V����
    {
        if (!mIsFiver) // �t�B�[�o�[���ȊO�̏���
        {
            UnFiver();
        }
        else
        {
            Fiver();
        }

        mCamEffect.feverMode = mIsFiver;
        // �t�B�[�o�[�Q�[�W���N�����v����
        mFiverGauge = Math.Clamp(mFiverGauge, 0.0f, mMaxFiverGauge);
    }

    // ----------------------------- �O������Ăяo���Ă��炤�֐� ----------------------------
    public void Damaged(int Damage_) // �_���[�W���󂯂�֐�
    {
        mHp -= Damage_; // �̗͂����炷
        mCurrentSpeed = mBaseSpeed; // ���x��������
    }

    public void GetResource(int Exp_) // �������擾����֐�(���� ���Z����o���l)
    {
        // �o���l�����Z����
        mCurrentExp += Exp_;
        // �ő�l���I�[�o�[�����烌�x�����㏸�����Čo���l��������
        if (mCurrentExp >= mMaxExp)
        {
            mCurrentLv++; // ���x�������Z
            // �ő僌�x���ŃN�����v
            mCurrentLv = Math.Clamp(mCurrentLv, 0, mMaxLv); 
            mCurrentExp -= mMaxExp; // �o���l�����̐��������Ƃ�
            mMaxExp += mMaxExpAddition; // �ő�o���l�����Z����
            fChangeMesh(); //���b�V����؂�ւ���
        }
    }


    // -------------------------------- �����蔻��̊֐� -------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            _isDeath = true;
        }
    }

    // -------------------------------- �\���؂�ւ��֐� -------------------------------
    void fChangeMesh()
    {
        foreach (var mesh in mMesh)
        {
            mesh.SetActive(false);
        }
        // ���x���ɉ��������b�V���\����؂�ւ�
        if (mMesh.Length >= mCurrentLv)
        {
            mMesh[mCurrentLv - 1].SetActive(true);
        }
    }
}
