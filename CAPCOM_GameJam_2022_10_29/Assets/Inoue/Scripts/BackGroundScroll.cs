using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackGroundScroll : MonoBehaviour
{
    [SerializeField]
    private GameObject BackGroundObj;
    public float ScrollSpeed;
    public Vector3 ScrollVector;
    public float DisappearTime;
    public GameManager GM;
    public Transform Spawn;
    public float t;
    public float objLength;
    public float CreateTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        // ScrollSpeed = GM.mPlayerSpeed;
        CreateTime = objLength / ScrollSpeed;
        if (t >= CreateTime)
        {
            Create();
            t = 0;
        }

        
    }
    void Create()
    {
        GameObject Obj = GameObject.Instantiate(BackGroundObj);
        Obj.transform.position = Spawn.transform.position;
        Obj.transform.rotation = Spawn.transform.rotation;
        Obj.AddComponent<MoveStraight>();
        Obj.GetComponent<MoveStraight>().MoveVector = ScrollVector;
        Obj.GetComponent<MoveStraight>().Speed = ScrollSpeed;
        Destroy(Obj, DisappearTime);

    }
}
