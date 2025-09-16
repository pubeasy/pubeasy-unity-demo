using System;
using System.Collections;
using System.Collections.Generic;
using PubeasySDK.Api;
using PubeasySDK.ThirdParty.MiniJSON;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterstitialUI : MonoBehaviour
{
    string adUnitId = Configure.Instance().InterstitialUnitId;
    string sceneId = Configure.Instance().InterstitialSceneId;
    string infoStr = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        float height = (Screen.height - 140) / 9 - 20;
        GUI.skin.button.fixedHeight = height;
        GUI.skin.button.fontSize = (int)(height / 3);
        GUI.skin.label.fontSize = (int)(height / 3);
        GUI.skin.label.fixedHeight = height;

        var rect = new Rect(20, 20, Screen.width - 40, Screen.height);
        rect.y += Screen.safeArea.y;
        GUILayout.BeginArea(rect);
        GUILayout.Space(20);
        if (GUILayout.Button("加载"))
        {
            infoStr = "开始加载";
            TPInterstitialExtra extra = new TPInterstitialExtra();
            #if UNITY_ANDROID

                    extra.isSimpleListener = Configure.Instance().SimplifyListener;
            #endif
            if (Configure.Instance().UseAdCustomMap)
            {
                //流量分组相关
                Dictionary<string, string> customMap = new Dictionary<string, string>();
                customMap.Add("user_id", "test_interstitial_userid");
                customMap.Add("custom_data", "interstitial_TestIMP");
                customMap.Add("segment_tag", "interstitial_segment_tag");
                extra.customMap = customMap;

                Dictionary<string, object> localParams = new Dictionary<string, object>();
                localParams.Add("user_id", "interstitial_userId");
                localParams.Add("custom_data", "interstitial_customData");
                extra.localParams = localParams;
            }
            PubeasyInterstitial.Instance().LoadInterstitialAd(adUnitId, extra);

            Dictionary<string, string> customAdInfo = new Dictionary<string, string>();
            customAdInfo.Add("act", "Load");
            customAdInfo.Add("time", ""+DateTimeOffset.Now);
            PubeasyInterstitial.Instance().SetCustomAdInfo(adUnitId, customAdInfo);
        }
        GUILayout.Space(20);
        if (GUILayout.Button("isReady"))
        {
            bool isReady = PubeasyInterstitial.Instance().InterstitialAdReady(adUnitId);
            infoStr = "isReady: "+ isReady;
        }
        GUILayout.Space(20);
        if (GUILayout.Button("展示"))
        {
            Dictionary<string, string> customAdInfo = new Dictionary<string, string>();
            customAdInfo.Add("act", "Show");
            customAdInfo.Add("time", "" + DateTimeOffset.Now);
            PubeasyInterstitial.Instance().SetCustomAdInfo(adUnitId, customAdInfo);

            infoStr = "";
            PubeasyInterstitial.Instance().ShowInterstitialAd(adUnitId, sceneId);
        }
        GUILayout.Space(20);
        GUILayout.Label(infoStr);
        GUILayout.Space(20);
        if (GUILayout.Button("进入广告场景"))
        {
            PubeasyInterstitial.Instance().EntryInterstitialAdScenario(adUnitId, sceneId);
        }
        GUILayout.Space(20);
        if (GUILayout.Button("返回首页"))
        {
            SceneManager.LoadScene("Main");
        }

        GUILayout.EndArea();
    }

    //回调设置
    private void OnEnable()
    {
        //常用
        PubeasyInterstitial.Instance().OnInterstitialLoaded += OnlLoaded;
        PubeasyInterstitial.Instance().OnInterstitialLoadFailed += OnLoadFailed;
        PubeasyInterstitial.Instance().OnInterstitialImpression += OnImpression;
        PubeasyInterstitial.Instance().OnInterstitialShowFailed += OnShowFailed;
        PubeasyInterstitial.Instance().OnInterstitialClicked += OnClicked;
        PubeasyInterstitial.Instance().OnInterstitialClosed += OnClosed;
        PubeasyInterstitial.Instance().OnInterstitialOneLayerLoadFailed += OnOneLayerLoadFailed;
        //
        PubeasyInterstitial.Instance().OnInterstitialStartLoad += OnStartLoad;
        PubeasyInterstitial.Instance().OnInterstitialBiddingStart += OnBiddingStart;
        PubeasyInterstitial.Instance().OnInterstitialBiddingEnd += OnBiddingEnd;
        PubeasyInterstitial.Instance().OnInterstitialOneLayerStartLoad += OnOneLayerStartLoad;
        PubeasyInterstitial.Instance().OnInterstitialOneLayerLoaded += OnOneLayerLoaded;
        PubeasyInterstitial.Instance().OnInterstitialVideoPlayStart += OnVideoPlayStart;
        PubeasyInterstitial.Instance().OnInterstitialVideoPlayEnd += OnVideoPlayEnd;
        PubeasyInterstitial.Instance().OnInterstitialAllLoaded += OnAllLoaded;

#if UNITY_ANDROID

        PubeasyInterstitial.Instance().OnDownloadStart += OnDownloadStart;
        PubeasyInterstitial.Instance().OnDownloadUpdate += OnDownloadUpdate;
        PubeasyInterstitial.Instance().OnDownloadFinish += OnDownloadFinish;
        PubeasyInterstitial.Instance().OnDownloadFailed += OnDownloadFailed;
        PubeasyInterstitial.Instance().OnDownloadPause += OnDownloadPause;
        PubeasyInterstitial.Instance().OnInstalled += OnInstallled;
#endif
    }

    private void OnDestroy()
    {
        PubeasyInterstitial.Instance().OnInterstitialLoaded -= OnlLoaded;
        PubeasyInterstitial.Instance().OnInterstitialLoadFailed -= OnLoadFailed;
        PubeasyInterstitial.Instance().OnInterstitialImpression -= OnImpression;
        PubeasyInterstitial.Instance().OnInterstitialShowFailed -= OnShowFailed;
        PubeasyInterstitial.Instance().OnInterstitialClicked -= OnClicked;
        PubeasyInterstitial.Instance().OnInterstitialClosed -= OnClosed;
        PubeasyInterstitial.Instance().OnInterstitialOneLayerLoadFailed -= OnOneLayerLoadFailed;

        PubeasyInterstitial.Instance().OnInterstitialStartLoad -= OnStartLoad;
        PubeasyInterstitial.Instance().OnInterstitialBiddingStart -= OnBiddingStart;
        PubeasyInterstitial.Instance().OnInterstitialBiddingEnd -= OnBiddingEnd;
        PubeasyInterstitial.Instance().OnInterstitialOneLayerStartLoad -= OnOneLayerStartLoad;
        PubeasyInterstitial.Instance().OnInterstitialOneLayerLoaded -= OnOneLayerLoaded;
        PubeasyInterstitial.Instance().OnInterstitialVideoPlayStart -= OnVideoPlayStart;
        PubeasyInterstitial.Instance().OnInterstitialVideoPlayEnd -= OnVideoPlayEnd;
        PubeasyInterstitial.Instance().OnInterstitialAllLoaded -= OnAllLoaded;

#if UNITY_ANDROID

        PubeasyInterstitial.Instance().OnDownloadStart -= OnDownloadStart;
        PubeasyInterstitial.Instance().OnDownloadUpdate -= OnDownloadUpdate;
        PubeasyInterstitial.Instance().OnDownloadFinish -= OnDownloadFinish;
        PubeasyInterstitial.Instance().OnDownloadFailed -= OnDownloadFailed;
        PubeasyInterstitial.Instance().OnDownloadPause -= OnDownloadPause;
        PubeasyInterstitial.Instance().OnInstalled -= OnInstallled;
#endif

    }


    void OnlLoaded(string adunit, Dictionary<string, object> adInfo)
    {
        infoStr = "加载成功";
        Configure.Instance().ShowLog("InterstitialUI OnlLoaded ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnLoadFailed(string adunit, Dictionary<string, object> error)
    {
        infoStr = "加载失败";
        Configure.Instance().ShowLog("InterstitialUI OnLoadFailed ------ adunit:" + adunit + "; error: " + Json.Serialize(error));
    }

    void OnImpression(string adunit, Dictionary<string, object> adInfo)
    {
        infoStr = "";
        Configure.Instance().ShowLog("InterstitialUI OnImpression ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnShowFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("InterstitialUI OnShowFailed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnClicked(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterstitialUI OnClicked ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnClosed(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterstitialUI OnClosed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnStartLoad(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterstitialUI OnStartLoad ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnBiddingStart(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterstitialUI OnBiddingStart ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnBiddingEnd(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("InterstitialUI OnBiddingEnd ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnOneLayerStartLoad(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterstitialUI OnOneLayerStartLoad ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnOneLayerLoaded(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterstitialUI OnOneLayerLoaded ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnOneLayerLoadFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("InterstitialUI OnOneLayerLoadFailed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnVideoPlayStart(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterstitialUI OnVideoPlayStart ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnVideoPlayEnd(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterstitialUI OnVideoPlayEnd ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnAllLoaded(string adunit, bool isSuccess)
    {
        Configure.Instance().ShowLog("InterstitialUI OnAllLoaded ------ adunit:" + adunit + "; isSuccess: " + isSuccess);
    }

    void OnDownloadStart(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("InterstitialUI OnDownloadStart ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadUpdate(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName, int progress)
    {
        Configure.Instance().ShowLog("InterstitialUI OnDownloadUpdate ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "; progress:" + progress);
    }

    void OnDownloadPause(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("InterstitialUI OnDownloadPause ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadFinish(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("InterstitialUI OnDownloadFinish ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadFailed(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("InterstitialUI OnDownloadFailed ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnInstallled(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("InterstitialUI OnInstallled ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }
}