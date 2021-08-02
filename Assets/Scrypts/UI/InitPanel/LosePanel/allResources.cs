using Assets.Scrypts.GameData;
using Assets.Scrypts.LevelManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class allResources : MonoBehaviour
{
    [SerializeField] ValutType valut;

    void Start()
    {
        Text text = GetComponent<Text>();
        text.text = (Profile.profileData.coin + LevelData.levelData.Coins).ToString();
    }
}
