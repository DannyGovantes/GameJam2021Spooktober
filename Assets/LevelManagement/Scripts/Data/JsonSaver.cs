using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//JSON UTILITY
using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;


public class JsonSaver
{
    private static readonly string _fileName = "saveData.sav";

    public static string GetSaveFilename()
    {
        return Application.persistentDataPath + "/" + _fileName;
    }

    public void Save(SaveData data)
    {

        data.hashValue = string.Empty;

        //Convert our data and shit into a json file
        string json = JsonUtility.ToJson(data);

        data.hashValue = GetSHA256(json);
        json = JsonUtility.ToJson(data);


        //Gettin a new file name
        string saveFileNme = GetSaveFilename();

        FileStream fileStream = new FileStream(saveFileNme, FileMode.Create);

        // This help us with the opening, saving and closing the file in the disk
        using (StreamWriter writer = new StreamWriter(fileStream))
        {


            writer.Write(json);
        }
    }

    public bool Load(SaveData data)
    {

        string loadFileName = GetSaveFilename();
        if (File.Exists(loadFileName))
        {
            using (StreamReader reader = new StreamReader(loadFileName))
            {
                string json = reader.ReadToEnd();
                if (CheckData(json))
                {

                    JsonUtility.FromJsonOverwrite(json, data);
                }
                else
                {
                    Debug.LogWarning("JSONSAVER: LOAD INVALID HASH");
                }

            }
            return true;
        }
        return false;

    }

    private bool CheckData(string json)
    {

        SaveData tempSaveData = new SaveData();
        JsonUtility.FromJsonOverwrite(json, tempSaveData);
        string oldHashValue = tempSaveData.hashValue;
        tempSaveData.hashValue = string.Empty;

        string tempJson = JsonUtility.ToJson(tempSaveData);
        string newHash = GetSHA256(tempJson);

        return (oldHashValue == newHash);
    }

    public void Delete()
    {
        File.Delete(GetSaveFilename());
    }

    private string GetSHA256(string text)
    {

        byte[] textToBytes = Encoding.UTF8.GetBytes(text);

        SHA256Managed sHA256Managed = new SHA256Managed();

        byte[] hashValue = sHA256Managed.ComputeHash(textToBytes);

        return GetHexSrtingFromHash(hashValue);

    }

    public string GetHexSrtingFromHash(byte[] hash)
    {
        string hexString = string.Empty;

        foreach (byte b in hash)
        {
            hexString += b.ToString("x2");
        }
        return hexString;
    }


}


