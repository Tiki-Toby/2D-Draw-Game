using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scrypts.UI;

public class SwitchSetting : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject fPanel;
    [SerializeField] GameObject sPanel;

    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() =>
        {
            Transform parent = transform.root;

            if (GameObject.Find(mainPanel.name))
            {
                parent.GetComponent<SpawnerManager>().SpawnPanel(fPanel);
            }
            else
            {
                parent.GetComponent<SpawnerManager>().SpawnPanel(sPanel);
            }
        });
    }
}
