using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    public static void SaveStars(StarTracker starTracker)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/stars.lmao";
        FileStream stream = new FileStream(path, FileMode.Create);
        StarSaver saver = new StarSaver(starTracker);
        formatter.Serialize(stream, saver);
        stream.Close();
    }
    public static StarSaver LoadStars()
    {
        string path = Application.persistentDataPath + "/stars.lmao";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            StarSaver data = formatter.Deserialize(stream) as StarSaver;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}