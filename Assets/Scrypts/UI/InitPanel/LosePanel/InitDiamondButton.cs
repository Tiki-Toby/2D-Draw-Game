using Assets.Scrypts.GameData;
using Assets.Scrypts.LevelManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts.UI
{
    public class InitDiamondButton : MonoBehaviour
    {
        void Start()
        {
            if (Profile.profileData.crystal < PanelControllData.DiamondForRepeatLevel)
                GetComponent<Button>().interactable = false;
            else
                GetComponent<Button>().onClick.AddListener(() =>
                {
                    GameObject.FindObjectOfType<LevelManager>().ReloadLevel();
                    DestroyActivePanel();
                    Profile.AddValut(-PanelControllData.DiamondForRepeatLevel, ValutType.Crystal);
                });
        }
        void DestroyActivePanel()
        {
            GameObject[] panels = GameObject.FindGameObjectsWithTag("Panel");
            foreach (GameObject obj in panels)
                Destroy(obj);
        }
    }
}