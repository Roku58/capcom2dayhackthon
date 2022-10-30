using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    //ƒŒ[ƒ“‚Ì’·‚³
    public float start, dead;

    //ƒŒ[ƒ“—v‘f
    public List<Scroll> lanes;

    public void Start()
    {
        foreach (var v in lanes)
        {
            v.setup(start, dead);
        }
    }
}
