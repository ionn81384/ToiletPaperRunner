using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AddManager : MonoBehaviour
{

    private BannerView bannerView;

    public void Start()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-3940256099942544~3347511713";
#elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
#else
            string appId = "unexpected_platform";
#endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        this.RequestBanner();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
    }






    //public string gameId = "3527247";
    //public string placementId = "BannerAddYo";
    //public bool testMode = true;

    //void Start()
    //{
    //    if (GameData.d.isAddOn)
    //    {
    //        Advertisement.Initialize(gameId, testMode);
    //        StartCoroutine(ShowBannerWhenReady());
    //    }
    //}

    //IEnumerator ShowBannerWhenReady()
    //{
    //    while (!Advertisement.IsReady(placementId))
    //    {
    //        yield return new WaitForSeconds(0.5f);
    //    }
    //    Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
    //    Advertisement.Banner.Show(placementId);
    //}
}
