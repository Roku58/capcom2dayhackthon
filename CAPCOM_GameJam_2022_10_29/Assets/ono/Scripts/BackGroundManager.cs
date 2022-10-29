using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    //レーンの長さ
    public float LineLength = 15.0f;

    //レーン要素
    public Scroll lane_0,lane_1;

    public void Start()
    {
        lane_0.setup(LineLength, 0);
        lane_1.setup(LineLength, LineLength);
    }
}
