using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorUI : MonoBehaviour
{
    public ShapeSettings settings;
    public Slider radiusSlider;

    public ShapeSettings shapeSettings;
    public ColourSettings colourSettings;
    //

    public void OnSliderValueChangedForRadius(float value)
    {
        settings.planetRadius = radiusSlider.value;
    }




    NoiseFilter noiseFilter;

    
}
