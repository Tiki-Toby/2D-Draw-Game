using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts.Ads
{
    public abstract class AdButtonController : MonoBehaviour
    {
        [SerializeField] MockAdController mockAdPrefab;
        MockAdController mockAd;
        private void Start()
        {
            //спавним панель рекламы
            mockAd = Instantiate(mockAdPrefab, GameObject.Find("BattleCan").transform);

            //загрузка рекламы
            mockAd.LoadRewardedVideo(gameObject);
            gameObject.SetActive(false);
            //запуск рекламы рип клике
            GetComponent<Button>().onClick.AddListener(() =>
            {
                if (mockAd.IsReadyRewardedVideo())
                    mockAd.ShowRewardedVideo();
            });

            mockAd.RewardedVideoFinished += Finished;
        }
        //вызов при завершении просмотра рекламы
        public abstract void Finished();
    }
}

