using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���̃R�[�h��BackGroundScroll.cs�ɂ�莩���I��AddComponent�����̂ŁAInspector��ł̎����͕s�v!
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
