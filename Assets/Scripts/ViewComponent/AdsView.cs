using UnityEngine;

namespace ShopTown.ViewComponent
{
public class AdsView : MonoBehaviour, IAdsView
{
#if UNITY_ANDROID
    [SerializeField] private string _appKey = "17e63e4dd";
#else
[SerializeField] private string _appKey = "unexpected_platform";
#endif

    private void Start()
    {
        IronSource.Agent.validateIntegration();
        IronSource.Agent.init(_appKey, IronSourceAdUnits.INTERSTITIAL);
        IronSourceEvents.onInterstitialAdClosedEvent += LoadInterstitialAd;
        LoadInterstitialAd();
    }

    public void ShowInterstitialAd()
    {
        IronSource.Agent.showInterstitial();
    }

    private void LoadInterstitialAd()
    {
        IronSource.Agent.loadInterstitial();
    }
}
}

public interface IAdsView
{
    void ShowInterstitialAd();
}
