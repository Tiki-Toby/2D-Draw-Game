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
            //������� ������ �������
            mockAd = Instantiate(mockAdPrefab, GameObject.Find("BattleCan").transform);

            //�������� �������
            mockAd.LoadRewardedVideo(gameObject);
            gameObject.SetActive(false);
            //������ ������� ��� �����
            GetComponent<Button>().onClick.AddListener(() =>
            {
                if (mockAd.IsReadyRewardedVideo())
                    mockAd.ShowRewardedVideo();
            });

            mockAd.RewardedVideoFinished += Finished;
        }
        //����� ��� ���������� ��������� �������
        public abstract void Finished();
    }
}

