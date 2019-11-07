using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveManager
{
    public static PlayerData current = new PlayerData();
    public static string SavefileVersion = "0.0.0";

    public static int Save()
    {
        if (current.Version != SavefileVersion) return -2;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + current.FileName + ".sav");
        if (file == null) return -3;
        bf.Serialize(file, current);
        file.Close();
        return 0;
    }

    public static void Save(string FileName)
    {
        //string temp = PlayerData.current.FileName;
        current.FileName = FileName;
        Save();
    }

    public static int Load(string FileName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + FileName + ".sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + FileName + ".sav", FileMode.Open);
            current = (PlayerData)bf.Deserialize(file);
            file.Close();

            if (current.Version != SavefileVersion)
                return -2;
            return 0;
        }
        else return -1;
    }

    public static string[] ListFiles()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*.sav");

        for (int i = 0; i < files.Length; i++)
        {
            files[i] = files[i].Substring(0, files[i].Length - 5);
            Debug.Log(files[i]);
        }

        return files;
    }

    public static bool Delete(string FileName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + FileName + ".sav"))
        {
            File.Delete(Application.persistentDataPath + "/" + FileName + ".sav");
            return true;
        }
        else return false;
    }
}
