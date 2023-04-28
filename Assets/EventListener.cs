using UnityEngine;

public static class EventDelegate
{
	public delegate void OnClockCreated();
	public static OnClockCreated ClockCreatedEvent;

	public static void FireClockCreated()
	{
		ClockCreatedEvent?.Invoke();
	}
}

// Bonus method for events
public class EventListener : MonoBehaviour
{
	private AudioSource _audioSource;
	[SerializeField] private AudioClip _audioClip;

	private void OnEnable()
	{
		EventDelegate.ClockCreatedEvent += OnClockCreated;
	}

	private void OnDisable()
	{
		EventDelegate.ClockCreatedEvent -= OnClockCreated;
	}
	
	void Start()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	private void OnClockCreated()
	{
		_audioSource.PlayOneShot(_audioClip);
	}
}