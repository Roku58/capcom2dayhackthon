using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    private AudioSource _AudioSource;

    [SerializeField]
    private List<AudioClip> _BGMClips;

    [SerializeField]
    private List<AudioClip> _SEClips;

    public void PlayBGM(uint index)
    {
#if UNITY_EDITOR
        var clipCount = _BGMClips.Count;
        if (clipCount <= index)
        {
            Log.Error(GetType(), $"“o˜^‚³‚ê‚Ä‚¢‚éClip‚Ì”‚ð’´‚¦‚Ä‚¢‚éˆ×ABGM‚ÌÄ¶‚ÉŽ¸”s‚µ‚Ü‚µ‚½BIndex : {index} Clip Count:{clipCount}");
            return;
        }
#endif // UNITY_EDITOR

        var clip = _BGMClips[(int)index];

#if UNITY_EDITOR
        if (clip == null)
        {
            Log.Error(GetType(), $"{nameof(_BGMClips)}‚ÉClip‚ª“o˜^‚³‚ê‚Ä‚¢‚È‚¢ˆ×A BGM‚ÌÄ¶‚ÉŽ¸”s‚µ‚Ü‚µ‚½BIndex : {index}");
            return;
        }
#endif // UNITY_EDITOR

        _AudioSource.loop = true;
        _AudioSource.clip = clip;
        _AudioSource.Play();
    }

    public void StopBGM()
    {
        _AudioSource.loop = false;
        _AudioSource.clip = null;
        _AudioSource.Stop();
    }

    public void PlaySE(uint index)
    {
#if UNITY_EDITOR
        var clipCount = _SEClips.Count;
        if (clipCount <= index)
        {
            Log.Error(GetType(), $"“o˜^‚³‚ê‚Ä‚¢‚éClip‚Ì”‚ð’´‚¦‚Ä‚¢‚éˆ×ASE‚ÌÄ¶‚ÉŽ¸”s‚µ‚Ü‚µ‚½BIndex : {index} Clip Count:{clipCount}");
            return;
        }
#endif // UNITY_EDITOR

        var clip = _SEClips[(int)index];

#if UNITY_EDITOR
        if (clip == null)
        {
            Log.Error(GetType(), $"{nameof(_SEClips)}‚ÉClip‚ª“o˜^‚³‚ê‚Ä‚¢‚È‚¢ˆ×A SE‚ÌÄ¶‚ÉŽ¸”s‚µ‚Ü‚µ‚½BIndex : {index}");
            return;
        }
#endif // UNITY_EDITOR

        _AudioSource.PlayOneShot(clip);
    }

    // Start is called before the first frame update
    void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
