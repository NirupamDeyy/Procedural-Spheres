﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NoiseFilter 
{

    public NoiseSettings settings; 
    Noise noise = new Noise();
  

    public NoiseFilter(NoiseSettings settings)
    {
        this.settings = settings;
    }
 
    public float Evaluate(Vector3 point)
    {
        float noiseValue = ((noise.Evaluate(point * settings.roughness + settings.centre) + 1) * 0.5f) * settings.strength;
        return noiseValue ;
       
    }
}
