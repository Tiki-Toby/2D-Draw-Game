using Assets.Scrypts.Entity;
using Assets.Scrypts.InputModule;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Assets.Scrypts.LevelManagerSystem
{
    class LevelManager : MonoBehaviour
    {
        [SerializeField] Animator playerAnimator;
        [SerializeField] LevelPreset levelPreset;
        [SerializeField] Spawner spawner;

        private void Awake()
        {
            List<Vector2> shelterPositions = new List<Vector2>();
            Transform shelterContainer = new GameObject().transform;
            shelterContainer.name = "Shelters";
            foreach(Shelter shelter in levelPreset.shelters)
            {
                shelter.SetRandomPosition();
                shelterPositions.Add(shelter.point);
                Instantiate(shelter.shelterPrefab, shelter.point, Quaternion.identity, shelterContainer);
            }
            LevelData.InitData(levelPreset, shelterPositions.ToArray());
            
            spawner.InitUnitInfos(levelPreset.unitsInfos);
        }
        private void OnLoseFortress(int count)
        {
            if (count == 0)
                Debug.Log("You Lose");
        }
    }
}
