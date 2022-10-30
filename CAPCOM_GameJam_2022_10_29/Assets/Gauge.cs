using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    PlayerController _playerController;
    [SerializeField]
    float _value;
    [SerializeField]
    float _a;
    [SerializeField] Image _gauge;

    void Start()
    {

        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        
    }


    void Update()
    {
        if(!_playerController)
        {
            _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        }
        if( _playerController != null )
        {
            _value = _playerController.mMaxFiverGauge;
            _a = _playerController.mFiverGauge;

            _gauge.fillAmount = _a / _value;
        }

    }

}
