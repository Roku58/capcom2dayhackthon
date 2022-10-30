using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    PlayerController _player;

    [SerializeField] Text LV_text, HP_text, EXP_text, HPMAX_text, EXPMAX_text, SPEED_text;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        textUpdate();
    }

    void textUpdate()
    {
        LV_text.text = _player.mCurrentLv.ToString();
        SPEED_text.text = _player.mCurrentSpeed.ToString();    
    }
}
