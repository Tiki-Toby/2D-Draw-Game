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
        private static LevelData _levelData;
        public static LevelData levelData
        {
            get
            {
                if (_levelData == null)
                    _levelData = new LevelData();
                return _levelData;
            }
            set
            {
                _levelData = value;
            }
        }

        public readonly string[] symbols;
        public readonly Dictionary<string, Sprite> symbolSprites; 
        public readonly LevelType levelType;
        public readonly LongReactiveProperty[] valutes;
        public readonly IntReactiveProperty enemyCount, fortressCount;
        public long Coins { get => valutes[(int)ValutType.Coin].Value; set => valutes[(int)ValutType.Coin].SetValueAndForceNotify(value); }
        public long Crystals { get => valutes[(int)ValutType.Crystal].Value; set => valutes[(int)ValutType.Crystal].SetValueAndForceNotify(value); }
        public readonly PathManager shelterPath;

        public Sprite GetSpriteOf(string symbolName) =>
            symbolSprites[symbolName];

        public Vector2 GetClosestFortress(Vector2 startPoint) =>
            shelterPath.ClosestFortress(startPoint);
        public Vector2[] GetNodesForPath(Vector2 startPoint) =>
            shelterPath.SearchPath(startPoint);

        public void AddValute(ValutType valutType, long add) =>
            valutes[(int)valutType].Value += add;
        public void SubscribeOnValut(ValutType valutType, Action<long> onChange) =>
            valutes[(int)valutType].Subscribe(onChange);

        private LevelData()
        {
            valutes = new LongReactiveProperty[2];
            foreach(ValutType valutType in Enum.GetValues(typeof(ValutType)))
                valutes[(int)valutType] = new LongReactiveProperty(0);

            enemyCount = new IntReactiveProperty(0);
            fortressCount = new IntReactiveProperty(0);

            symbols = new string[] { "a" };
            levelType = LevelType.Normal;
        }
        private LevelData(LevelPreset levelPreset, Vector2[] points)
        {
            symbols = levelPreset.Symbols.Clone() as string[];
            symbolSprites = new Dictionary<string, Sprite>();
            Sprite[] sprites = Resources.LoadAll<Sprite>("SymbolLists/SymbolSprites");

            for(int i = 0; i < symbols.Length; i++)
            {
                foreach(Sprite sprite in sprites)
                    if (sprite.name.Equals(symbols[i]))
                    {
                        symbols[i] = symbols[i].ToLower();
                        Debug.Log(symbols[i]);
                        symbolSprites.Add(symbols[i], sprite);
                        break;
                    }
            }
                    
            levelType = levelPreset.levelType;

            valutes = new LongReactiveProperty[2];
            foreach (ValutType valutType in Enum.GetValues(typeof(ValutType)))
                valutes[(int)valutType] = new LongReactiveProperty(0);

            shelterPath = new PathManager(points);

            enemyCount = new IntReactiveProperty(levelPreset.unitsInfos.Length);
            fortressCount = new IntReactiveProperty(1);
        }
        public static void InitData(LevelPreset levelPreset, Vector2[] points) =>
            levelData = new LevelData(levelPreset, points);
    }
}
