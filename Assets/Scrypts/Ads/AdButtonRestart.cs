using Assets.Scrypts.LevelManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrypts.Ads
{
    public class AdButtonRestart : AdButtonController
    {
        public override void Finished()
        {
            GameObject.FindObjectOfType<LevelManager>().ReloadLevel();

            GameObject[] panels = GameObject.FindGameObjectsWithTag("Panel");
            foreach (GameObject obj in panels)
                Destroy(obj);
        }
    }
}

