using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[CreateAssetMenu(fileName = "New Lighting Preset", menuName = "Lighting Preset",order =1)]
public class LightingPresetScript : ScriptableObject
{
    public Gradient Ambientcolor;
    public Gradient DirectionalColor;
    public Gradient FogColor;

   
}
