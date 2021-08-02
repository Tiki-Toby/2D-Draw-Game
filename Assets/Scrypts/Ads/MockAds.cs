using UnityEngine;
using System;

public interface IAdsProvider
{
    /// <summary>
    /// ���������������� ���������
    /// </summary>
    /// <param name="isChildDirected">������� �������</param>
    void Init(bool isChildDirected = false);

    /// <summary>
    /// ����������
    /// </summary>
    void Deinit();

    /// <summary>
    /// ��������� Rewarded ����� �����
    /// </summary>
    void LoadRewardedVideo(GameObject gameObject);

    /// <summary>
    /// ������ �� Rewarded �����
    /// </summary>
    /// <returns>true - ���� ������ � ������, � ��������� ������ false</returns>
    bool IsReadyRewardedVideo();

    /// <summary>
    /// �������� Rewarded �����
    /// </summary>
    void ShowRewardedVideo();

    /// <summary>
    /// ���������� ��������� Reward �����
    /// </summary>
    Action RewardedVideoFinished { get; set; }

    /// <summary>
    /// ������� Reward �����
    /// </summary>
    Action RewardedVideoSkipped { get; set; }

}
