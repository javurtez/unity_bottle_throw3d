using GoogleMobileAds.Api;
using System;

public class AdManager : MonoManager<AdManager>
{
    public AdScriptable adScriptable;

    public bool isShowingAds = false;

    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;

    protected override void Start()
    {
        RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
           .SetMaxAdContentRating(MaxAdContentRating.G)
           .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        MobileAds.Initialize(initStatus =>
        {
            RequestRewardAds();
            RequestInterstitial();
        });
    }

    public void RequestRewardAds()
    {
        if (adScriptable.appRewardedId == "") return;

        rewardedAd = new RewardedAd(adScriptable.appRewardedId);
        this.rewardedAd.OnAdLoaded += HandleRewardBasedVideoStarted;
        this.rewardedAd.OnUserEarnedReward += HandleRewardBasedVideoRewarded;
        this.rewardedAd.OnAdClosed += HandleRewardBasedVideoClosed;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);

    }
    public void OnRewardShow()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }

    private void RequestBanner()
    {
        if (adScriptable.appBannerId == "") return;
        OnBannerClose();

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adScriptable.appBannerId, AdSize.Banner, adScriptable.bannerPosition);

        // Called when the user returned from the app after an ad click.
        bannerView.OnAdClosed += HandleOnBannerClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    private void RequestInterstitial()
    {
        if (adScriptable.appInterstitialId == "") return;
        OnInterstitialClose();

        this.interstitial = new InterstitialAd(adScriptable.appInterstitialId);

        interstitial.OnAdClosed += HandleOnInterstitialAdClosed;
        interstitial.OnAdOpening += HandleOnInterstitialAdOpen;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    public void OnBannerShow()
    {
        RequestBanner();
    }
    public void OnBannerClose()
    {
        if (bannerView != null)
            bannerView.Destroy();
    }

    public void OnInterstitialShow()
    {
        if (interstitial == null) return;
        if (this.interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }
    public void OnInterstitialClose()
    {
        if (interstitial != null)
            interstitial.Destroy();
    }

    public void HandleOnBannerClosed(object sender, EventArgs args)
    {
        //this.RequestBanner();
    }

    private void HandleOnInterstitialAdOpen(object sender, EventArgs args)
    {
        isShowingAds = true;
    }
    public void HandleOnInterstitialAdClosed(object sender, EventArgs args)
    {
        this.RequestInterstitial();
        isShowingAds = false;
    }

    private void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        isShowingAds = true;
    }
    private void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        PlayerManager.Instance.AddCoins(250);
        BottlePanel.Instance.OnBottleRandom();
    }
    private void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        isShowingAds = false;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }
}