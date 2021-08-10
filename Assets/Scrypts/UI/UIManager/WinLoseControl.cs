using Assets.Scrypts.GameData;
using Assets.Scrypts.LevelManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts.UI
{
    public class WinLoseControl : MonoBehaviour
    {
        [SerializeField] Image pauseButton;
        [SerializeField] PanelController winPanel;
        [SerializeField] PanelController losePanel;

        void Start()
        {
            LevelData.levelData.enemyCount.Where(x => x == 0).Subscribe(Win);
            LevelData.levelData.fortressCount.Where(x => x == 0).Subscribe(Lose);
        }
        
        void Win(int count) =>
            StartCoroutine(DelayOnStart(winPanel));

        void Lose(int count) =>
            StartCoroutine(DelayOnStart(losePanel));

        IEnumerator DelayOnStart(PanelController panelPrefab)
        {
            UIManager.Instance.Blur();
            yield return new WaitForSeconds(PanelControllData.TimePanelSpawnDelay);
            UIManager.Instance.AddPanel(panelPrefab);
        }

    }
}

