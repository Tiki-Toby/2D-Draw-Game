using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class Ads : IAdsProvider
{
    [SerializeField] bool IsReady;
    [SerializeField] Button Finished, Skipped;

    private static string gameId = "999999999999";

    public Action RewardedVideoFinished { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Action RewardedVideoSkipped { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Init(bool isChildDirected = false)
    {
        Advertisement.Initialize(gameId, true);
        if (isChildDirected)
            Debug.Log("Ads activate");
        else
         Debug.Log("Ads activate");
    }

    public void Deinit() => Debug.Log("Ads deactivate");

    public bool IsReadyRewardedVideo() {
        if (Advertisement.isInitialized && Advertisement.isSupported && IsReady)
        {
            Debug.Log("Rewarded video is ready");
            return true;
        }
        else
        {
            Debug.Log("Rewarded video is not ready");
            return false;
        }
     }

    public void LoadRewardedVideo(GameObject gameObject)
    {
        Button button = gameObject.GetComponent<Button>();
        if (button.IsActive())
        {
            Advertisement.Load("rewardedVideo");
            Debug.Log("Rewarded video was load");
        }
        else
            Debug.Log("Rewarded video was not load");

    }

    public void ShowRewardedVideo()
    {
        Advertisement.Show("rewardedVideo");
        Debug.Log("Rewarded video shows");
        RewardedVideoFinished?.Invoke();
    }
}
