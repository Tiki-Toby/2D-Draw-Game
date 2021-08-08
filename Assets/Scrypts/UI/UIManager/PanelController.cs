using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts.UI
{
    public class PanelController : MonoBehaviour
    {
        [SerializeField] Button[] OnOffButtons;

        UIManager manager;
        public void SetManager(UIManager manager)
        {
            this.manager = manager;
        }

        public void CreatePanel(PanelController panel)
        {
            manager.AddPanel(panel);
        }

        public void DestroySelf()
        {
            manager.DeletePanel(this);
            
        }

        public void DestoySelfAndLoadNew(PanelController panel)
        {
            manager.lastPanelName = this.gameObject.name;
            manager.AddPanel(panel);
            DestroySelf();
        }

        public void RaycastController(bool isActive)
        {
            for (int i = 0; i< OnOffButtons.Length; i++)
            {
                OnOffButtons[i].interactable = isActive;
            }
        }
    }

}
