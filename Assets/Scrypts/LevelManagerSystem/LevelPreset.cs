using Assets.Scrypts.Enemy;
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
        public Vector2 GetRandomPosition()
        {
            float angel = UnityEngine.Random.Range(0, 360);
            return new Vector2(Mathf.Cos(angel), Mathf.Sin(angel)) * radius + point;
        }
    }

    [CreateAssetMenu(fileName = "LevelPreset", menuName = "GamePresets/Level", order = 1)]
    class LevelPreset : ScriptableObject
    {
        public LevelType levelType;
        public Shelter[] shelters;
        public UnitInfos[] unitsInfos;
        public PlayableSymbolList symbols;
        public string[] Symbols { get => symbols.symbols; }
        public Vector2[] Shelters
        {
            get
            {
                Vector2[] points = new Vector2[shelters.Length];
                for(int i = 0; i < shelters.Length; i++) 
                    points[i] = shelters[i].shelterPrefab.transform.position;
                return points;
            }
        }

        //public Vector2[] SpawnShelters()
        //{
        //    GameObject[] Forteress = GameObject.FindGameObjectsWithTag("Fortress");
        //    Transform shelterContainer = new GameObject().transform;
        //    shelterContainer.name = "Shelters";
        //    Vector2[] points = new Vector2[shelters.Length];
        //    for (int i = 0; i < shelters.Length; i++)
        //    {
        //        points[i] = shelters[i].GetRandomPosition();
        //        for(int j = 0; j < Forteress.Length; j++)
        //        {
        //            while (points[i] == (Vector2)Forteress[j].transform.position)
        //            {
        //                points[i] = shelters[i].GetRandomPosition();
        //            }
        //        }
        //        Instantiate(shelters[i].shelterPrefab, points[i], Quaternion.identity, shelterContainer);
        //    }
        //    return points;
        //}

        public Vector2[] SpawnShelters()
        {

            Transform shelterContainer = new GameObject().transform;
            shelterContainer.name = "Shelters";
            Vector2[] points = new Vector2[shelters.Length];
            for (int i = 0; i < shelters.Length; i++)
            {
                points[i] = shelters[i].GetRandomPosition();
                Instantiate(shelters[i].shelterPrefab, points[i], Quaternion.identity, shelterContainer);
            }
            return points;
        }
    }
}
