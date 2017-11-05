using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour {
    public static SaveManager instance;

    public SaveData saveData;

	// Use this for initialization
	void Awake() {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }else
        {
            Destroy(gameObject);
        }
	}


    bool waitingtoputplayer = false;
    private void Update()
    {
        if (waitingtoputplayer)
        {
            Player p = FindObjectOfType<Player>();
            if(p != null)
            {
                p.transform.position = System.Array.Find<Checkpoint>(FindObjectsOfType<Checkpoint>(), (c) => c.id == saveData.checkpointID).transform.position;
                waitingtoputplayer = false;
            }
        }else if (Input.GetKeyDown(KeyCode.K))
        {
            Destroy(FindObjectOfType<Player>().gameObject);
            SceneManager.LoadScene(saveData.lastCheckpointScene);
            waitingtoputplayer = true;
        }
    }

    public static bool isTrue(string tag)
    {
        return instance.saveData.tags.Contains(tag);
    }
	
    public static void setTrue(string tag)
    {
        instance.saveData.tags.Add(tag);
    }

    public static void setFalse(string tag)
    {
        instance.saveData.tags.Remove(tag);
    }

    public static void loadFromSave(string saveFileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open(saveFileName, FileMode.Open);

        var saveData = (SaveData)formatter.Deserialize(saveFile);
        instance.saveData = saveData;

        saveFile.Close();
    }

    public static void saveToFile(string saveFileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create(saveFileName);

        formatter.Serialize(saveFile, instance.saveData);

        saveFile.Close();
    }
}

public struct SaveData
{
    public string lastCheckpointScene;
    public int checkpointID; //unique per scene
    public HashSet<string> tags;
}
