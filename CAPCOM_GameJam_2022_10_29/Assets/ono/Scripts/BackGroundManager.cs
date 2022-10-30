using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    //���[���̒���
    public float start, dead;

    //���[���v�f
    public List<Scroll> lanes;

    public void Start()
    {
        foreach (var v in lanes)
        {
            v.setup(start, dead);
        }
    }
}
