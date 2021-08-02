using Assets.Scrypts.Enemy;
using Assets.Scrypts.GameData;
using UniRx;
using UnityEngine;

namespace Assets.Scrypts.LevelManagerSystem
{
    class LevelManager : MonoBehaviour
    {
        [SerializeField] PlayerAnimatorController playerAnimator;
        [SerializeField] LevelPreset levelPreset;
        [SerializeField] Spawner spawner;

        private void Awake()
        {
            Profile.LoadData();

            Vector2[] points = levelPreset.SpawnShelters();
            LevelData.InitData(levelPreset, points);
            
            spawner.InitUnitInfos(levelPreset.unitsInfos);

            LevelData.levelData.fortressCount.Where(x => x == 0).Subscribe(OnLose);
            LevelData.levelData.enemyCount.Where(x => x == 0).Subscribe(OnWin);
        }
        private void OnLose(int count)
        {
            if(count == 0)
            {
                playerAnimator.SetLose();
                Debug.Log("You Lose!");
            }
        }
        private void OnWin(int count)
        {
            if (count == 0)
            {
                playerAnimator.SetVictory();
                Debug.Log("You Win!");
            }
        }
    }
}
