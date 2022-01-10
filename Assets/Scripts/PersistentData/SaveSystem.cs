using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    public static void SaveStars(StarTracker starTracker)
    {
        var formatter = new BinaryFormatter();
        var path = Application.persistentDataPath + "/stars.lmao";
        var stream = new FileStream(path, FileMode.Create);
        var saver = new StarSaver(starTracker);
        formatter.Serialize(stream, saver);
        stream.Close();
    }
    public static StarSaver LoadStars()
    {
        string path = Application.persistentDataPath + "/stars.lmao";
        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);
            var data = formatter.Deserialize(stream) as StarSaver;
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