using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPulseManager : MonoBehaviour
{
    public Material material;
    public Color startColor;
    public Color endColor;
    public float pulseSpeed = 1f;

    private Color originalEmissionColor;

    private bool isPulsating = true;

    public void StartPulsating()
    {
        // Ensure the material has emission enabled
        material.EnableKeyword("_EMISSION");

        // Store the original emission color
        originalEmissionColor = material.GetColor("_EmissionColor");

        // Start the pulsating coroutine
        if (!isPulsating)
        {
            isPulsating = true;
            StartCoroutine(PulseEmissionCoroutine());
        }
    }

    public void StopPulsating()
    {
        material.SetColor("_EmissionColor", originalEmissionColor);

        // Disable emission to stop the pulsating effect
        material.DisableKeyword("_EMISSION");

        // Set isPulsating to false to stop the coroutine
        isPulsating = false;
    }

    private IEnumerator PulseEmissionCoroutine()
    {
        while (isPulsating)
        {
            // Calculate the pulsating emission color based on time
            float t = Mathf.PingPong(Time.time * pulseSpeed, 1f);
            Color lerpedColor = Color.Lerp(startColor, endColor, t);

            // Set the emission color of the material
            material.SetColor("_EmissionColor", lerpedColor);

            yield return null;
        }
    }
}
