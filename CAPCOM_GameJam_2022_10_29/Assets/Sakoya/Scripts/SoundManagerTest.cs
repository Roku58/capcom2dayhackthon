using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SoundManager.Instance.PlayBGM(0);
            Log.Info(GetType(), "A");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SoundManager.Instance.StopBGM();
            Log.Info(GetType(), "S");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SoundManager.Instance.PlaySE(0);
            Log.Info(GetType(), "Space");
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            SoundManager.Instance.PlaySE(1);
            Log.Info(GetType(), "Space");
        }
    }
}
