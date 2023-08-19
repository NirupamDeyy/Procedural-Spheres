using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
public static class SaveSystem
{
    public static int loadNumber;
    public static int largestNumber;
    public static string itemName;
    public static byte[] capturedBytes;

    //public static int saveNumber;
   
    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

    public static string ignoredFileNamePattern = "largest_number";
    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))//if folder exists
        {
            Directory.CreateDirectory(SAVE_FOLDER);//create save folder
        }
    }

    public static void StartSave()
    {
        string[] textFiles = Directory.GetFiles(SAVE_FOLDER, "*.txt");

        int lgnum = 0;
        foreach (string textFile in textFiles)
        {
            string fileName = Path.GetFileName(textFile);

            // Check if the file name matches the ignore pattern
            if (fileName.StartsWith(ignoredFileNamePattern))
            {
               lgnum++;
            }
        }
        
        if(lgnum == 0)
        {
            File.Create(SAVE_FOLDER + "largest_number" + 0 + ".txt");
        }
           

        }

    public static List<string> ReadItemNames()
    {
        List<string> itemNames = new List<string>();

        // Get all text files in the specified folder
        string[] textFiles = Directory.GetFiles(SAVE_FOLDER, "*.txt");

        foreach (string textFile in textFiles)
        {
            string fileName = Path.GetFileName(textFile);

            // Check if the file name matches the ignore pattern
            if (fileName.StartsWith(ignoredFileNamePattern))
            {
                continue;
            }

            // Read the content of the text file
            string content = File.ReadAllText(textFile);

            // Find the "itemName" field in the content
            int startIndex = content.IndexOf("\"itemName\":") + "\"itemName\":".Length;
            int endIndex = content.IndexOf("\"", startIndex + 1);

            if (startIndex >= 0 && endIndex > startIndex)
            {
                string itemName = content.Substring(startIndex, endIndex - startIndex);
                itemNames.Add(itemName);
            }
        }
        itemNames.Reverse();
        return itemNames;
    }


    public static List<int> GetFileNumbers()
    {
        List<int> numberList = new List<int>();
       
        int largestNumber = 0;
        while (!File.Exists(SAVE_FOLDER + "largest_number" + largestNumber + ".txt"))
        {
            largestNumber++;
        }
        //Debug.Log(largestNumber);
        
        for(int i = 0; i <= largestNumber; i++)
        {
            if (File.Exists(SAVE_FOLDER + "save_" + i + ".txt"))
            {
                //Debug.Log("filenum: " + i);
                numberList.Add(i);
            }
        }
        return numberList;
    }
    public static void Save(string saveString)
    {
        int saveNumber = 0;
        int largestNumber =0;

        while (!File.Exists(SAVE_FOLDER + "largest_number" + largestNumber + ".txt"))
        {
            largestNumber++;
        }
        int newLargest = largestNumber + 1;
        File.Move(SAVE_FOLDER + "largest_number" + largestNumber + ".txt", SAVE_FOLDER + "largest_number" + newLargest + ".txt");

        largestNumber = newLargest;
        saveNumber = largestNumber;

        File.WriteAllText(SAVE_FOLDER + "save_" + saveNumber + ".txt", saveString);
        Debug.Log("saved_" + saveNumber + "_file");
        
    }
    public static string Load()
    {
        if (File.Exists(SAVE_FOLDER + "save_" + loadNumber + ".txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "save_" + loadNumber + ".txt");
            return saveString;
        }
        else
        {
            return null;
        }
    }

    public static void Delete()
    {
        if (File.Exists(SAVE_FOLDER + "save_" + loadNumber + ".txt"))
        {
            File.Delete(SAVE_FOLDER + "save_" + loadNumber + ".txt");
        }
       
    }
}