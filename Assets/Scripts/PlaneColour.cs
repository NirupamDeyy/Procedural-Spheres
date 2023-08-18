 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneColour : MonoBehaviour
{
    public Color planeColour = Color.black;
    public ColourPicker CC;
   

    public void GeneratePlaneColour()
    {
        
        GetComponent<Renderer>().material.color = CC.updatedColor;
    }
    
}
