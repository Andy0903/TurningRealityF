using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathManager : MonoBehaviour
{
    public static PathManager Instance { get; private set; }

    static Guid guid;
    static List<SerializableVector3> positions = new List<SerializableVector3>();
    string fileName = "/PlayerPath";
    [SerializeField]
    bool activated = true;
    [SerializeField]
    float saveIntervalTime = 10f;

    int lastSavedBuildIndex = -1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        InvokeRepeating("Save", saveIntervalTime, saveIntervalTime);
    }

    public void UpdateData(Vector3 position)
    {
        if (activated == false) return;
        positions.Add(position);
    }

    public void Save()
    {
        if (activated == false) return;

        if (lastSavedBuildIndex != SceneManager.GetActiveScene().buildIndex)
        {
            guid = Guid.NewGuid();
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + fileName + "Build_" + SceneManager.GetActiveScene().buildIndex + "ID_" + guid.ToString() + ".data");

        bf.Serialize(file, positions);
        file.Close();

        lastSavedBuildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void Clear()
    {
        positions = new List<SerializableVector3>();
    }
}
