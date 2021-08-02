using Assets.Scrypts.LevelManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesPerLvl: MonoBehaviour
{
    [SerializeField] ValutType valut;

    void Start()
    {
        Text text = GetComponent<Text>();
        LevelData.levelData.SubscribeOnValut(valut, (long value) => text.text = value.ToString());
    }

    void Update()
    {
        
    }
}
