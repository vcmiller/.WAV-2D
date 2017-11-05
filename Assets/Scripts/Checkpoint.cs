using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour {

    private static bool CHECK_IN_CASE_THE_DEVS_ARE_RETARDED = true;

    public int id;

    private void Awake()
    {
        if (CHECK_IN_CASE_THE_DEVS_ARE_RETARDED)
        {
            foreach (Checkpoint c in FindObjectsOfType<Checkpoint>())
            {
                if(c != this && c.id == id)
                {
                    throw new System.Exception("holy shit just make the ids different. it's " + name + " and " + c.name + " you fucked up, btw.");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInChildren<Player>())
        {
            Save();
        }
    }

    void Save()
    {
        SaveManager.instance.saveData.lastCheckpointScene = SceneManager.GetActiveScene().name;
        SaveManager.instance.saveData.checkpointID = id;
    }

}
