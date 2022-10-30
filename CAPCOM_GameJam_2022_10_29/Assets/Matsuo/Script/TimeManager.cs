using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{

	//　Time.timeScaleに設定する値
	[SerializeField]
	 float timeScale = 0.1f;
	//　時間を遅くしている時間
	[SerializeField]
	 float slowTime = 1f;
	//　経過時間
	 float elapsedTime = 0f;
	//　時間を遅くしているかどうか
	 bool isSlowDown = false;
	bool _isGameOver = false;
	bool _isGameEnd = false;

	void Update()
	{
		//　スローダウンフラグがtrueの時は時間計測
		if (isSlowDown)
		{
			elapsedTime += Time.unscaledDeltaTime;
			if (elapsedTime >= slowTime)
			{
				SetNormalTime();
			}
		}
		if(_isGameEnd)
        {
			End();
        }
	}
	/// <summary>
	/// 時間を遅らせる処理(ヒットストップ)
	/// </summary>
	public void SlowDown()
	{
		_isGameOver = true;
		Debug.Log("SlowDown");
		elapsedTime = 0f;
		Time.timeScale = timeScale;
		isSlowDown = true;
		Debug.Log("SlowDown" + Time.timeScale);

	}
	/// <summary>
	/// 時間を元に戻す処理
	/// </summary>
	public void SetNormalTime()
	{
		Time.timeScale = 1f;
		isSlowDown = false;
		_isGameEnd = true;
	}
	void End()
    {
		Debug.Log("GameOver");
		SceneManager.LoadScene("Result");
	}
}