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
    [SerializeField, Range(0,80)]
    private float DayTime;

    private void Update()
    {
        //to set the angle

        if(preset == null)
        {
            return;
            //if its not assigned
        }

        //setting it if the time is playing

        if(Application.isPlaying) 
        {
            DayTime += Time.deltaTime;
            DayTime %= 80;
            //setting it between 0-24
            UpdateLighting(DayTime / 80f);
            
        }
        else
        {

            UpdateLighting(DayTime / 24f);
           
        }

    }
    private void UpdateLighting(float timeper)
        {

         RenderSettings.ambientLight = preset.Ambientcolor.Evaluate(timeper);
         RenderSettings.fogColor = preset.FogColor.Evaluate(timeper);

        //to check we got the light or not then color and rotation set

        if(DirectionalLight != null) 
        {
            DirectionalLight.color = preset.DirectionalColor.Evaluate(timeper);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timeper * 360f) - 90f, -170, 0));
        }
    }
    private void OnValidate()
    {
        if (DirectionalLight != null)
        {
            return;
        }
        //to find the first light in the scene we can get

        if (RenderSettings.sun!=null) 
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights)
            {
                if(light.type == LightType.Directional) 
                {
                 DirectionalLight = light;
                    return;
                }
            }
        }
    }
   
}
