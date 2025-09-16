using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configure
{
    private static Configure _instance;

    public static Configure Instance()
    {
        if (_instance == null)
        {
            _instance = new Configure();
        }
        return _instance;
    }

    private static string appId;
    public string AppId
    {
        get {
            return appId;
        }
    }

    private static string interstitialUnitId;
    public string InterstitialUnitId
    {
        get
        {
            return interstitialUnitId;
        }
    }

    private static string rewardedUnitId;

    public string RewardedUnitId
    {
        get
        {
            return rewardedUnitId;
        }
    }

    private static string offerwallUnitId;

    public string OfferwallUnitId
    {
        get
        {
            return offerwallUnitId;
        }
    }

    private static string bannerUnitId;
    public string BannerUnitId
    {
        get
        {
            return bannerUnitId;
        }
    }

    private static string nativeUnitId;
    public string NativeUnitId
    {
        get
        {
            return nativeUnitId;
        }
    }

    private static string nativeBannerUnitId;
    public string NativeBannerUnitId
    {
        get
        {
            return nativeBannerUnitId;
        }
    }

    private static string interActiveUnitId;
    public string InterActiveUnitId
    {
        get
        {
            return interActiveUnitId;
        }
    }

    private static string splashUnitId;
    public string SplashUnitId
    {
        get
        {
            return splashUnitId;
        }
    }

    public void ShowLog(string logStr)
    {
        Debug.LogWarning(logStr);
    }

    private Dictionary<string, string> mainCustomMap;
    public Dictionary<string, string> MainCustomMap
    {
        get
        {
            return mainCustomMap;
        }
    }

    private bool useAdCustomMap;
    public bool UseAdCustomMap
    {
        get
        {
            return useAdCustomMap;
        }
        set
        {
            useAdCustomMap = value;
            if(useAdCustomMap)
            {
                PlayerPrefs.SetInt("tp.demo.UseAdCustomMap", 1);
            }
            else
            {
                PlayerPrefs.SetInt("tp.demo.UseAdCustomMap", 0);
            }
        }
    }

    private bool simplifyListener;
    public bool SimplifyListener
    {
        get
        {
            return simplifyListener;
        }
        set
        {
            simplifyListener = value;
            if (simplifyListener)
            {
                PlayerPrefs.SetInt("tp.demo.SimplifyListener", 1);
            }
            else
            {
                PlayerPrefs.SetInt("tp.demo.SimplifyListener", 0);
            }
        }
    }

    private static string bannerSceneId;
    public string BannerSceneId
    {
        get
        {
            return bannerSceneId;
        }
    }

    private static string interstitialSceneId;
    public string InterstitialSceneId
    {
        get
        {
            return interstitialSceneId;
        }
    }

    private static string rewardVideoSceneId;
    public string RewardVideoSceneId
    {
        get
        {
            return rewardVideoSceneId;
        }
    }

    private static string nativeSceneId;
    public string NativeSceneId
    {
        get
        {
            return nativeSceneId;
        }
    }

    private static string nativeBannerSceneId;
    public string NativeBannerSceneId
    {
        get
        {
            return nativeBannerSceneId;
        }
    }

    private static string interactiveSceneId;
    public string InterActiveSceneId
    {
        get
        {
            return interactiveSceneId;
        }
    }

    public Configure()
    {
        mainCustomMap = new Dictionary<string, string>();

#if UNITY_IOS
        appId = "0E9CBC7B740B3776E1CCE54D6BA82B46";
        interstitialUnitId = "F12C3A991B46844545DFDDFD830E95AD";
        rewardedUnitId = "B11E1D8624F2DDF12DEF8FC92F35CF69";
        offerwallUnitId = "D40E5B08582582BD7CEB716AF001014C";
        bannerUnitId = "623FCE0A903A2F605569C8BD93225CBB";
        nativeUnitId = "C35635DF42EF4620E18F8F56DF41A09D";
        nativeBannerUnitId = "88BD49596ABC9F70F82FA2C43B0EC54B";
        splashUnitId = "16A71BCD17A9B9F0917232A05F76F276";

        mainCustomMap.Add("user_id", "test_user_id");
        mainCustomMap.Add("user_age", "19");
        mainCustomMap.Add("segment_id", "1571");
        mainCustomMap.Add("bucket_id", "299");
        mainCustomMap.Add("custom_data", "TestIMP");
        mainCustomMap.Add("channel", "tp_channel");
        mainCustomMap.Add("sub_channel", "tp_sub_channel");

        bannerSceneId = "009513B2A78F64";
        interstitialSceneId = "A54829DC948F7D";
        rewardVideoSceneId = "828F88157D28F8";
        nativeSceneId = "2D064EC9EF4106";
        nativeBannerSceneId = "2323113";
#else
        interstitialUnitId = "2CC8169FCBF2A4BCEE9B87FA8567FF6B";
        rewardedUnitId = "FFB99979271F91CFCA427E8E984D153D";
        bannerUnitId = "B176F62CCEBFFB8AF8E86924CEC60BB3";
        nativeUnitId = "2985C8017A674F25B487452E83E8C696";
        nativeBannerUnitId = "9F4D76E204326B16BD42FA877AFE8E7D";
        offerwallUnitId = "EFB33584FC8C2DD33FA241A65862AA47";
        interActiveUnitId = "3E72D3CD57BC9802344FA3D3CA20F31A";
        splashUnitId = "7038FE2F9FC5505329779857159AB623";

        mainCustomMap.Add("user_id", "test_user_id");
        mainCustomMap.Add("user_age", "19");
        mainCustomMap.Add("custom_data", "TestIMP");
        mainCustomMap.Add("channel", "tp_channel");
        mainCustomMap.Add("sub_channel", "tp_sub_channel");

        appId = "6F5EDDDF0DA174AF1ECFAE11AFB9C001";

        bannerSceneId = "123";
        interstitialSceneId = "345";//测试
        rewardVideoSceneId = "567";//测试
        nativeSceneId = "789";//测试
        nativeBannerSceneId = "2333333";
        interactiveSceneId = "22222333";
#endif
    }
}
