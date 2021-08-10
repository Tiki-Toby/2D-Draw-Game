using Assets.Scrypts.LevelManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Assets.Scrypts.GameData;

namespace Assets.Scrypts.UI
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;
        public static UIManager Instance => _instance;

        [SerializeField] Image drawArea;
        [SerializeField] GameObject blurPanel;

        List<PanelController> panels;

        public void AddPanel(PanelController panelPrefab)
        {
            if (panels.Count == 0)
                SetPause(true);
            else
                foreach (PanelController panel in panels)
                    if (panelPrefab.gameObject.name == panel.gameObject.name)
                        return;

            SpawnPanel(panelPrefab);
        }
        public void Blur()
        {
            blurPanel.SetActive(true);
            Image blurImage = blurPanel.GetComponent<Image>();
            Color color = blurImage.color;
            blurImage.color = new Color(color.r, color.g, color.b, 0);
            blurImage.DOFade(0.4f, PanelControllData.TimePanelSpawnDelay).SetEase(Ease.InQuint);
        }

        public void DeletePanel(PanelController panel)
        {
            panels.Remove(panel);
            DestroyPanel(panel);

            if (panels.Count == 0)
            {
                SetPause(false);
            }
        }

        private void Awake()
        {
            panels = new List<PanelController>();
            _instance = this;

            PanelController[] activePanels = GameObject.FindObjectsOfType<PanelController>();
            for (int i = 0; i < activePanels.Length; i++)
                panels.Add(activePanels[i]);
            SetPause(panels.Count > 0);
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
                drawArea.raycastTarget = false;
                blurPanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                drawArea.raycastTarget = true;
                blurPanel.SetActive(false);
            }
        }
    }
}

