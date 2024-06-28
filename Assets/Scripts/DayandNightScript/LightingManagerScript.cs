using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using LightType = UnityEngine.LightType;
using RenderSettings = UnityEngine.RenderSettings;

//it can make it edited while not playing the game but also be able to see the changes
[ExecuteAlways]

public class LightingManagerScript : MonoBehaviour
{
    [SerializeField]
    private Light DirectionalLight;
    [SerializeField]
    private LightingPresetScript preset;
    [SerializeField, Range(8, 24)]
    private float DayTime = 12;  // Starting at 12 (noon) to simulate starting with day

    private const float dayDurationInSeconds = 1800f;  // 30 minutes * 60 seconds

    private void Update()
    {
        if (preset == null)
        {
            return;
        }

        if (Application.isPlaying)
        {
            DayTime += (Time.deltaTime / dayDurationInSeconds) * 24f; // Adjust time based on 30-minute day
            DayTime %= 24; // Ensure time stays within 0-24 hours
            UpdateLighting(DayTime / 24f);
        }
        else
        {
            UpdateLighting(DayTime / 24f);
        }
    }

    private void UpdateLighting(float timePercentage)
    {
        RenderSettings.ambientLight = preset.Ambientcolor.Evaluate(timePercentage);
        RenderSettings.fogColor = preset.FogColor.Evaluate(timePercentage);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = preset.DirectionalColor.Evaluate(timePercentage);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercentage * 360f) - 90f, -170, 0));
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight != null)
        {
            return;
        }

        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

}
