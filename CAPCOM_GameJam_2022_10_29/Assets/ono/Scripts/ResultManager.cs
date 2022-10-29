using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private Text length_text, lv_text;

    public List<GameObject> not_opens = new List<GameObject>();
    public List<GameObject> opens = new List<GameObject>();

    private float length = 200;
    private int lv = 1;

    [SerializeField] int split = 100;

    public void Awake()
    {
        length_text = GameObject.Find("Area_Num_Text").GetComponent<Text>();
        lv_text = GameObject.Find("Lv_Num_Text").GetComponent<Text>();
    }

    public void Start()
    {
        SetList();
        CheckLength();
        SetText();
    }

    //子要素の取得とリストに格納
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

    //長さに応じて演出
    void CheckLength()
    {
        //開拓後の土地をすべて非表示にする
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

    public void SetText()
    {
        lv_text.text = lv.ToString();
        length_text.text = length.ToString()+"m²";
    }
}
