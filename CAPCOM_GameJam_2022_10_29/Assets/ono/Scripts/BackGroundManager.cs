using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    //���[���̒���
    public float LineLength = 15.0f;

    //���[���v�f
    public Scroll lane_0,lane_1;

    public void Start()
    {
        lane_0.setup(LineLength, 0);
        lane_1.setup(LineLength, LineLength);
    }
}
