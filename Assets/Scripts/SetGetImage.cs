using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SetGetImage : MonoBehaviour
{
    public string FileName;
    public RenderTexture RT;
    public GameObject RenderCamera;
    public RawImage RI;
    public AssignDeleteButton assignDeleteButton;
    public int deleteButtonIndex;
   
    public void Start()
    {
        setImage();
        
    }
    public void getImage()
    {
        //RenderCamera.SetActive(true);
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
       // RenderCamera.SetActive(false);
    }
    public void setImage()
    {
        Debug.Log("setImageStarted");
        List<Transform> buttonTransforms = assignDeleteButton.buttonTransforms;
        List<int> existingImageNumbers = new List<int>(); // Store existing image numbers

        int newLargest = 0;
        int maxImageNumber = 10;
        if (File.Exists(Application.persistentDataPath + "aA" + newLargest + ".txt"))
        {
            maxImageNumber =  newLargest;
        }
        else
        {
            maxImageNumber = 20; 
        }


         // Set the maximum expected image number
        // Collect the existing image numbers
        for (int i = maxImageNumber; i >= 0; i--)
        {
            if (File.Exists(Application.persistentDataPath + FileName + i + ".png"))
            {
                existingImageNumbers.Add(i);
            }
        }
        // existingImageNumbers.Reverse();
        buttonTransforms.Reverse();
        foreach (int imageNumber in existingImageNumbers)
        {
            Debug.Log("image num"+ imageNumber);
            string path = Application.persistentDataPath + FileName + imageNumber + ".png";
            byte[] bytes = File.ReadAllBytes(path);

            Texture2D texture2D = new Texture2D(2, 2); // Create a new texture for each image
            texture2D.LoadImage(bytes);
            texture2D.Apply();
            Debug.Log("texture applied for " + imageNumber);

            if (imageNumber < buttonTransforms.Count)
            {
                Button button = buttonTransforms[imageNumber].GetComponent<Button>();
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
                Debug.LogError("No button found at index: " + imageNumber);
            }
        }
    }
   
    IEnumerator  RenderProcess()
    {
        RenderCamera.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        getImage();
        yield return new WaitForSeconds(0.01f);
        //setImage();
        yield return new WaitForSeconds(0.01f);
        RenderCamera.SetActive(false);
    }

    public int DeleteImageBuffer(int deleteButtonIndex)
    {
        Debug.Log (deleteButtonIndex);
        return  deleteButtonIndex;
       
    }

    public void DeleteImage()
    {
        int y = deleteButtonIndex;
       
        if (File.Exists(Application.persistentDataPath + FileName + y + ".png"))
        {
            Debug.Log("deleted image" + y);
           
            File.Delete(Application.persistentDataPath + FileName + y + ".png"); 
        }
        else
        {
            Debug.Log("bhak bc");
        }
    }
    public void GetSetImageButton()
    {
       StartCoroutine(RenderProcess());
    }

}
