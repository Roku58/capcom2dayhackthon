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
    private float t;
    public float Intensity;
    public float Speed;
    public bool hit;
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
            t += Time.deltaTime*Speed;
            vignette.enabled.Override(true);
            vignette.intensity.Override(Mathf.Abs(Mathf.Sin(t))*Intensity);
        }
        else
        {
            vignette.enabled.Override(true);
            vignette.intensity.Override(Mathf.MoveTowards(vignette.intensity.value, 0, Time.deltaTime));
        }
        PPV = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, vignette);
        if (hit)
            StartCoroutine("Hitstop");
    }
    void SetVolume()
    {
        vignette = ScriptableObject.CreateInstance<Vignette>();
    }
    public IEnumerator Hitstop()
    {
        if (this)
        {
            Time.timeScale = 0.2f;
            yield return new WaitForSeconds(0.2f);
            Time.timeScale = Mathf.MoveTowards(Time.timeScale, 1, Time.unscaledDeltaTime);
            hit = false;
        }
    }
}
