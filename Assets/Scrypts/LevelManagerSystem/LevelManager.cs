using Assets.Scrypts.Enemy;
using Assets.Scrypts.Entity;
using Assets.Scrypts.GameData;
using Assets.Scrypts.InputModule;
using Assets.Scrypts.UI;
using System;
using UniRx;
using UnityEngine;

namespace Assets.Scrypts.LevelManagerSystem
{
    class LevelManager : MonoBehaviour
    {
        [SerializeField] FortressController fortressPrefab;
        [SerializeField] PlayerAnimatorController playerAnimator;
        [SerializeField] LvlInfo lvlInfoOtputter;
        [SerializeField] Spawner spawner;
        [SerializeField] LevelEnemyManager enemiesController;
        [SerializeField] DrawInput drawInput;
        private LevelPreset levelPreset;

        private LevelPreset NextLevel =>
            Resources.Load<LevelPreset>($"Levels/Level{LevelData.levelData.currentLvl + 1}");

        private void Awake()
        {
            //грузим сохранение
            Profile.LoadData();
            //создаем данные уровня
            LevelData.InitData();

            //подписка на победу и поражение
            LevelData.levelData.fortressCount.Where(x => x == 0).Subscribe(OnLose);
            LevelData.levelData.enemyCount.Where(x => x == 0).Subscribe(OnWin);
        }
        //загрузка следующего уровня
        public void LoadNextLevel()
        {
            levelPreset = NextLevel;
            LevelData.levelData.currentLvl++;
            //проверка на существование следующего уровня
            if (levelPreset == null)
            {
                LevelData.levelData.currentLvl = 1;
                levelPreset = Resources.Load<LevelPreset>($"Levels/Level1");
            }
            LoadLevel();
        }
        public void LoadLevel()
        {
            DeserializeLevel();
            Vector2[] points = levelPreset.SpawnShelters();
            PathManager.pathManager.CreateNodeList(points);
            LevelData.levelData.InitLevelData(levelPreset);
            drawInput.InitSymbolTrainingAssets();

            lvlInfoOtputter.OutputLvlInfo();
            spawner.InitUnitInfos(levelPreset.unitsInfos);
        }
        public void LoadFirstLevel()
        {
            LevelData.levelData.currentLvl = 1;
            DeserializeLevel();
            GameObject[] forts = GameObject.FindGameObjectsWithTag("Fortress");
            for (int i = 0; i < forts.Length; i++)
                Destroy(forts[i]);

            Instantiate(fortressPrefab, new Vector2(0, -2f), Quaternion.identity);
            levelPreset = Resources.Load<LevelPreset>($"Levels/Level1");

            LoadLevel();
            LevelData.levelData.SetStartValutes();
        }
        //перезапуск прошлого состояния уровня
        public void ReloadLevel()
        {
            LevelData.levelData.ResetLvl(fortressPrefab);
            levelPreset = Resources.Load<LevelPreset>($"Levels/Level{LevelData.levelData.currentLvl}");
            LoadLevel();
        }
        //уничтожение укрытий и врагов
        private void DeserializeLevel()
        {
            playerAnimator.Reset();
            GameObject shelters = GameObject.Find("Shelters");

            enemiesController.DestroyAll();
            Destroy(shelters);

            //прячем панель с инфоормацией об уровне
            lvlInfoOtputter.Destruct();
        }
        private void OnLose(int count)
        {
            if(count == 0)
            {
                playerAnimator.SetLose();
                levelPreset = Resources.Load<LevelPreset>($"Levels/Level1");
                drawInput.Clear();
                Debug.Log("You Lose!");
            }
        }
        private void OnWin(int count)
        {
            if (count == 0)
            {
                playerAnimator.SetVictory();
                Profile.UpdateLevel(LevelData.levelData.currentLvl);
                Profile.SaveData();
                drawInput.Clear();
                Debug.Log("You Win!");
            }
        }
        private void OnDestroy()
        {
            Profile.SaveData();
        }
    }
}
