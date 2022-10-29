using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ステージにオブジェクトを生成するクラス
/// </summary>
public class ObjectGeneration : MonoBehaviour
{
    [SerializeField, Header("生成頻度")]
    float _intervalTime;

    [SerializeField, Header("経過時間　確認用なので設定不要")]
    float _elapsedTime;

    [SerializeField, Header("生成ポイント 空のオブジェクトをドラッグ")]
    Transform[] _generationPoints;

    [SerializeField, Header("生成するオブジェクト")]
    GameObject[] _generationObjects;

    void Start()
    {
        
    }

    void Update()
    {
        _elapsedTime　+= Time.deltaTime;
        ObjectSpown(_intervalTime);
    }

    void ObjectSpown(float second)
    {
        if(_elapsedTime > _intervalTime)
        {
            var rdmPos = Random.Range(0, _generationPoints.Length);
            var rdmObj = Random.Range(0, _generationObjects.Length);

            var obj = Instantiate(_generationObjects[rdmObj]);
            obj.gameObject.transform.position = _generationPoints[rdmPos].position;
            _elapsedTime = 0;
        }
    }
}
