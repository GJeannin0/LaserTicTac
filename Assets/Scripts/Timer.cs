using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
	[SerializeField] private float duration;
	private float currentTime;

	private bool running = false;
	private bool ready = false;
	private bool automatic = false;

	[SerializeField] public delegate void CallResult();
	public CallResult callResult;

	void Update()
    {
		if (running)
		{
			if (!ready)
			{
				currentTime -= Time.deltaTime;
				if (currentTime <= 0)
				{
					if (automatic)
					{
						callResult();
						UseReady();
					}
					else
						ready = true;
					currentTime = duration;
				}
			}
		}
    }

	public bool GetReady()
	{
		return ready;
	}

	public void UseReady()
	{
		if (ready)
		{
			ready = false;
			currentTime = duration;
		}
	}

	public float GetRemainingTime()
	{
		return currentTime;
	}

	public void SetDuration(float setDuration)
	{
		duration = setDuration;
	}

	public void Enable(bool active)
	{
		running = active;
	}

	public void TimerSetUp(bool startsReady, bool isAutomatic, float setDuration, CallResult result)
	{
		duration = setDuration;
		automatic = isAutomatic;
		if (!startsReady)
		{
			currentTime = duration;
		}
		else
		{
			currentTime = 0;
			ready = startsReady;
		}

		callResult = result;
	}
}
