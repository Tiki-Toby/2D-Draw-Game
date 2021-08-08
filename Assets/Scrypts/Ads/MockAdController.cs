
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrypts.Ads
{
    class MockAdController : MonoBehaviour, IAdsProvider
    {
        [SerializeField] float timeForLoad;
        [SerializeField] float timeForShowAd;

        private GameObject observer;
        private static string gameId = "999999";
        private bool IsReady;
        private bool isAdShowen;
        private bool isChildDirected;
        //подписки на события

        //полный просмотр рекламы
        public Action RewardedVideoFinished { get; set; }
        //пропуск рекламы
        public Action RewardedVideoSkipped { get; set; }

        //деструктор - унитожаем панель
        public void Deinit()
        {
            if (!isAdShowen)
                RewardedVideoSkipped?.Invoke();
            Destroy(observer);
            Destroy(gameObject);
        }

        public void Init(bool isChildDirected = false)
        {
            isAdShowen = true;
            transform.GetChild(0).gameObject.SetActive(false);
            IsReady = false;
            this.isChildDirected = isChildDirected;
        }

        public bool IsReadyRewardedVideo() => IsReady;

        public void LoadRewardedVideo(GameObject gameObject)
        {
            IsReady = false;
            observer = gameObject;
            StartCoroutine(LoadAd());
        }

        public void ShowRewardedVideo()
        {
            transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(ShowAd());
        }
        private IEnumerator LoadAd()
        {
            yield return new WaitForSecondsRealtime(timeForLoad);
            Debug.Log("Ready");
            IsReady = true;
            observer.SetActive(true);
        }
        //показ рекламы
        private IEnumerator ShowAd()
        {
            isAdShowen = false;
            yield return new WaitForSecondsRealtime(timeForShowAd);
            isAdShowen = true;
            RewardedVideoFinished.Invoke();
            Deinit();
        }
        private void Start()
        {
            Init();
        }
    }
}
