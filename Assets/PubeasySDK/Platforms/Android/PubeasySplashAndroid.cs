
using System;
using System.Collections.Generic;
using PubeasySDK.Api;
using PubeasySDK.ThirdParty.MiniJSON;
using UnityEngine;

namespace PubeasySDK.Android
{

    public class PubeasySplashAndroid
    {
        private static AndroidJavaClass TPSplashClass = new AndroidJavaClass("com.tradplus.unity.plugin.splash.TPSplashManager");
        private AndroidJavaObject TPSplash = TPSplashClass.CallStatic<AndroidJavaObject>("getInstance");


        private static PubeasySplashAndroid _instance;

        public static PubeasySplashAndroid Instance()
        {
            if (_instance == null)
            {
                _instance = new PubeasySplashAndroid();
            }
            return _instance;
        }

        public void LoadSplashAd(string adUnitId, TPSplashExtra extra)
        {
            AdLoadListenerAdapter loadListenerAdapter = new AdLoadListenerAdapter();
            Dictionary<string, object> info = new Dictionary<string, object>();
            info.Add("customMap",extra.customMap);
            info.Add("localParams", extra.localParams);
            info.Add("isSimpleListener", extra.isSimpleListener);
            info.Add("maxWaitTime", extra.maxWaitTime);
            info.Add("openAutoLoadCallback", extra.openAutoLoadCallback);



            TPSplash.Call("loadAd",adUnitId,Json.Serialize(info),loadListenerAdapter);
        }

        public void ShowSplashAd(string adUnitId, string sceneId)
        {
            TPSplash.Call("showAd", adUnitId, sceneId);

        }

        public bool SplashAdReady(string adUnitId)
        {
            return TPSplash.Call<bool>("isReady",adUnitId);
        }

        public void EntrySplashAdScenario(string adUnitId, string sceneId)
        {
            TPSplash.Call("entryAdScenario", adUnitId, sceneId);
        }

        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
            TPSplash.Call("setCustomShowData", adUnitId, Json.Serialize(customAdInfo));

        }

        private class AdLoadListenerAdapter : AndroidJavaProxy
        {

            public AdLoadListenerAdapter() : base("com.tradplus.unity.plugin.splash.TPSplashListener")
            {
            }

            void onAdLoaded(string unitId, string tpAdInfo) {
            if(PubeasySplashAndroid.Instance().OnSplashLoaded != null)
                PubeasySplashAndroid.Instance().OnSplashLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdClicked(string unitId, string tpAdInfo) {
                        if(PubeasySplashAndroid.Instance().OnSplashClicked != null)

                PubeasySplashAndroid.Instance().OnSplashClicked(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdImpression(string unitId, string tpAdInfo) {
                        if(PubeasySplashAndroid.Instance().OnSplashImpression != null)

                PubeasySplashAndroid.Instance().OnSplashImpression(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }// 展示 1300

            void onAdFailed(string unitId, string error) {
                        if(PubeasySplashAndroid.Instance().OnSplashLoadFailed != null)

                PubeasySplashAndroid.Instance().OnSplashLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(error));
            }

            void onAdClosed(string unitId, string tpAdInfo) {
                        if(PubeasySplashAndroid.Instance().OnSplashClosed != null)

                PubeasySplashAndroid.Instance().OnSplashClosed(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 关闭 1400

            void onAdAllLoaded(string unitId,bool b)
            {
                        if(PubeasySplashAndroid.Instance().OnSplashAllLoaded != null)

                PubeasySplashAndroid.Instance().OnSplashAllLoaded(unitId, b);

            }

            void oneLayerLoadFailed(string unitId, string tperror, string tpAdInfo)
            {
                        if(PubeasySplashAndroid.Instance().OnSplashOneLayerLoadFailed != null)

                PubeasySplashAndroid.Instance().OnSplashOneLayerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(tperror),(Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onZoomOutStart(string unitId, string tpAdInfo)
            {
                        if(PubeasySplashAndroid.Instance().OnSplashZoomOutStart != null)

                PubeasySplashAndroid.Instance().OnSplashZoomOutStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onZoomOutEnd(string unitId, string tpAdInfo)
            {
                        if(PubeasySplashAndroid.Instance().OnSplashZoomOutEnd != null)

                PubeasySplashAndroid.Instance().OnSplashZoomOutEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onSplashSkip(string unitId, string tpAdInfo)
            {
                        if(PubeasySplashAndroid.Instance().OnSplashSkip != null)

                PubeasySplashAndroid.Instance().OnSplashSkip(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void oneLayerLoaded(string unitId, string tpAdInfo)
            {
                        if(PubeasySplashAndroid.Instance().OnSplashOneLayerLoaded != null)

                PubeasySplashAndroid.Instance().OnSplashOneLayerLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onAdStartLoad(string unitId)
            {
            if(PubeasySplashAndroid.Instance().OnSplashStartLoad != null)

                PubeasySplashAndroid.Instance().OnSplashStartLoad(unitId,null);

            }

            void oneLayerLoadStart(string unitId, string tpAdInfo)
            {
                        if(PubeasySplashAndroid.Instance().OnSplashOneLayerStartLoad != null)

                PubeasySplashAndroid.Instance().OnSplashOneLayerStartLoad(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingStart(string unitId, string tpAdInfo)
            {
                        if(PubeasySplashAndroid.Instance().OnSplashBiddingStart != null)

                PubeasySplashAndroid.Instance().OnSplashBiddingStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingEnd(string unitId, string tpAdInfo, string tpAdError)
            {
                        if(PubeasySplashAndroid.Instance().OnSplashBiddingEnd != null)

                PubeasySplashAndroid.Instance().OnSplashBiddingEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), (Dictionary<string, object>)Json.Deserialize(tpAdError));

            }

            void onAdIsLoading(string unitId)
            {
                        if(PubeasySplashAndroid.Instance().OnSplashIsLoading != null)

                PubeasySplashAndroid.Instance().OnSplashIsLoading(unitId);

            }

            void onDownloadStart(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                        if(PubeasySplashAndroid.Instance().OnDownloadStart != null)

                PubeasySplashAndroid.Instance().OnDownloadStart(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes,currBytes,fileName,appName);
            }

            void onDownloadUpdate(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName, int progress)
            {
                        if(PubeasySplashAndroid.Instance().OnDownloadUpdate != null)

                PubeasySplashAndroid.Instance().OnDownloadUpdate(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName,progress);
            }

            void onDownloadPause(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                        if(PubeasySplashAndroid.Instance().OnDownloadPause != null)

                PubeasySplashAndroid.Instance().OnDownloadPause(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFinish(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                        if(PubeasySplashAndroid.Instance().OnDownloadFinish != null)

                PubeasySplashAndroid.Instance().OnDownloadFinish(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFail(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                        if(PubeasySplashAndroid.Instance().OnDownloadFailed != null)

                PubeasySplashAndroid.Instance().OnDownloadFailed(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onInstallled(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                        if(PubeasySplashAndroid.Instance().OnInstalled != null)

                PubeasySplashAndroid.Instance().OnInstalled(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }
        }

        public PubeasySplashAndroid()
        {

        }

        public event Action<string, Dictionary<string, object>> OnSplashLoaded;

        public event Action<string, Dictionary<string, object>> OnSplashLoadFailed;

        public event Action<string, Dictionary<string, object>> OnSplashImpression;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnSplashShowFailed;

        public event Action<string, Dictionary<string, object>> OnSplashClicked;

        public event Action<string, Dictionary<string, object>> OnSplashClosed;

        public event Action<string, Dictionary<string, object>> OnSplashSplash;

        public event Action<string, Dictionary<string, object>> OnSplashStartLoad;

        public event Action<string, Dictionary<string, object>> OnSplashBiddingStart;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnSplashBiddingEnd;

        public event Action<string> OnSplashIsLoading;

        public event Action<string, Dictionary<string, object>> OnSplashOneLayerStartLoad;

        public event Action<string, Dictionary<string, object>> OnSplashOneLayerLoaded;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnSplashOneLayerLoadFailed;


        public event Action<string, Dictionary<string, object>> OnSplashSkip;
        public event Action<string, Dictionary<string, object>> OnSplashZoomOutEnd;
        public event Action<string, Dictionary<string, object>> OnSplashZoomOutStart;

        public event Action<string, bool> OnSplashAllLoaded;


        //国内下载监听
        public event Action<string, Dictionary<string, object>, long, long, string, string> OnDownloadStart;

        public event Action<string, Dictionary<string, object>, long, long, string, string, int> OnDownloadUpdate;

        public event Action<string, Dictionary<string, object>, long, long, string, string> OnDownloadPause;

        public event Action<string, Dictionary<string, object>, long, long, string, string> OnDownloadFinish;

        public event Action<string, Dictionary<string, object>, long, long, string, string> OnDownloadFailed;

        public event Action<string, Dictionary<string, object>, long, long, string, string> OnInstalled;



    }
}