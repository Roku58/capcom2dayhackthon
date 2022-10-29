using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CamEffect : MonoBehaviour
{
    public bool feverMode;
    public PostProcessVolume PPV;
    public Vignette vignette;
    // Start is called before the first frame update
    void Start()
    {
        vignette = ScriptableObject.CreateInstance<Vignette>();
        SetVolume();
    }

    // Update is called once per frame
    void Update()
    {
        if (feverMode)
        {
            vignette.enabled.Override(true);
            vignette.intensity.Override(Mathf.MoveTowards(vignette.intensity.value, 1, Time.deltaTime));
        }
        else
        {
            vignette.enabled.Override(true);
            vignette.intensity.Override(Mathf.MoveTowards(vignette.intensity.value, 0, Time.deltaTime));
        }
        PPV = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, vignette);
    }
    void SetVolume()
    {
        vignette = ScriptableObject.CreateInstance<Vignette>();
    }
}
