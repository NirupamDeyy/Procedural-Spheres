using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Planet : MonoBehaviour {

    [Range(2,256)]
    public int resolution = 10;
    public bool autoUpdate = true;

    public ShapeSettings shapeSettings;
    public ColourSettings colourSettings;
    //
    public bool move = false;
    public bool randomMove = false;
    public NoiseSettings NS;
    public ColourPicker CC;

    public float speed;
    public float x;
    public float y;
    public float z;

    public Slider roughnessSlider;
    public Slider strengthSlider;
    public Slider resolutionSlider;
    public Slider radiusSlider;
    public Slider speedSlider;
    public Slider xSlider;
    public Slider ySlider;
    public Slider zSlider;
    public Button toMove;
    public Button toRandomMove;

    public TMP_Text polygonText;

    //
    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colourSettingsFoldout;

    ShapeGenerator shapeGenerator;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    TerrainFace terrainFace;
    
    NoiseFilter noiseFilter;
    
    

    public void Start()
    {
        move = false;
    }

    private void Update()
    {
        if(move)
        {
            Move();
        }

        if(randomMove)
        {
            RandomMove();
           // Debug.Log("rand");
        }
       
        polygonText.text = "Polygon Count: " + (12*(resolution - 1)*(resolution - 1)).ToString();

        // GeneratePlanet();
    }

    public void ActionOnClick()
    {
        if(move == false && move != true)
        {
            move = true;
            speed = 2.0f;
        }
        else if(move == true && move != false)
        {
            move = false;
        }
    }

    public void ActionOnClickRandom()
    {
        if (randomMove == false && randomMove != true)
        {
            randomMove = true;
            speed = 1.0f;
        }
        else if (randomMove == true && randomMove != false)
        {
            randomMove = false;
        }
    }

    public Planet(ShapeSettings shapeSettings)
    {
        this.shapeSettings = shapeSettings;
        noiseFilter = new NoiseFilter(shapeSettings.noiseSettings);
    }


    public void OnSliderValueChangedRoughness(float value)
    {
        shapeSettings.noiseSettings.roughness = roughnessSlider.value;
        OnShapeSettingsUpdated();
    }

    public void OnSliderValueChangedStrength(float value)
    {
        shapeSettings.noiseSettings.strength = strengthSlider.value;
        OnShapeSettingsUpdated();
    }

    public void OnSliderValueChangedResolution(float value)
    {
        resolution = (int)resolutionSlider.value;
        OnShapeSettingsUpdated();
    }

    public void OnSliderValueChangedRadius(float value)
    {
        shapeSettings.planetRadius = radiusSlider.value;
        OnShapeSettingsUpdated();
    }

    public void OnSliderValueChangedSpeed(float value)
    {
        speed = speedSlider.value;
        OnShapeSettingsUpdated();
    }

    public void MoveX(float value)
    {
        shapeSettings.noiseSettings.centre = new Vector3(x, y, z);
        x = xSlider.value;
        OnShapeSettingsUpdated();
    }
    public void MoveY(float value)
    {
        shapeSettings.noiseSettings.centre = new Vector3(x, y, z);
        y = ySlider.value;
    }

    public void MoveZ(float value)
    {
        shapeSettings.noiseSettings.centre = new Vector3(x, y, z);
        z = zSlider.value;
    }


    public void Move()
    {
        shapeSettings.noiseSettings.centre = new Vector3(x, y, z);
        x += speed * Time.deltaTime;
        OnShapeSettingsUpdated();
    }

    public void RandomMove()
    {
        shapeSettings.noiseSettings.centre = new Vector3(x, y, z);
        x += speed * Time.deltaTime;
        y += speed * Time.deltaTime;
        z += speed * Time.deltaTime;
        OnShapeSettingsUpdated();
     
    }
    void Initialize()
    {
        shapeGenerator = new ShapeGenerator(shapeSettings);

        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        terrainFaces = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
            
        }
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColours();
    }

    public void OnShapeSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
    }

    public void OnColourSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateColours();
        }
    }

    void GenerateMesh()
    {
        foreach (TerrainFace face in terrainFaces)
        {
            face.ConstructMesh();
        }
    }

    void GenerateColours()
    {
        foreach (MeshFilter m in meshFilters)
        {
            m.GetComponent<MeshRenderer>().material.color = CC.updatedColor;
        }
    }
}
