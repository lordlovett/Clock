using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockCreator : MonoBehaviour
{
	[SerializeField] private Clock _clockPrefab;

	[SerializeField] private uint _numberOfClocks = 1;

	private int ClockCount => (int)_numberOfClocks;

	private int _area1D;
	private Vector3 _offset = Vector3.zero;

	void Start()
	{
		_area1D = Mathf.CeilToInt((float)ClockCount / 2);

		_offset.x -= _area1D / 2f;
		_offset.y += _area1D / 2f;

		transform.localPosition = _offset;
		
		CreateClocks(ClockCount);
	}

	private void CreateClocks(int count)
	{
		int xPos = 1, yPos = -1, x = 0, y = 0;
		
		for (int i = 0; i < count; i++)
		{
			Clock clock = Instantiate(_clockPrefab, transform);
			Vector3 position = new Vector3(xPos * x, yPos * y, 0);
			clock.transform.localPosition = position;

			x++;
			if (x >= _area1D)
			{
				x = 0;
				y++;
			}
		}
	}
}