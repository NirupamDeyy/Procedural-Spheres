using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;


public class SetGetImage : MonoBehaviour
{
    public string FileName;
    public RenderTexture RT;
    public GameObject RenderCamera;
    public RawImage RI;
    public AssignDeleteButton assignDeleteButton;

    public void Start()
    {
        setImage();
        
    }
    public void getImage()
    {
        
        Texture2D texture2D = new Texture2D(RT.width, RT.height, TextureFormat.ARGB32, false);
        RenderTexture.active = RT;
        texture2D.ReadPixels(new Rect(0, 0, RT.width, RT.height),0,0);
        texture2D.Apply();
        byte[] bytes = texture2D.EncodeToPNG();

        int ImageNumber = 0;
        while(File.Exists(Application.persistentDataPath + FileName + ImageNumber + ".png"))
        {
            ImageNumber++;
            Debug.Log("new image num" + ImageNumber);
        }

        string Path = Application.persistentDataPath + FileName + ImageNumber + ".png";
        File.WriteAllBytes(Path, bytes);
        Debug.Log("Save Path: " + Path);
    }
    public void setImage()
    {
        List<Transform> buttonTransforms = assignDeleteButton.buttonTransforms;

        int ImageNumber = 0;
        while (File.Exists(Application.persistentDataPath + FileName + ImageNumber + ".png"))
        {
            string path = Application.persistentDataPath + FileName + ImageNumber + ".png";
            byte[] bytes = File.ReadAllBytes(path);

            Texture2D texture2D = new Texture2D(2, 2); // Create a new texture for each image
            texture2D.LoadImage(bytes);
            texture2D.Apply();

            if (ImageNumber < buttonTransforms.Count)
            {
                Button button = buttonTransforms[ImageNumber].GetComponent<Button>();
                if (button != null)
                {
                    RawImage rawImage = button.GetComponentInChildren<RawImage>();
                    if (rawImage != null)
                    {
                        rawImage.texture = texture2D;
                    }
                    else
                    {
                        Debug.LogError("RawImage component not found on the button.");
                    }
                }
                else
                {
                    Debug.LogError("Button component not found on the button transform.");
                }
            }
            else
            {
                Debug.LogError("No button found at index: " + ImageNumber);
            }

            ImageNumber++;
        }
    }
    /*int largestImageNumber = 0;
    ImageNumber = largestImageNumber;*/
    /*string path = Application.persistentDataPath + FileName + ImageNumber + ".png";
    byte[] bytes = File.ReadAllBytes(path);

    texture2D.LoadImage(bytes);
    texture2D.Apply ();
    //get which image to show the file
    // assign RI texture
    RI.texture = texture2D;*/

    IEnumerator RenderProcess()
    {
        RenderCamera.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        getImage();
        yield return new WaitForSeconds(0.01f);
        //setImage();
        yield return new WaitForSeconds(0.01f);
        RenderCamera.SetActive(false);
    }
    public void GetSetImageButton()
    {
        StartCoroutine(RenderProcess());
    }

}
