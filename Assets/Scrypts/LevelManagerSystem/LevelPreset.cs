using Assets.Scrypts.Entity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrypts.LevelManagerSystem
{
    [Serializable]
    struct Shelter
    {
        public Vector2 point;
        public float radius;
        public GameObject shelterPrefab;

        public Shelter(Vector2 point, float radius, GameObject shelterPrefab)
        {
            this.point = point;
            this.radius = radius;
            this.shelterPrefab = shelterPrefab;
        }
        public void SetRandomPosition()
        {
            float angel = UnityEngine.Random.Range(0, 360);
            Vector2 offset = new Vector2(Mathf.Cos(angel), Mathf.Sin(angel)) * radius;
            point += offset;
        }
    }

    [CreateAssetMenu(fileName = "LevelPreset", menuName = "GamePresets/Level", order = 1)]
    class LevelPreset : ScriptableObject
    {
        public LevelType levelType;
        public Shelter[] shelters;
        public UnitInfos[] unitsInfos;
        public PlayableSymbolList symbols;
        public char[] Symbols { get => symbols.symbols; }
    }
}
