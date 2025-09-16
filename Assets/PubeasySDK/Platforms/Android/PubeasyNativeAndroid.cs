
using System;
using System.Collections.Generic;
using PubeasySDK.Api;
using PubeasySDK.ThirdParty.MiniJSON;
using UnityEngine;

namespace PubeasySDK.Android
{

    public class PubeasyNativeAndroid
    {
        private static AndroidJavaClass TPNativeClass = new AndroidJavaClass("com.tradplus.unity.plugin.nativead.TPNativeManager");
        private AndroidJavaObject TPNative = TPNativeClass.CallStatic<AndroidJavaObject>("getInstance");


        private static PubeasyNativeAndroid _instance;

        public static PubeasyNativeAndroid Instance()
        {
            if (_instance == null)
            {
                _instance = new PubeasyNativeAndroid();
            }
            return _instance;
        }

        public void LoadNativeAd(string adUnitId, TPNativeExtra extra)
        {
            AdLoadListenerAdapter loadListenerAdapter = new AdLoadListenerAdapter();
            Dictionary<string, object> info = new Dictionary<string, object>();

            info.Add("width", extra.width);
            info.Add("height", extra.height);
            info.Add("x", extra.x);
            info.Add("y", extra.y);
            info.Add("customMap", extra.customMap);
            info.Add("adPosition", (int)extra.adPosition);
            info.Add("localParams", extra.localParams);
            info.Add("isSimpleListener", extra.isSimpleListener);
            info.Add("maxWaitTime", extra.maxWaitTime);
            info.Add("openAutoLoadCallback", extra.openAutoLoadCallback);



            TPNative.Call("loadAd", adUnitId, Json.Serialize(info), loadListenerAdapter);

        }

        public void ShowNativeAd(string adUnitId, string sceneId, string className)
        {
            TPNative.Call("showAd", adUnitId, sceneId,className);

        }

        public bool NativeAdReady(string adUnitId)
        {
            return TPNative.Call<bool>("isReady", adUnitId);
        }

        public void EntryNativeAdScenario(string adUnitId, string sceneId)
        {
            TPNative.Call("entryAdScenario", adUnitId, sceneId);

        }

        public void HideNative(string adUnitId)
        {
            TPNative.Call("hideBanner", adUnitId);

        }

        public void DisplayNative(string adUnitId)
        {
            TPNative.Call("displayBanner", adUnitId);

        }


        public void DestroyNative(string adUnitId)
        {
            TPNative.Call("destroyBanner", adUnitId);

        }

        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
            TPNative.Call("setCustomShowData", adUnitId, Json.Serialize(customAdInfo));

        }

        private class AdLoadListenerAdapter : AndroidJavaProxy
        {

            public AdLoadListenerAdapter() : base("com.tradplus.unity.plugin.nativead.TPNativeListener")
            {
            }

            void onAdLoaded(string unitId, string tpAdInfo)
            {
                PubeasyNativeAndroid.Instance().OnNativeLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdClicked(string unitId, string tpAdInfo)
            {
                PubeasyNativeAndroid.Instance().OnNativeClicked(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdImpression(string unitId, string tpAdInfo)
            {
                PubeasyNativeAndroid.Instance().OnNativeImpression(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }// 展示 1300

            void onAdLoadFailed(string unitId, string error)
            {
                PubeasyNativeAndroid.Instance().OnNativeLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(error));
            }

            void onAdClosed(string unitId, string tpAdInfo)
            {
                PubeasyNativeAndroid.Instance().OnNativeClosed(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 关闭 1400

            void onAdVideoError(string unitId, string tpAdInfo, string error)
            {
                //TradplusNativeAndroid.Instance().videoerror(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdAllLoaded(string unitId, bool b)
            {
                PubeasyNativeAndroid.Instance().OnNativeAllLoaded(unitId, b);

            }

            void oneLayerLoadFailed(string unitId, string tperror, string tpAdInfo)
            {
                PubeasyNativeAndroid.Instance().OnNativeOneLayerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(tperror), (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void oneLayerLoaded(string unitId, string tpAdInfo)
            {
                PubeasyNativeAndroid.Instance().OnNativeOneLayerLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onAdStartLoad(string unitId)
            {
                PubeasyNativeAndroid.Instance().OnNativeStartLoad(unitId, null);

            }

            void oneLayerLoadStart(string unitId, string tpAdInfo)
            {
                PubeasyNativeAndroid.Instance().OnNativeOneLayerStartLoad(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingStart(string unitId, string tpAdInfo)
            {
                PubeasyNativeAndroid.Instance().OnNativeBiddingStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingEnd(string unitId, string tpAdInfo, string tpAdError)
            {
                PubeasyNativeAndroid.Instance().OnNativeBiddingEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), (Dictionary<string, object>)Json.Deserialize(tpAdError));

            }

            void onAdIsLoading(string unitId)
            {
                PubeasyNativeAndroid.Instance().OnNativeIsLoading(unitId);

            }

            void onDownloadStart(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                PubeasyNativeAndroid.Instance().OnDownloadStart(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadUpdate(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName, int progress)
            {
                PubeasyNativeAndroid.Instance().OnDownloadUpdate(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName, progress);
            }

            void onDownloadPause(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                PubeasyNativeAndroid.Instance().OnDownloadPause(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFinish(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                PubeasyNativeAndroid.Instance().OnDownloadFinish(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFail(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                PubeasyNativeAndroid.Instance().OnDownloadFailed(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onInstallled(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                PubeasyNativeAndroid.Instance().OnInstalled(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }
        }

        public PubeasyNativeAndroid()
        {
        }

        public event Action<string, Dictionary<string, object>> OnNativeLoaded;

        public event Action<string, Dictionary<string, object>> OnNativeLoadFailed;

        public event Action<string, Dictionary<string, object>> OnNativeImpression;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeShowFailed;

        public event Action<string, Dictionary<string, object>> OnNativeClicked;

        public event Action<string, Dictionary<string, object>> OnNativeClosed;

        public event Action<string, Dictionary<string, object>> OnNativeStartLoad;

        public event Action<string, Dictionary<string, object>> OnNativeBiddingStart;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeBiddingEnd;

        public event Action<string> OnNativeIsLoading;

        public event Action<string, Dictionary<string, object>> OnNativeOneLayerStartLoad;

        public event Action<string, Dictionary<string, object>> OnNativeOneLayerLoaded;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeOneLayerLoadFailed;

        public event Action<string, Dictionary<string, object>> OnNativeVideoPlayStart;

        public event Action<string, Dictionary<string, object>> OnNativeVideoPlayEnd;

        public event Action<string, bool> OnNativeAllLoaded;

        //国内下载监听
        public event Action<string, Dictionary<string, object>, long, long, string, string> OnDownloadStart;

        public event Action<string, Dictionary<string, object>, long, long, string, string, int> OnDownloadUpdate;

        public event Action<string, Dictionary<string, object>, long, long, string, string> OnDownloadPause;

        public event Action<string, Dictionary<string, object>, long, long, string, string> OnDownloadFinish;

        public event Action<string, Dictionary<string, object>, long, long, string, string> OnDownloadFailed;

        public event Action<string, Dictionary<string, object>, long, long, string, string> OnInstalled;
    }

}