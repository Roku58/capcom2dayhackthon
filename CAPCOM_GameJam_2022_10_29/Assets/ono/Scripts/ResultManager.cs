using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private Text length_text;

    public List<GameObject> not_opens = new List<GameObject>();
    public List<GameObject> opens = new List<GameObject>();

    GameObject TitleButton, ReStartButton;

    private float length = 850;

    [SerializeField] int split = 100, land_max = 50;

    [SerializeField] GameObject open_pref, not_open_pref, player;


    public void Awake()
    {
        length_text = GameObject.Find("Area_Num_Text").GetComponent<Text>();
        TitleButton = GameObject.Find("TitleButton"); TitleButton.SetActive(false);
        ReStartButton = GameObject.Find("ReStartButton"); ReStartButton.SetActive(false);
        length = GameManager.mPlayerMoveLength;
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

        for (int i = 0; i < land_max; i++)
        {
            GameObject obj = Instantiate(not_open_pref, ParentObject.transform);
            Vector3 pos = new Vector3(-25, 0, 0);
            pos.x += 5 * i;
            obj.transform.localPosition = pos;
            not_opens.Add(obj);
        }

        ParentObject = GameObject.Find("open");

        for (int i = 0; i < land_max; i++)
        {
            GameObject obj = Instantiate(open_pref, ParentObject.transform);
            Vector3 pos = new Vector3(-25, 0, 0);
            pos.x += 5 * i;
            obj.transform.localPosition = pos;
            opens.Add(obj);
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
        if (num > land_max) num = land_max;

        for (int i = 0; i < num; i++)
        {
            opens[i].SetActive(true);
            not_opens[i].SetActive(false);
        }
    }

    //テキストの設定
    public void SetText()
    {
        length_text.text = "0m²";
    }

    //アニメーション
    public void Animation()
    {
        float pos = -40.0f;
        float ease_time = 3.0f, delay = 1.0f;
        int num = (int)(length / (split));
        if (num > land_max) num = land_max;

        pos += num * 5.0f + 10.0f;

        ease_time += num * 0.02f;

        Camera.main.transform.position = new Vector3(-50.0f, Camera.main.transform.position.y, Camera.main.transform.position.z);
        Camera.main.transform.DOLocalMove(new Vector3(pos, -20, -20.5f), ease_time).SetDelay(delay).SetEase(Ease.OutQuint).OnComplete(() =>
        {
            ReStartButton.SetActive(true);
            TitleButton.SetActive(true);
        });

        player.transform.DOLocalMove(new Vector3(pos, -24.5f, -10), ease_time).SetDelay(delay).SetEase(Ease.OutQuint);



        DOVirtual.Float(0f, length, ease_time, value =>
        {
            length_text.text = value.ToString("f0")+"m²";
        }).SetDelay(delay).SetEase(Ease.OutQuint);

    }
}
