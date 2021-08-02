using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class SpawnerManager : MonoBehaviour
{
    public void SpawnPanel(GameObject panel)
    {
        GameObject obj = GameObject.Find(panel.name);
        if (obj)
        {
            Destroy(obj);
            return;
        }

        DestroyActivePanel();
        Instantiate(panel, this.transform).name = panel.name;
    }

    void DestroyActivePanel()
    { 
        GameObject[] panels = GameObject.FindGameObjectsWithTag("Panel");
        foreach (GameObject obj in panels)
        {
            Destroy(obj);
        }
    }
}
