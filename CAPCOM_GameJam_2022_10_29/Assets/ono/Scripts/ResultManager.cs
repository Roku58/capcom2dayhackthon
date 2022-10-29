using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private Text length_text;

    public List<GameObject> not_opens = new List<GameObject>();
    public List<GameObject> opens = new List<GameObject>();

    private float length = 200;
    [SerializeField] int split = 100;

    public void Awake()
    {
        length_text = GameObject.Find("Area_Num_Text").GetComponent<Text>();
    }

    public void Start()
    {
        SetList();
        CheckLength();
    }

    //�q�v�f�̎擾�ƃ��X�g�Ɋi�[
    void SetList()
    {
        GameObject ParentObject;

        ParentObject = GameObject.Find("not_open");

        for (int i = 0; i < ParentObject.transform.childCount; i++)
        {
            not_opens.Add(ParentObject.transform.GetChild(i).gameObject);
        }

        ParentObject = GameObject.Find("open");

        for (int i = 0; i < ParentObject.transform.childCount; i++)
        {
            opens.Add(ParentObject.transform.GetChild(i).gameObject);
        }
    }

    //�����ɉ����ĉ��o
    void CheckLength()
    {
        //�J���̓y�n�����ׂĔ�\���ɂ���
        foreach (var open in opens)
        {
            open.SetActive(false);
        }

        int num = (int)(length  / (split));

        for (int i = 0; i < num; i++)
        {
            opens[i].SetActive(true);
            not_opens[i].SetActive(false);
        }
    }
}
