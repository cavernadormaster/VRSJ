using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class ADInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGAmeId;
    [SerializeField] string _IOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    [SerializeField] RewardsAds rewardsAdsButton;

    private void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _IOSGameId
            : _androidGAmeId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("ADS COMPLETE");
        rewardsAdsButton.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity ADS Initialization FAild : {error.ToString()} - {message}");
    }

   
}
