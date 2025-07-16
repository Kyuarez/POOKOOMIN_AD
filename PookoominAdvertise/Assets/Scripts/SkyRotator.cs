using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class SkyRotator : MonoBehaviour
{
    [SerializeField] private Volume _volume;
    [SerializeField] private float rotationSpeed = 1f;

    private HDRISky _hdriSky;

    private void Awake()
    {
        if (_volume.profile.TryGet<HDRISky>(out var hdriSky))
        {
            _hdriSky = hdriSky;
        }
        else
        {
            Debug.LogWarning("HDRISky component not found in VolumeProfile.");
        }
    }

    private void Update()
    {
        float currentRotation = _hdriSky.rotation.value;
        currentRotation += rotationSpeed * Time.deltaTime;
        if (currentRotation > 360f)
        {
            currentRotation -= 360f;
        }

        _hdriSky.rotation.value = currentRotation;
    }
}
