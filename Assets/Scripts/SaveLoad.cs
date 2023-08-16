using System;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public Planet planet;
    public AssignDeleteButton assignDeleteButton;
    
    public int loadInt = 2;
    public void Awake()
    {
        SaveSystem.Init();
        SaveSystem.StartSave();

    }
    
    public void Save()
    {
        int resolution = planet.resolution;
        float roughness = planet.shapeSettings.noiseSettings.roughness;
        float strength = planet.shapeSettings.noiseSettings.strength;
        float radius = planet.shapeSettings.planetRadius;
        bool move = planet.move;
        float speed = planet.speed;
        string itemName = assignDeleteButton.itemName;
        

        SaveObject saveObject = new SaveObject { resolution = resolution, roughness = roughness, strength = strength, radius = radius, move = move, speed = speed, itemName= itemName,};

        string json = JsonUtility.ToJson(saveObject);
        SaveSystem.itemName = itemName;
        SaveSystem.Save(json);
        
    }

    public void Load( )
    {
        SaveSystem.loadNumber = loadInt;
        string saveString = SaveSystem.Load();

        if(saveString != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            planet.resolution = saveObject.resolution;
            planet.shapeSettings.noiseSettings.roughness = saveObject.roughness;
            planet.shapeSettings.noiseSettings.strength = saveObject.strength;
            planet.shapeSettings.planetRadius = saveObject.radius;
            planet.move = saveObject.move;
            planet.speed = saveObject.speed;    


            planet.OnShapeSettingsUpdated();
        }
    }


    private class SaveObject
    {
        public int resolution;
        public float roughness;
        public float strength;
        public float radius;
        public bool move;
        public float speed;
        public string itemName;
       

    }

}
