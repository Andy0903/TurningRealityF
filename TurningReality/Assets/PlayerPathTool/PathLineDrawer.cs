using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PathLineDrawer : MonoBehaviour
{
    [SerializeField]
    Color color = Color.red;
    [SerializeField]
    string fileName;
    static List<SerializableVector3> positions = new List<SerializableVector3>();

    void Awake()
    {
        Load();
    }

    private void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/" + fileName + ".data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + fileName + ".data", FileMode.Open);
            positions = (List<SerializableVector3>)bf.Deserialize(file);
            file.Close();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        for (int i = 0; i < positions.Count - 1; i++)
        {
            Gizmos.DrawLine(positions[i], positions[i + 1]);
        }
    }
}
