using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    //ƒŒ[ƒ“‚Ì’·‚³
    public float LineLength = 15.0f;

    //ƒŒ[ƒ“—v‘f
    public Scroll lane_0,lane_1;

    public void Start()
    {
        lane_0.setup(LineLength, 0);
        lane_1.setup(LineLength, LineLength);
    }
}
