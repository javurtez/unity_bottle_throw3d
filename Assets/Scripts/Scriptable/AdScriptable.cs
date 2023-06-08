using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

[CreateAssetMenu(fileName = "Ad Data", menuName = "Scriptable/Ad Data", order = 1)]
public class AdScriptable : ScriptableObject
{
    public string appId;
    public string appBannerId;
    public string appInterstitialId;
    public string appRewardedId;

    public AdPosition bannerPosition;
}