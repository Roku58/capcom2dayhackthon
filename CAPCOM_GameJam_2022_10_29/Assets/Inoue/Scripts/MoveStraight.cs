using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStraight : MonoBehaviour
{
    public Vector3 MoveVector;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(MoveVector * Speed*Time.deltaTime);
    }
}
