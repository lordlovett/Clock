using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
	[SerializeField] private Transform _hourPivot;
	[SerializeField] private Transform _minutePivot;

	private float _degPerHour = 30f;
	private float _degPerMinute = 6f;

	private int _minutePerHour = 60;
	private int _secondPerMinute = 60;

	void Start()
	{
		SetHands(DateTime.Now);
	}

	void Update()
	{
		SetHands(DateTime.Now);
	}

	void SetHands(DateTime time)
	{
		float degHour = _degPerHour * time.Hour + _degPerHour / _minutePerHour * time.Minute;
		float degMinute = (_degPerMinute * time.Minute) + (_degPerMinute / _secondPerMinute) * time.Second ;
		
		_hourPivot.localRotation = Quaternion.Euler(0, degHour, 0);
		_minutePivot.localRotation = Quaternion.Euler(0, degMinute, 0);
	}
}