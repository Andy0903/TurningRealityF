using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class PathLineDrawer : MonoBehaviour
{
    [SerializeField]
    Color[] colors;
    [SerializeField]
    string[] fileNames;
    List<List<SerializableVector3>> positions = new List<List<SerializableVector3>>();   //AllFiles, AllPositions.. 2D

    void Awake()
    {
        Load();
    }

    private void Load()
    {
        foreach (string fileName in fileNames)
        {
            if (File.Exists(Application.persistentDataPath + "/" + fileName + ".data"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/" + fileName + ".data", FileMode.Open);
                positions.Add((List<SerializableVector3>)bf.Deserialize(file));
                file.Close();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (EditorApplication.isPlaying)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                Gizmos.color = colors[i % colors.Length];
                Debug.Log(Gizmos.color);
                for (int j = 0; j < positions[i].Count - 1; j++)
                {
                    Gizmos.DrawLine(positions[i][j], positions[i][j + 1]);
                }
            }
        }
    }
}
