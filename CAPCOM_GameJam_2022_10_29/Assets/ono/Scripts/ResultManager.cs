using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private Text length_text, lv_text;

    public List<GameObject> not_opens = new List<GameObject>();
    public List<GameObject> opens = new List<GameObject>();

    GameObject TitleButton, ReStartButton;

    private float length = 850;
    private int lv = 1;

    [SerializeField] int split = 100;

    public void Awake()
    {
        length_text = GameObject.Find("Area_Num_Text").GetComponent<Text>();
        lv_text = GameObject.Find("Lv_Num_Text").GetComponent<Text>();
        TitleButton = GameObject.Find("TitleButton"); TitleButton.SetActive(false);
        ReStartButton = GameObject.Find("ReStartButton"); ReStartButton.SetActive(false);
    }

    public void Start()
    {
        SetList();
        CheckLength();
        SetText();
        Animation();
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

    //テキストの設定
    public void SetText()
    {
        lv_text.text = lv.ToString();
        length_text.text = "0m²";
    }

    //アニメーション
    public void Animation()
    {
        float pos = -40.0f;

        int num = (int)(length / (split));

        pos += num * 5.0f + 10.0f;

        Camera.main.transform.position = new Vector3(-50.0f, Camera.main.transform.position.y, Camera.main.transform.position.z);
        Camera.main.transform.DOLocalMove(new Vector3(pos, -20, -20.5f), 2.0f).SetDelay(1.0f).OnComplete(() =>
        {
            ReStartButton.SetActive(true);
            TitleButton.SetActive(true);
        }); ;

        DOVirtual.Float(0f, length, 2.0f, value =>
        {
            length_text.text = value.ToString("f0")+"m²";
        }).SetDelay(1.0f);

    }
}
