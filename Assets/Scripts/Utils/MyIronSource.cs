using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MyIronSource : MonoBehaviour
{
    public static MyIronSource instance;
#if UNITY_ANDROID
    string YOUR_APP_KEY = "183ec6055";
#elif UNITY_IOS
    string YOUR_APP_KEY = "187371e85";
#else
    string YOUR_APP_KEY = "unexpected_platform";
#endif
    public event Action currentOnRewardedEvent;

    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        //IronSource.Agent.init(YOUR_APP_KEY, IronSourceAdUnits.REWARDED_VIDEO);
        //IronSource.Agent.init(YOUR_APP_KEY, IronSourceAdUnits.INTERSTITIAL);
        //IronSource.Agent.init(YOUR_APP_KEY, IronSourceAdUnits.BANNER);

        IronSource.Agent.validateIntegration();
        IronSource.Agent.init(YOUR_APP_KEY);

        IronSource.Agent.loadBanner(IronSourceBannerSize.SMART, IronSourceBannerPosition.BOTTOM);
        IronSourceBannerSize.BANNER.SetAdaptive(true);
        IronSource.Agent.shouldTrackNetworkState(true);
    }

    private void OnEnable()
    {
        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;

        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;


        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;


        IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;
    }
    void Update()
    {

    }

    public void TestLoadBanner()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.SMART, IronSourceBannerPosition.BOTTOM);
        IronSource.Agent.displayBanner();
    }
    public void LoadInterstitial()
    {
        IronSource.Agent.loadInterstitial();
    }

    public void LoadRewardAds(Action onRewarded)
    {
        currentOnRewardedEvent = onRewarded;
        if(IronSource.Agent.isRewardedVideoAvailable()) IronSource.Agent.showRewardedVideo();
    }
    
    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void SdkInitializationCompletedEvent()
    {
        Debug.LogWarning("SdkInitializationCompletedEvent");
    }

    void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
        Debug.LogError("InterstitialAdLoadFailedEvent");
    }

    // Invoked when the ad fails to show.
    // @param description - string - contains information about the failure.
    void InterstitialAdShowFailedEvent(IronSourceError error)
    {
        Debug.LogError("InterstitialAdShowFailedEvent");
    }

    // Invoked when end user clicked on the interstitial ad
    void InterstitialAdClickedEvent()
    {
        Debug.LogWarning("InterstitialAdClickedEvent");
    }

    // Invoked when the interstitial ad closed and the user goes back to the application screen.
    void InterstitialAdClosedEvent()
    {
        Debug.LogWarning("InterstitialAdClosedEvent");
    }

    // Invoked when the Interstitial is Ready to shown after load function is called
    void InterstitialAdReadyEvent()
    {
        Debug.LogWarning("InterstitialAdReadyEvent");
        IronSource.Agent.showInterstitial();
    }

    // Invoked when the Interstitial Ad Unit has opened
    void InterstitialAdOpenedEvent()
    {
        Debug.LogWarning("InterstitialAdOpenedEvent");
    }

    // Invoked right before the Interstitial screen is about to open.
    // NOTE - This event is available only for some of the networks. 
    // You should not treat this event as an interstitial impression, but rather use InterstitialAdOpenedEvent
    void InterstitialAdShowSucceededEvent()
    {
        Debug.LogWarning("InterstitialAdShowSucceededEvent");
    }

    //Invoked when the RewardedVideo ad view has opened.
    //Your Activity will lose focus. Please avoid performing heavy 
    //tasks till the video ad will be closed.
    void RewardedVideoAdOpenedEvent()
    {
        Debug.LogWarning("RewardedVideoAdOpenedEvent");
    }

    //Invoked when the RewardedVideo ad view is about to be closed.
    //Your activity will now regain its focus.
    void RewardedVideoAdClosedEvent()
    {
        Debug.LogWarning("RewardedVideoAdClosedEvent");
        IronSource.Agent.init(YOUR_APP_KEY, IronSourceAdUnits.REWARDED_VIDEO);
        IronSource.Agent.shouldTrackNetworkState(true);
        currentOnRewardedEvent?.Invoke();
        currentOnRewardedEvent = null;
    }

    //Invoked when there is a change in the ad availability status.
    //@param - available - value will change to true when rewarded videos are available. 
    //You can then show the video by calling showRewardedVideo().
    //Value will change to false when no videos are available.
    void RewardedVideoAvailabilityChangedEvent(bool available)
    {
        //Change the in-app 'Traffic Driver' state according to availability.
        Debug.LogWarning("RewardedVideoAvailabilityChangedEvent" + available.ToString());
        bool rewardedVideoAvailability = available;
    }

    //Invoked when the user completed the video and should be rewarded. 
    //If using server-to-server callbacks you may ignore this events and wait for 
    // the callback from the  ironSource server.
    //@param - placement - placement object which contains the reward data
    void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
    {
        Debug.LogWarning("RewardedVideoAdRewardedEvent" + placement.ToString());
    }

    //Invoked when the Rewarded Video failed to show
    //@param description - string - contains information about the failure.
    void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {
        Debug.LogError("RewardedVideoAdShowFailedEvent " + error.ToString());
    }

    // ----------------------------------------------------------------------------------------
    // Note: the events below are not available for all supported rewarded video ad networks. 
    // Check which events are available per ad network you choose to include in your build. 
    // We recommend only using events which register to ALL ad networks you include in your build. 
    // ----------------------------------------------------------------------------------------

    //Invoked when the video ad starts playing. 
    void RewardedVideoAdStartedEvent()
    {
        Debug.LogWarning("RewardedVideoAdStartedEvent");
    }

    //Invoked when the video ad finishes playing. 
    void RewardedVideoAdEndedEvent()
    {
        Debug.LogWarning("RewardedVideoAdEndedEvent");
    }

    //Invoked when the video ad is clicked. 
    void RewardedVideoAdClickedEvent(IronSourcePlacement placement)
    {
        Debug.LogWarning("RewardedVideoAdClickedEvent" + placement.ToString());
    }

    void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
    {
        Debug.LogWarning("RewardedVideoOnAdAvailable");
    }

    // Indicates that no ads are available to be displayed
    // This replaces the RewardedVideoAvailabilityChangedEvent(false) event
    void RewardedVideoOnAdUnavailable()
    {
        //TW.I.AddPopup_Warning("", "There is no available video at the movement, please try again later");
        Debug.LogError("RewardedVideoOnAdUnavailable");
    }
    // The Rewarded Video ad view has opened. Your activity will loose focus.
    void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
        Debug.LogWarning("RewardedVideoOnAdOpenedEvent " + adInfo.ToString());
    }
    // The Rewarded Video ad view is about to be closed. Your activity will regain its focus.
    void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
        Debug.LogWarning("RewardedVideoOnAdClosedEvent " + adInfo.ToString());
    }

    // The user completed to watch the video, and should be rewarded.
    // The placement parameter will include the reward data.
    // When using server-to-server callbacks, you may ignore this event and wait for the ironSource server callback.
    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    {
        Debug.LogWarning("RewardedVideoOnAdRewardedEvent " + placement.ToString() + " ||||| " + adInfo.ToString());
    }

    // The rewarded video ad was failed to show.
    void RewardedVideoOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo)
    {
        Debug.LogError("RewardedVideoOnAdShowFailedEvent " + error.ToString() + " ||||| " + adInfo.ToString());
    }

    // Invoked when the video ad was clicked.
    // This callback is not supported by all networks, and we recommend using it only if
    // it’s supported by all networks you included in your build.
    void RewardedVideoOnAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    {
        Debug.LogWarning("RewardedVideoOnAdClickedEvent " + placement.ToString() + " ||||| " + adInfo.ToString());
    }
}
