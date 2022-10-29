using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    //ƒV[ƒ“‚Ì“Ç‚İ‚İ
    public void LoadScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name, LoadSceneMode.Single);
    }
}
