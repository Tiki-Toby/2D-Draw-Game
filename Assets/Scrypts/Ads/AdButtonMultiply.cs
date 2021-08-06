using Assets.Scrypts.GameData;
using Assets.Scrypts.LevelManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts.Ads
{
    public class AdButtonMultiply : AdButtonController
    {
        [SerializeField] Text coin;
        [SerializeField] Text crystal;
        public override void Finished()
        {
            Profile.AddValut(LevelData.levelData.LvlCoins, ValutType.Coin);
            Profile.AddValut(LevelData.levelData.LvlCrystals, ValutType.Crystal);
            Profile.SaveData();

            LevelData.levelData.LvlCoins *= 2;
            LevelData.levelData.LvlCrystals *= 2;

            coin.text = LevelData.levelData.LvlCoins.ToString();
            crystal.text = LevelData.levelData.LvlCrystals.ToString();
        }
    }
}
