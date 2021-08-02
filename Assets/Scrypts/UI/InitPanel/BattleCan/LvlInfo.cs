using Assets.Scrypts.LevelManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LvlInfo : MonoBehaviour
{
    void Start()
    {
        Text lvlType = transform.GetChild(0).GetComponent<Text>();
        Text lvl = transform.GetChild(1).GetComponent<Text>();


        string type = LevelData.levelData.levelType.ToString();
        switch (LevelData.levelData.levelType)
        {
            case LevelType.Normal:
                lvlType.gameObject.SetActive(false);
                break;
            case LevelType.Bonus:
                lvlType.text = type;
                break;
            case LevelType.Boss:
                lvlType.text = type;
                break;
        }
        //lvl.text = "LEVEL " + LevelData.levelData.
    }

    void Update()
    {
        
    }
}
