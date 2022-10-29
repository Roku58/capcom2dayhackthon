using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    //シーンの読み込み
    public void LoadScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name, LoadSceneMode.Single);
    }
}
