using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PathManager
{
    public static List<SerializableVector3> positions = new List<SerializableVector3>();
    static string fileName = "/PlayerPath";
    static bool activated = true;

    public static void UpdateData(Vector3 position)
    {
        if (activated == false) return;
        positions.Add(position);
    }

    public static void Save()
    {
        if (activated == false) return;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath +  fileName + " Build_ " + SceneManager.GetActiveScene().buildIndex + " Time_ " + System.DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".data");

        bf.Serialize(file, positions);
        file.Close();
    }
}
