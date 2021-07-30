using Assets.Scrypts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Assets.Scrypts.LevelManagerSystem
{
    public enum LevelType
    {
        Normal,
        Bonus,
        Boss
    }
    public enum ValutType
    {
        Coin,
        Crystal
    }
    class LevelData
    {
        private static LevelData levelData;
        public static LevelData Data
        {
            get
            {
                if (levelData == null)
                    levelData = new LevelData();
                return levelData;
            }
            set
            {
                levelData = value;
            }
        }

        public readonly char[] symbols;
        public readonly LevelType levelType;
        public readonly LongReactiveProperty coins, crystals;
        public readonly IntReactiveProperty enemyCount, shelterCount;
        public long Coins { get => coins.Value; set => coins.SetValueAndForceNotify(value); }
        public long Crystals { get => crystals.Value; set => crystals.SetValueAndForceNotify(value); }
        public readonly PathManager shelterPath;

        public Vector2[] GetNodesForPath(Vector2 startPoint) =>
            shelterPath.SearchPath(startPoint);

        private LevelData()
        {
            coins = new LongReactiveProperty(0);
            crystals = new LongReactiveProperty(0);

            enemyCount = new IntReactiveProperty(0);
            enemyCount = new IntReactiveProperty(0);

            symbols = new char[1] { 'a' };
            levelType = LevelType.Normal;
        }
        private LevelData(LevelPreset levelPreset, Vector2[] points)
        {
            symbols = levelPreset.Symbols;
            levelType = levelPreset.levelType;

            coins = new LongReactiveProperty(0);
            crystals = new LongReactiveProperty(0);

            shelterPath = new PathManager(points);

            enemyCount = new IntReactiveProperty(levelPreset.unitsInfos.Length);
            shelterCount = new IntReactiveProperty(1);
        }
        public static void InitData(LevelPreset levelPreset, Vector2[] points) =>
            levelData = new LevelData(levelPreset, points);
    }
}
