using Assets.Scrypts.LevelManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


public class InitBattleCanvas : MonoBehaviour
{
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;

    void Start()
    {
        LevelData.levelData.enemyCount.Where(x => x == 0).Subscribe(Win);
        LevelData.levelData.fortressCount.Where(x => x == 0).Subscribe(Lose);
    }

    void Win(int count)
    {
        Debug.Log(GameObject.Find(losePanel.name));
        if(!GameObject.Find(losePanel.name))
        {
            Instantiate(winPanel, this.transform).name = winPanel.name;
        }
    }

    void Lose(int count)
    {
        Instantiate(losePanel, this.transform).name = losePanel.name;
    }
}
