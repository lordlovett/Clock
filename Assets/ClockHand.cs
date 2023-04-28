using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHand : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    public void SetColour(Color colour)
    {
        _renderer.material.color = colour;
    }
}
