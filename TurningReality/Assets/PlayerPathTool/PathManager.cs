using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathManager : MonoBehaviour
{
    public static PathManager Instance { get; private set; }

    static List<SerializableVector3> positions = new List<SerializableVector3>();
    string fileName = "/PlayerPath";
    [SerializeField]
    bool activated = true;
    [SerializeField]
    float saveIntervalTime = 10f;

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
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + fileName + " Build_ " + SceneManager.GetActiveScene().buildIndex + " Time_ " + System.DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".data");

        bf.Serialize(file, positions);
        file.Close();
    }

    public void Clear()
    {
        positions = new List<SerializableVector3>();
    }
}
