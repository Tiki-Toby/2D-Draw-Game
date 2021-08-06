using Assets.Scrypts.GameData;
using Assets.Scrypts.InputModule;
using Assets.Scrypts.LevelManagerSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scrypts.Enemy
{
    //Очередность закрытия символов-хп врага
    public enum SymbolCloseType
    {
        Left,
        Right,
        AnyOrder
    }
    [Serializable]
    public struct AttackData
    {
        [SerializeField] public float dmg;
        [SerializeField] public float heatPerSecond;
        [SerializeField] public int attackCount;

        public float AttackDelay { get => 1f / heatPerSecond; }

        public AttackData(float dmg, float heatPerSecond, int attackCount)
        {
            this.dmg = dmg;
            this.heatPerSecond = heatPerSecond;
            this.attackCount = attackCount;
        }
    }

    [Serializable]
    public class EnemyHP
    {
        [SerializeField] private SymbolCloseType closeType;
        [SerializeField] private int countSymbol;

        public bool isHide { get; private set; }
        public bool isAlive { get => hpSymbols.Count > 0; }
        private List<string> hpSymbols;
        private Func<string, bool> onTakeDamage;
        private SymbolOutputController symbolOutputter;
        public SymbolOutputController SymbolContainer => symbolOutputter;

        public void CreateHp(Transform enemy)
        {
            hpSymbols = new List<string>();
            int last = LevelData.levelData.symbols.Length;
            for (int i = 0; i < countSymbol; i++)
            {
                int index = UnityEngine.Random.Range(0, last);
                hpSymbols.Add(LevelData.levelData.symbols[index]);
            }
            InitTakeDamage(closeType);

            symbolOutputter = new GameObject("Symbols").AddComponent<SymbolOutputController>();

            symbolOutputter.InitSymbolChain(hpSymbols.ToArray());
            Transform symbolsContainer = symbolOutputter.transform;
            symbolsContainer.SetParent(enemy);
            symbolsContainer.localPosition = new Vector2(0, 0.5f);
            symbolsContainer.localScale = Vector2.one * EnemyData.SymbolIconScale;
        }
        public bool isTakeDamage(string c) => onTakeDamage.Invoke(c);
        public void SwitchHide()
        {
            isHide = !isHide;
            symbolOutputter.HideSymbols(isHide);
        }
        public void SetHide(bool isHide)
        {
            this.isHide = isHide;
            symbolOutputter.HideSymbols(isHide);
        }

        private void InitTakeDamage(SymbolCloseType closeType)
        {
            switch (closeType)
            {
                case SymbolCloseType.AnyOrder:
                    onTakeDamage = (string c) =>
                    {
                        if (hpSymbols.Contains(c))
                            return OnTakeDamage(c, hpSymbols.IndexOf(c));
                        else
                            return false;
                    };
                    break;
                case SymbolCloseType.Left:
                    onTakeDamage = (string c) =>
                        OnTakeDamage(c, 0);
                    break;
                case SymbolCloseType.Right:
                    onTakeDamage = (string c) =>
                        OnTakeDamage(c, hpSymbols.Count - 1);
                    break;
            }
        }
        private bool OnTakeDamage(string c, int index)
        {
            bool isDamage = hpSymbols[index] == c && !isHide;
            if (isDamage)
            {
                symbolOutputter.TakeDamage(index);
                hpSymbols.RemoveAt(index);
            }
            return isDamage;
        }
    }
}
