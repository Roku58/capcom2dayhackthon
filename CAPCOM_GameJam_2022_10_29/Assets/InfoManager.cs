using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    public PlayerController _player;

    [SerializeField] Text LV_text, EXP_text, EXPMAX_text, SPEED_text;

    void Update()
    {
        if(_player == null) _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        textUpdate();
    }

    void textUpdate()
    {
        LV_text.text = _player.mCurrentLv.ToString();
        SPEED_text.text = _player.mCurrentSpeed.ToString();
        EXP_text.text = _player.mCurrentExp.ToString();
        EXPMAX_text.text = _player.mMaxExp.ToString();
    }
}
