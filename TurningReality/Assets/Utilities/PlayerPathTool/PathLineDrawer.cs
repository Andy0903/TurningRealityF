using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//using UnityEditor;
using UnityEngine;

public class PathLineDrawer : MonoBehaviour
{
    [SerializeField]
    GameObject Pathpointprefab;
    Transform world;

    [SerializeField]
    Color[] colors;
    [SerializeField]
    string[] fileNames;
    List<List<SerializableVector3>> files = new List<List<SerializableVector3>>();   //AllFiles, AllPositions.. 2D
    List<List<GameObject>> pathPositionObjects = new List<List<GameObject>>();

    void Awake()
    {
        world = GameObject.FindGameObjectWithTag("WorldOrigin").transform;
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
                files.Add((List<SerializableVector3>)bf.Deserialize(file));
                file.Close();
            }
        }

        GameObject masterPathPoint = GameObject.Instantiate(Pathpointprefab, world.position, world.rotation, world);
        for (int i = 0; i < files.Count; i++)
        {
            pathPositionObjects.Add(new List<GameObject>());
            for (int j = 0; j < files[i].Count; j++)
            {
                pathPositionObjects[i].Add(GameObject.Instantiate(Pathpointprefab, files[i][j], world.rotation, masterPathPoint.transform));
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    if (EditorApplication.isPlaying)
    //    {
    //        for (int i = 0; i < pathPositionObjects.Count; i++)
    //        {
    //            Gizmos.color = colors[i % colors.Length];
    //            for (int j = 0; j < pathPositionObjects[i].Count - 1; j++)
    //            {
    //                Gizmos.DrawLine(pathPositionObjects[i][j].transform.position, pathPositionObjects[i][j + 1].transform.position);
    //            }
    //        }
    //    }
    //}
}
