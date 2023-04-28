using System;
using UnityEngine;
using UnityEngine.Events;

public class Clock : MonoBehaviour
{
	[SerializeField] private Transform _hourPivot;
	[SerializeField] private Transform _minutePivot;

	private float _degPerHour = 30f;
	private float _degPerMinute = 6f;

	private int _minutePerHour = 60;
	private int _secondPerMinute = 60;
	
	private UnityEvent _crossoverEvent = new UnityEvent();
	private bool _crossoverInvoked = false;
	
	void Start()
	{
		_crossoverEvent.AddListener(OnCrossOver);
		SetHands(DateTime.Now);
	}

	void Update()
	{
		SetHands(DateTime.Now);
	}

	void SetHands(DateTime time)
	{
		time = time.AddHours(1);
		float degHour = _degPerHour * time.Hour + _degPerHour / _minutePerHour * time.Minute;
		float degMinute = (_degPerMinute * time.Minute) + (_degPerMinute / _secondPerMinute) * time.Second ;
		
		_hourPivot.localRotation = Quaternion.Euler(0, degHour, 0);
		_minutePivot.localRotation = Quaternion.Euler(0, degMinute, 0);

		float delta = degHour - degMinute;
		if (delta > 0f && delta < Mathf.Epsilon)
		{
			if (!_crossoverInvoked)
			{
				_crossoverEvent?.Invoke();
			}
		}
		// Avoid invoking every frame while minute hand crosses over hour hand
		if (_crossoverInvoked && delta < 0.5f)
		{
			_crossoverInvoked = false;
		}
	}

	private void OnCrossOver()
	{
		Debug.LogFormat("Minute Hand passed over Hour Hand.");
		_crossoverInvoked = true;
	}
}