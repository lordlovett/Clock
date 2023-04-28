using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClockCreator : MonoBehaviour
{
	[SerializeField] private Clock _clockPrefab;
	[SerializeField] private uint _numberOfClocks = 1;
	
	private AudioSource _audioSource;
	[SerializeField] private AudioClip _audioClip;

	private int ClockCount => (int)_numberOfClocks;

	[SerializeField]
	private int _area1D;
	private Vector3 _offset = Vector3.zero;

	private readonly int _baseXPos = 1, _baseYPos = -1;

	private List<Clock> _clocks = new List<Clock>();

	private UnityEvent _audioEvent = new UnityEvent();
	
	void Start()
	{
		_audioSource = GetComponent<AudioSource>();
		_audioEvent.AddListener(PlayAudioClip);
		
		SetPosition();
		
		CreateClocks(ClockCount);
	}

	private void CreateClocks(int count)
	{
		int xMul = 0, yMul = 0;
		
		for (int i = 0; i < count; i++)
		{
			Clock clock;
			if (i < _clocks.Count)
			{
				clock = _clocks[i];
			}
			else
			{
				clock = Instantiate(_clockPrefab, transform);
				_clocks.Add(clock);
			}
			
			Vector3 position = new Vector3(_baseXPos * xMul, _baseYPos * yMul, 0);
			clock.transform.localPosition = position;

			xMul++;
			if (xMul >= _area1D)
			{
				xMul = 0;
				yMul++;
			}
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			//Debug.Log("Spacebar");
			// Add a Clock
			_numberOfClocks++;
			SetPosition();
			CreateClocks(ClockCount);
			
			_audioEvent?.Invoke();
			EventDelegate.FireClockCreated();
		}
	}

	private void SetPosition()
	{
		_area1D = Mathf.CeilToInt(Mathf.Sqrt(ClockCount));
		_offset = Vector3.zero;
		_offset.x -= _area1D / 2f - 0.5f;
		_offset.y += _area1D / 2f - 0.5f;

		transform.localPosition = _offset;
	}

	private void PlayAudioClip()
	{
		_audioSource.PlayOneShot(_audioClip);
	}
}