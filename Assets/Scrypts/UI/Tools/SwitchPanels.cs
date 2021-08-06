using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts.UI
{
    public class SwitchPanels : MonoBehaviour
    {
        [SerializeField] GameObject panel;
        private void Start()
        {
            this.GetComponent<Button>().onClick.AddListener(() =>
            {
                Transform parent = transform.root;
                parent.GetComponent<SpawnerManager>().SpawnPanel(panel);
            });
        }
    }
}