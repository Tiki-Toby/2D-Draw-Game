using Assets.Scrypts.GameData;
using Assets.Scrypts.LevelManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scrypts.UI
{
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
            StartCoroutine(DelayOnStart(winPanel));
        }

        void Lose(int count)
        {
            StartCoroutine(DelayOnStart(losePanel));
        }

        IEnumerator DelayOnStart(GameObject panel)
        {
            yield return new WaitForSeconds(PanelControllData.TimePanelSpawnDelay);
            Instantiate(panel, this.transform).name = panel.name;
        }
    }
}