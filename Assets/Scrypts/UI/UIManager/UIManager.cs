using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject drawArea;
        [SerializeField] GameObject blurPanel;

        List<PanelController> panels;
        string lastPanelName;

        public void SetLastPanelName(string name)
        {
            this.lastPanelName = name;
        }

        public void AddPanel(PanelController panelPrefab)
        {
            Debug.Log(lastPanelName);
            if (panels.Count == 0)
            {
                SpawnPanel(panelPrefab);
                SetPause(true);
            }
            else
            {
                foreach (PanelController panel in panels)
                {
                    if ((panelPrefab.gameObject.name == panel.gameObject.name) || (panelPrefab.gameObject.name == lastPanelName))
                    {
                        return;
                    }
                }

                panels[panels.Count - 1].RaycastController(false);
                SpawnPanel(panelPrefab);
            }

            panels[panels.Count - 1].SetManager(this);
        }

        public void DeletePanel(PanelController panel)
        {
            panels.Remove(panel);
            DestroyPanel(panel);

            if (panels.Count == 0)
            {
                SetPause(false);
            }
            else
            {
                panels[panels.Count - 1].RaycastController(true);
            }

        }

        private void Awake()
        {
            panels = new List<PanelController>();

            GameObject[] activePanels = GameObject.FindGameObjectsWithTag("Panel");
            for (int i = 0; i < activePanels.Length; i++)
            {
                panels.Add(activePanels[i].GetComponent<PanelController>());
                panels[i].SetManager(this);
            }

            Debug.Log(panels);
        }

        private void SpawnPanel(PanelController panelPrefab)
        {
            PanelController panel = Instantiate(panelPrefab, this.transform);
            panel.name = panelPrefab.name;
            panels.Add(panel);
        }

        private void DestroyPanel(PanelController panel)
        {
            Destroy(panel.gameObject);
        }

        private void SetPause(bool key)
        {
            if (key)
            {
                Time.timeScale = 0;
                drawArea.SetActive(false);
                blurPanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                drawArea.SetActive(true);
                blurPanel.SetActive(false);
            }
        }
    }
}

