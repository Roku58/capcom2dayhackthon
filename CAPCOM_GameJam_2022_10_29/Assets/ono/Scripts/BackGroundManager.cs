using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    //レーンの長さ
    public float start, dead;

    //レーン要素
    public List<Scroll> lanes;

    public void Start()
    {
        foreach (var v in lanes)
        {
            v.setup(start, dead);
        }
    }
}
