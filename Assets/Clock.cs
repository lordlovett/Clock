using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

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

	[SerializeField] private Vector3 _hourHandScale;
	[SerializeField] private Vector3 _minuteHandScale;
	[SerializeField] private ClockHand[] _handPrefabs;
	[SerializeField] private Color[] _colours;
	
	private void Start()
	{
		CreateHands();
		
		_crossoverEvent.AddListener(OnCrossOver);
		SetHands(DateTime.Now);
	}

	private void Update()
	{
		SetHands(DateTime.Now);
	}

	private void CreateHands()
	{
		int hourHand = Random.Range(0, _handPrefabs.Length);
		int hourColour = Random.Range(0, _colours.Length);
		ClockHand hour = Instantiate(_handPrefabs[hourHand], _hourPivot);
		hour.SetColour(_colours[hourColour]);
		hour.transform.localScale = _hourHandScale;
		hour.transform.localPosition = new Vector3(0, 0, -0.1f);
		
		int minuteHand = Random.Range(0, _handPrefabs.Length);
		int minuteColour = Random.Range(0, _colours.Length);
		ClockHand minute = Instantiate(_handPrefabs[minuteHand], _minutePivot);
		minute.SetColour(_colours[minuteColour]);
		minute.transform.localScale = _minuteHandScale;
		minute.transform.localPosition = new Vector3(0, 0, -0.15f);
	}

	private void SetHands(DateTime time)
	{
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