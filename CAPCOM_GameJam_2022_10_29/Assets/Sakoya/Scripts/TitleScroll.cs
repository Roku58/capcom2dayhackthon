using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScroll : MonoBehaviour
{
    private const int ScrollObjectCount = 8;
    private static readonly Vector3 StartScrollPosition = new Vector3(-20, -1.5f, 3);
    private static readonly Vector3 ScrollInterval = new Vector3(5.0f, 0.0f, 0.0f);
    private static readonly Vector3 DeadScrollPosition = new Vector3(-25, -1.5f, 3);

    [SerializeField]
    private GameObject _ScrollTarget;

    [SerializeField]
    private float _ScrollSpeed = 0.5f;

    private GameObject[] _ScrollObjectArray;

    // Start is called before the first frame update
    void Start()
    {
        _ScrollObjectArray = new GameObject[ScrollObjectCount];

        for (int i = 0; i < ScrollObjectCount; i++)
        {
            _ScrollObjectArray[i] = Instantiate(_ScrollTarget);
            _ScrollObjectArray[i].transform.position = CalculateStartScrollPosition(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ScrollObjectCount; i++)
        {
            var scrollObject = _ScrollObjectArray[i];
            var position = _ScrollObjectArray[i].transform.position;

            position.x -= _ScrollSpeed * Time.deltaTime;

            _ScrollObjectArray[i].transform.position = position;

            if (position.x >= DeadScrollPosition.x)
            {
                continue;
            }

            Destroy(scrollObject);
            _ScrollObjectArray[i] = Instantiate(_ScrollTarget);
            _ScrollObjectArray[i].transform.position = CalculateStartScrollPosition(ScrollObjectCount - 1);
        }
    }

    private Vector3 CalculateStartScrollPosition(int value)
    {
        return StartScrollPosition + ScrollInterval * value;
    }
}
