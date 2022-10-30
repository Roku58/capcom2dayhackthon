using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//このコードはBackGroundScroll.csにより自動的にAddComponentされるので、Inspector上での実装は不要!
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
