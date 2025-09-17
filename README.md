## Unity3D Access Guide

### Unity Plugin Download

| SDK name | Pubeasy SDK |
| --- | --- |
| Developer | BeiJIng Pubeasy Network Technology Co., Ltd. |
| Features | 1. Provide pull and display functions for third-party advertising alliance SDK ads   2. Provide advertising pulling and display functions, and connected developers can obtain advertising revenue. |


### Download URL


| **<font style="background-color:rgb(245, 246, 247);">Version Code</font>** | **<font style="background-color:rgb(245, 246, 247);">Download URL</font>** | **<font style="background-color:rgb(245, 246, 247);">Date</font>** |
| --- | --- | --- |
| 0.1 | [unity_plugin-0.1.zip](https://www.yuque.com/attachments/yuque/0/2025/zip/57821747/1758012924971-6997654a-2b89-4b05-b901-249a0e1d6adb.zip) | 2025-08-14 |
| | | |

### Basic Integration Instructions

#### Plugin Integration Instructions
# <font style="color:rgb(28, 32, 36);">Plugin 集成说明</font>
## <font style="color:rgb(28, 32, 36);">下载Pubeasy Plugin并导入</font>
+ <font style="color:rgb(28, 32, 36);">将 PubeasySDK.unitypackage 导入项目</font>

#### SDK Initialization
**Before creating any advertising slot object, you must first call the initialization Pubeasy SDK.**

## API
+ Initialize Pubeasy in the game startup class: <font style="color:#dd0000;">otherwise the background cannot accurately count DAU.</font>

| Method | Description |
| --- | --- |
| `PubeasyAds.Instance().InitSDK("appId")` | appId：The application ID created by Pubeasy background. Just call it once.   Get the location in Pubeasy backend **Apps & Ad Units -> Add App** |


+ Callback

| Method | Parameter | Description |
| --- | --- | --- |
| `PubeasyAds.Instance().OnInitFinish` | bool success | Initialization end callback.   success:Whether the initialization is successful, true means the initialization is successful. |


## Other API
| Method | Description |
| --- | --- |
| PubeasyAds.Instance().SetCustomMap(Dictionary customMap) | Set global traffic grouping.   Called before initializing the SDK. |
| `Dictionary<string, object> settingMap = new Dictionary<string, object>();`<br/>   `settingMap.Add("oaid","oaid obtained by the developer");`<br/>   `PubeasyAds.Instance().SetSettingDataParam(settingMap);` | Set global OAID.   Called before requesting an ad.   V10.2.0.1 starts to support (Android only) |


## Code Example
```csharp
using PubeasySDK.Api;

void Start()
{
	// Add initialization callback monitoring
	PubeasyAds.Instance().OnInitFinish += OnInitFinish;
	
	// Set traffic grouping data
	Dictionary<string, string> customMap = Configure.Instance().MainCustomMap;
	PubeasyAds.Instance().SetCustomMap(customMap);
	
	//Initialize SDK
	PubeasyAds.Instance().InitSDK(appId);
}

void OnInitFinish(bool success)
{
// Initialization is complete, it is recommended to request advertisements after this callback
}
// Remove listening
PubeasyAds.Instance().OnInitFinish -= OnInitFinish;
```



#### Rewarded Ads
### **1.Request Ads**
```csharp
using PubeasySDK.Api;

// traffic group
Dictionary<string, string> customMap = {};
// local custom map, only for Android
Dictionary<string, string> localParams = {};

// set additional parameter extra
TPRewardVideoExtra extra = new TPRewardVideoExtra();
extra.customMap = customMap;
extra.localParams = localParams;
extra.userId = "rewardVideo_userId";
extra.customData = "rewardVideo_customData";
extra.openAutoLoadCallback = false;

// request ad
PubeasyRewardVideo.Instance().LoadRewardVideoAd("Ad unit ID created on the TP platform",extra);
```

**Parameter Description**

##### unitId：Ad unit ID created on the Pubeasy platform.
+ The developer needs to fill in correctly, for example, if there are spaces before and after the unitId setting, it will cause the ad to fail to request due to failure to retrieve the configuration.

### **2.Checking for Available Ads**
+ It is recommended to call this API to check if there are available ads before displaying an ad. Only call the show method if there are ads available.
+ true indicates that there are available ads, false indicates that there are no available ads at the moment.

```csharp
bool isReady = PubeasyRewardVideo.Instance().RewardVideoAdReady("Ad unit ID created on the TP platform");
```

### **3.Entering Ad Scene (Optional)**
```csharp
PubeasyRewardVideo.Instance().EntryRewardVideoAdScenario("Ad unit ID created on the TP platform", "sceneId");
```

**Parameter Description**

##### sceneId ：Ad Scene ID
+ Developers can create an ad scene in Pubeasy backend. The location is as follows: Application Management - Advertising Scenes.
+ When entering the advertising scene, pass in the sceneId. The sceneId must also be passed in when displaying the ad, otherwise it will affect statistics.

### **4.Display Ads**
```csharp
// Check if there is an ad before calling show
bool isReady = PubeasyRewardVideo.Instance().RewardVideoAdReady("Ad unit ID created on the TP platform");
if(isReady)
{
	// Display Ads
	PubeasyRewardVideo.Instance().ShowRewardVideoAd("Ad unit ID created on the TP platform", "sceneId");
}
```

### **5.Listen for Callbacks**
**Parameter Description**

+ adInfo: Ad unit ID, third-party ad platform, eCPM, and other information. Please refer to [<font style="color:#117CEE;">Android Callback Information</font>](https://www.yuque.com/keyness/sdk/kwg1uzhhh7d3glx2) and [<font style="color:#117CEE;">iOS Callback Information</font>](https://www.yuque.com/keyness/sdk/pwuyex55cgit8np9).

#### **Common Callbacks**
```csharp
// Ad loaded successfully
PubeasyRewardVideo.Instance().OnRewardVideoLoaded += OnlLoaded;
// Ad loading failed
PubeasyRewardVideo.Instance().OnRewardVideoLoadFailed += OnLoadFailed;
// Ad displayed successfully
PubeasyRewardVideo.Instance().OnRewardVideoImpression += OnImpression;
// Ad display failed
PubeasyRewardVideo.Instance().OnRewardVideoShowFailed += OnShowFailed;
// Ad clicked
PubeasyRewardVideo.Instance().OnRewardVideoClicked += OnClicked;
// Ad closed
PubeasyRewardVideo.Instance().OnRewardVideoClosed += OnClosed;
// Ad Reward
PubeasyRewardVideo.Instance().OnRewardVideoReward += OnReward;
// Callback when each layer of waterfall fails to load
PubeasyRewardVideo.Instance().OnRewardVideoOneLayerLoadFailed += OnOneLayerLoadFailed;

void OnlLoaded(string adunit, Dictionary<string, object> adInfo)
{
	// Ad loaded successfully
	// v1.1.2 optimizes the callback method. One loadAd corresponds to one loaded callback. If it is not called, there will be no callback.
}

void OnLoadFailed(string adunit, Dictionary<string, object> error)
{
	// Ad loading failed
}

void OnImpression(string adunit, Dictionary<string, object> adInfo)
{
	// Ad display successful
}

void OnShowFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
{
	// Ad display failed
}

void OnClicked(string adunit, Dictionary<string, object> adInfo)
{
	// Ad click
}

void OnClosed(string adunit, Dictionary<string, object> adInfo)
{
	// Ad close
}

void OnReward(string adunit, Dictionary<string, object> adInfo)
{
	// Ad Reward
}

void OnOneLayerLoadFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
{
	// Callback when each layer of waterfall fails to load
}
```

#### **Ad Source Dimension Callback Listener (Optional)**
```csharp
// Callback returned each time the load method is called
PubeasyRewardVideo.Instance().OnRewardVideoStartLoad += OnStartLoad;
// Bidding start (called once for each bidding ad source)
PubeasyRewardVideo.Instance().OnRewardVideoBiddingStart += OnBiddingStart;
// Bidding end (called once for each bidding ad source)
PubeasyRewardVideo.Instance().OnRewardVideoBiddingEnd += OnBiddingEnd;
// Called when each layer of the waterfall starts loading
PubeasyRewardVideo.Instance().OnRewardVideoOneLayerStartLoad += OnOneLayerStartLoad;
// Called when each layer of the waterfall has successfully loaded
PubeasyRewardVideo.Instance().OnRewardVideoOneLayerLoaded += OnOneLayerLoaded;
// Called when the video playback starts
PubeasyRewardVideo.Instance().OnRewardVideoPlayStart += OnVideoPlayStart;
// Called when the video playback ends
PubeasyRewardVideo.Instance().OnRewardVideoPlayEnd += OnVideoPlayEnd;
// Called when the loading process ends
PubeasyRewardVideo.Instance().OnRewardVideoAllLoaded += OnAllLoaded;
//If this callback is received after calling load, it means that the ad space is still being loaded and cannot trigger a new round of ad loading. Added in V1.0.5
PubeasyRewardVideo.Instance().OnRewardVideoIsLoading += OnAdIsLoading;

void OnStartLoad(string adunit, Dictionary<string, object> adInfo)
{
	// The callback returned each time the load method is called
}

void OnBiddingStart(string adunit, Dictionary<string, object> adInfo)
{
	// Bidding start (called once for each bidding ad source)
}

void OnBiddingEnd(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
{
	// Bidding end (called once for each bidding ad source)
}

void onAdIsLoading(string unitId)
{
	// If this callback is received after calling load, it means that the ad space is still being loaded and cannot trigger a new round of ad loading. Added in V1.0.5
}

void OnOneLayerStartLoad(string adunit, Dictionary<string, object> adInfo)
{
	// Called when each layer of the waterfall starts loading
}

void OnOneLayerLoaded(string adunit, Dictionary<string, object> adInfo)
{
	// Called when each layer of the waterfall has successfully loaded
}

void OnVideoPlayStart(string adunit, Dictionary<string, object> adInfo)
{
	// Called when the video playback starts
}

void OnVideoPlayEnd(string adunit, Dictionary<string, object> adInfo)
{
	// Called when the video playback ends
}

void OnAllLoaded(string adunit, bool isSuccess)
{
	// Called when the loading process ends
	// isSuccess is true if at least one ad source has successfully loaded ads for the ad unit ID
    // isSuccess is false if all ad sources have failed to load ads for the ad unit ID
}
```



#### Interstitials
### **1.Request Ads**
```csharp
using PubeasySDK.Api;

// traffic group
Dictionary<string, string> customMap = {};
// local custom map, only for Android
Dictionary<string, string> localParams = {};

// set additional parameter extra
TPInterstitialExtra extra = new TPInterstitialExtra();
extra.customMap = customMap;
extra.localParams = localParams;
extra.openAutoLoadCallback = false;

// request ad
PubeasyInterstitial.Instance().LoadInterstitialAd("Ad unit ID created on the TP platform", extra);
```

**Parameter Description**

##### unitId：Ad unit ID created on the Pubeasy platform
+ The developer needs to fill in correctly, for example, if there are spaces before and after the unitId setting, it will cause the ad to fail to request due to failure to retrieve the configuration.



### **2.Checking for Available Ads**
+ It is recommended to call this API to check if there are available ads before displaying an ad. Only call the show method if there are ads available.
+ true indicates that there are available ads, false indicates that there are no available ads at the moment.

```csharp
bool isReady = PubeasyInterstitial.Instance().InterstitialAdReady("Ad unit ID created on the TP platform");
```

### **3.Entering Ad Scene (Optional)**
```csharp
PubeasyInterstitial.Instance().EntryInterstitialAdScenario("Ad unit ID created on the TP platform", "sceneId");
```

**Parameter Description**

##### sceneId ：Ad Scene ID
+ Developers can create an ad scene in Pubeasy backend. The location is as follows: Application Management - Advertising Scenes.
+ When entering the advertising scene, pass in the sceneId. The sceneId must also be passed in when displaying the ad, otherwise it will affect statistics.

### **4.Display Ads**
```csharp
// Check if there is an ad before calling show
bool isReady = PubeasyInterstitial.Instance().InterstitialAdReady("Ad unit ID created on the TP platform");
if(isReady)
{
	// Display Ads
	PubeasyInterstitial.Instance().ShowInterstitialAd("Ad unit ID created on the TP platform", "sceneId");
}
```

### **5.Listen for Callbacks**
**Parameter Description**

+ adInfo: Ad unit ID, third-party ad platform, eCPM, and other information. Please refer to [<font style="color:#117CEE;">Android Callback Information</font>](https://www.yuque.com/keyness/sdk/kwg1uzhhh7d3glx2) and [<font style="color:#117CEE;">iOS Callback Information</font>](https://www.yuque.com/keyness/sdk/pwuyex55cgit8np9).

#### **Common Callbacks**
```csharp
// Ad loaded successfully
PubeasyInterstitial.Instance().OnInterstitialLoaded += OnlLoaded;
// Ad loading failed
PubeasyInterstitial.Instance().OnInterstitialLoadFailed += OnLoadFailed;
// Ad displayed successfully
PubeasyInterstitial.Instance().OnInterstitialImpression += OnImpression;
// Ad display failed
PubeasyInterstitial.Instance().OnInterstitialShowFailed += OnShowFailed;
// Ad clicked
PubeasyInterstitial.Instance().OnInterstitialClicked += OnClicked;
// Ad closed
PubeasyInterstitial.Instance().OnInterstitialClosed += OnClosed;
// Callback when each layer of waterfall fails to load
PubeasyInterstitial.Instance().OnInterstitialOneLayerLoadFailed += OnOneLayerLoadFailed;

void OnlLoaded(string adunit, Dictionary<string, object> adInfo)
{
	// Ad loaded successfully
	// v1.1.2 optimizes the callback method. One loadAd corresponds to one loaded callback. If it is not called, there will be no callback.
}

void OnLoadFailed(string adunit, Dictionary<string, object> error)
{
	// Ad loading failed
}

void OnImpression(string adunit, Dictionary<string, object> adInfo)
{
	// Ad display successful
}

void OnShowFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
{
	// Ad display failed
}

void OnClicked(string adunit, Dictionary<string, object> adInfo)
{
	// Ad click
}

void OnClosed(string adunit, Dictionary<string, object> adInfo)
{
	// Ad close
}

void OnOneLayerLoadFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
{
	// Callback when each layer of waterfall fails to load
}
```

#### **Ad Source Dimension Callback Listener (Optional)**
```csharp
// Callback returned each time the load method is called
PubeasyInterstitial.Instance().OnInterstitialStartLoad += OnStartLoad;
// Bidding start (called once for each bidding ad source)
PubeasyInterstitial.Instance().OnInterstitialBiddingStart += OnBiddingStart;
// Bidding end (called once for each bidding ad source)
PubeasyInterstitial.Instance().OnInterstitialBiddingEnd += OnBiddingEnd;
// Called when each layer of the waterfall starts loading
PubeasyInterstitial.Instance().OnInterstitialOneLayerStartLoad += OnOneLayerStartLoad;
// Called when each layer of the waterfall has successfully loaded
PubeasyInterstitial.Instance().OnInterstitialOneLayerLoaded += OnOneLayerLoaded;
// Called when the video playback starts
PubeasyInterstitial.Instance().OnInterstitialVideoPlayStart += OnVideoPlayStart;
// Called when the video playback ends
PubeasyInterstitial.Instance().OnInterstitialVideoPlayEnd += OnVideoPlayEnd;
// Loading process completed
PubeasyInterstitial.Instance().OnInterstitialAllLoaded += OnAllLoaded;
// If this callback is received after calling load, it means that the ad space is still being loaded and cannot trigger a new round of ad loading. Added in V1.0.5
PubeasyInterstitial.Instance().OnInterstitialIsLoading += OnAdIsLoading;

void OnStartLoad(string adunit, Dictionary<string, object> adInfo)
{
	// The callback returned each time the load method is called
}

void OnBiddingStart(string adunit, Dictionary<string, object> adInfo)
{
	// Bidding start (called once for each bidding ad source)
}

void OnBiddingEnd(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
{
	// Bidding end (called once for each bidding ad source)
}

void onAdIsLoading(string unitId)
{
    // If this callback is received after calling load, it means that the ad space is still being loaded and cannot trigger a new round of ad loading. Added in V1.0.5
}

void OnOneLayerStartLoad(string adunit, Dictionary<string, object> adInfo)
{
	// Called when each layer of the waterfall starts loading
}

void OnOneLayerLoaded(string adunit, Dictionary<string, object> adInfo)
{
	// Called when each layer of the waterfall has successfully loaded
}

void OnVideoPlayStart(string adunit, Dictionary<string, object> adInfo)
{
	// Called when the video playback starts
}

void OnVideoPlayEnd(string adunit, Dictionary<string, object> adInfo)
{
	// Called when the video playback ends
}

	
void OnAllLoaded(string adunit, bool isSuccess)
{
	// Loading process completed
	// isSuccess is true if at least one ad source has successfully loaded ads for the ad unit ID
    // isSuccess is false if all ad sources have failed to load ads for the ad unit ID
}
```



#### Native Ads
### **1. Request Ad**
```csharp
using PubeasySDK.Api;

// traffic group
Dictionary<string, string> customMap = {};
// local custom map, only for Android
Dictionary<string, string> localParams = {};

// set additional parameter extra
TPNativeExtra extra = new TPNativeExtra();
extra.x = 0;
extra.y = 0;
extra.width = 320;
extra.height = 200;
extra.adPosition = PubeasyBase.AdPosition.TopLeft;
extra.customMap = customMap;
extra.localParams = localParams;
extra.openAutoLoadCallback = false;

// request ad
PubeasyNative.Instance().LoadNativeAd("Ad unit ID created on the TP platform", extra);
```

**Parameter Description**

##### unitId: Ad unit ID created on the Pubeasy platform.
+ The developer needs to fill in correctly, for example, if there are spaces before and after the unitId setting, it will cause the ad to fail to request due to failure to retrieve the configuration.

##### TPNativeExtra: Extra Parameters
+ x: Coordinate x, default 0.
+ y: Coordinate y, default 0.
+ width: Width, default 320.
+ height: Height, default 200.
+ adPosition: Screen position positioning (effective when x and y are both 0), default TopLeft.
+ localParams: Set local parameters. **Only for Android**. Some ad platforms require special parameters to be set.

### **2. Check for Available Ads**
+ It is recommended that developers call this API to determine whether there are available ads before displaying the ad. Only when there are ads, call the show method.
+ True means there is an available ad, and false means there are no ads available temporarily.

```csharp
bool isReady = PubeasyNative.Instance().NativeAdReady("Ad unit ID on TP platform");
```

### **3. Enter Ad Scene (Optional)**
```csharp
PubeasyNative.Instance().EntryNativeAdScenario("Ad unit ID created on the TP platform", "sceneId");
```

**Parameter Description**

##### sceneId: Ad Scene ID
+ Developers can create an ad scene in Pubeasy backend. The location is as follows: Application Management - Advertising Scenes.
+ When entering the advertising scene, pass in the sceneId. The sceneId must also be passed in when displaying the ad, otherwise it will affect statistics.

### **4. Display Ads**
```csharp
// Call show after checking whether there are any ads
bool isReady = PubeasyNative.Instance().NativeAdReady("unitId");
if(isReady)
{
	// If there is no style requirement, className can be passed without setting
	PubeasyNative.Instance().ShowNativeAd("unitId", "sceneId","className");
}
```

**Parameter Description**

##### className: Template Name
+ Developers can set the standard native rendering template through this parameter, and the plugin default template will be used for rendering by default.
+ The iOS default template is located in the Assets/Plugins/IOS directory (TPNativeTemplate.h, TPNativeTemplate.m, TPNativeTemplate.xib)
+ The Android default template has been included in the Pubeasy plugin, and developers do not need to reference it again.

### **5. Hide Displayed Ads**
```csharp
PubeasyNative.Instance().HideNative("Ad unit ID created on the TP platform");
```

### **6. Display Hidden Ads**
```csharp
PubeasyNative.Instance().DisplayNative("Ad unit ID created on the TP platform");
```

### **7. Destroy Ads**
```csharp
PubeasyNative.Instance().DestroyNative("Ad unit ID created on the TP platform");
```

### **8. Listen for Callbacks**
**Parameter Description**

+ adInfo: Ad unit ID, third-party ad platform, eCPM, and other information. Please refer to [<font style="color:#117CEE;">Android Callback Information</font>](https://www.yuque.com/keyness/sdk/kwg1uzhhh7d3glx2) and [<font style="color:#117CEE;">iOS Callback Information</font>](https://www.yuque.com/keyness/sdk/pwuyex55cgit8np9).

#### **Common Callbacks**
```csharp
// Ad loaded successfully
PubeasyNative.Instance().OnNativeLoaded += OnlLoaded;
// Ad load failed
PubeasyNative.Instance().OnNativeLoadFailed += OnLoadFailed;
// Ad displayed successfully
PubeasyNative.Instance().OnNativeImpression += OnImpression;
// Ad display failed
PubeasyNative.Instance().OnNativeShowFailed += OnShowFailed;
// Ad click
PubeasyNative.Instance().OnNativeClicked += OnClicked;
// Ad closed
PubeasyNative.Instance().OnNativeClosed += OnClosed;
// Callback when each layer of the waterfall fails to load
PubeasyNative.Instance().OnNativeOneLayerLoadFailed += OnOneLayerLoadFailed;

void OnlLoaded(string adunit, Dictionary<string, object> adInfo)
{
	// Ad loaded successfully
	// v1.1.2 optimized the callback method. One loadAd corresponds to one loaded callback, which will not be called without a call.
}

void OnLoadFailed(string adunit, Dictionary<string, object> error)
{
	// Ad load failed
}

void OnImpression(string adunit, Dictionary<string, object> adInfo)
{
	// Ad displayed successfully
}

void OnShowFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
{
	// Ad display failed
}

void OnClicked(string adunit, Dictionary<string, object> adInfo)
{
	// Ad click
}

void OnClosed(string adunit, Dictionary<string, object> adInfo)
{
	// Ad closed
}

void OnOneLayerLoadFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
{
	// Callback when each layer of the waterfall fails to load
}
```

#### **Ad Source Dimension Callback Listener** (Optional)
```csharp
// Callback returned each time the load method is called
PubeasyNative.Instance().OnNativeStartLoad += OnStartLoad;
// Bidding start (called once for each bidding ad source)
PubeasyNative.Instance().OnNativeBiddingStart += OnBiddingStart;
// Bidding end (called once for each bidding ad source)
PubeasyNative.Instance().OnNativeBiddingEnd += OnBiddingEnd;
// Called when each layer of the waterfall starts loading
PubeasyNative.Instance().OnNativeOneLayerStartLoad += OnOneLayerStartLoad;
// Called when each layer of the waterfall has successfully loaded
PubeasyNative.Instance().OnNativeOneLayerLoaded += OnOneLayerLoaded;
// Called when the video playback starts
PubeasyNative.Instance().OnNativeVideoPlayStart += OnVideoPlayStart;
// Called when the video playback ends
PubeasyNative.Instance().OnNativeVideoPlayEnd += OnVideoPlayEnd;
// Called when the loading process ends
PubeasyNative.Instance().OnNativeAllLoaded += OnAllLoaded;
// If this callback is received after calling load, it means that the ad space is still being loaded and cannot trigger a new round of ad loading. Added in V1.0.5
PubeasyNative.Instance().OnNativeIsLoading += OnAdIsLoading;

void OnStartLoad(string adunit, Dictionary<string, object> adInfo)
{
    // The callback returned each time the load method is called
}

void onAdIsLoading(string unitId)
{
    // If this callback is received after calling load, it means that the ad space is still being loaded and cannot trigger a new round of ad loading. Added in V1.0.5
}

void OnBiddingStart(string adunit, Dictionary<string, object> adInfo)
{
    // Bidding start (called once for each bidding ad source)
}

void OnBiddingEnd(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
{
    // Bidding end (called once for each bidding ad source)
}

void OnOneLayerStartLoad(string adunit, Dictionary<string, object> adInfo)
{
    // Called when each layer of the waterfall starts loading
}

void OnOneLayerLoaded(string adunit, Dictionary<string, object> adInfo)
{
    // Called when each layer of the waterfall has successfully loaded
}

void OnVideoPlayStart(string adunit, Dictionary<string, object> adInfo)
{
    // Called when the video playback starts
}

void OnVideoPlayEnd(string adunit, Dictionary<string, object> adInfo)
{
    // Called when the video playback ends
}

void OnAllLoaded(string adunit, bool isSuccess)
{
    // Called when the loading process ends
    // isSuccess is true if at least one ad source has successfully loaded ads for the ad unit ID, or false if all ad sources have failed to load ads for the ad unit ID.
}
```



#### Banners
### **1. Request Ad**
```csharp
using PubeasySDK.Api;

// Traffic segmentation
Dictionary<string, string> customMap = {};
// local custom Map, Android only
Dictionary<string, string> localParams = {};

// Set additional parameter extra
TPBannerExtra extra = new TPBannerExtra();
extra.x = 0;
extra.y = 0;
extra.width = 320;
extra.height = 50;
extra.closeAutoShow = false;
extra.adPosition = PubeasyBase.AdPosition.TopLeft;
extra.customMap = customMap;
extra.localParams = localParams;
extra.className = "tp_native_banner_ad_unit";

// Request Ad
PubeasyBanner.Instance().LoadBannerAd("Ad unit ID created on TP platform", sceneId, extra);
```

**Parameter Description**

##### unitId: Ad unit ID created on Pubeasy backend
+ Developers need to fill in correctly, for example: if there are spaces before and after unitId, it will cause the advertising request to fail due to failure to pull configuration.

##### sceneId: Ad scene ID
+ Developers can create it in Pubeasy backend, located as follows: App Management-Ad Scene.
+ Enter sceneId when entering the ad scene, and also need to pass sceneId when displaying the ad, otherwise it will affect statistics.

##### TPBannerExtra: Additional parameters
+ x: Coordinate x, default is 0.
+ y: Coordinate y, default is 0.
+ width: Width, default is 320.
+ height: Height, default is 50.
+ adPosition: Screen position positioning (effective when both x and y are 0), default TopLeft.
+ closeAutoShow: Whether to close automatic display. Automatic display is enabled by default, and true is passed to turn off.
+ localParams: Set local parameters. **Android only**. Some advertising platforms require special parameters to be set.
+ className: Set the native banner using a custom rendering method. Developers can pass in the layout name. **Android only**, added in V8.7.0.1 API  
(1) From Android V8.7.0.1, native banner types can be configured using banner ad unit settings, and the SDK has built-in default layout styles. If developers need to define their own layout styles, they need to use this method for rendering.  
(2) For more information, see the section "How to Use Banner Ad Units to Configure Native Banners" below.  
(3) tp_native_banner_ad_unit is the style layout file provided by Pubeasy and can be obtained in the downloaded Unity package.

### **2. Check for Available Ads**
+ It is recommended that developers call this API to determine if there is an available ad before showing the ad. The show method is called only when there is an ad available.
+ true means there is an available ad, false means there is no available ad temporarily.

```csharp
bool isReady = PubeasyBanner.Instance().BannerAdReady("Ad unit ID created on TP platform");
```

### **3. Enter the ad scene (optional)**
```csharp
PubeasyBanner.Instance().EntryBannerAdScenario("Ad unit ID created on TP platform", "sceneId");
```

### **4. Display Ads**
This interface is used in conjunction with closeAutoShow to turn off automatic display

```csharp
// Check if there is an ad before displaying it
bool isReady = PubeasyBanner.Instance().BannerAdReady("Ad unit ID created on TP platform");
if(isReady)
{
	// Display Ad
	PubeasyBanner.Instance().ShowBannerAd("Ad unit ID created on TP platform", "sceneId");
}
```

### **5. Hide Displayed Ads**
```csharp
PubeasyBanner.Instance().HideBanner("Ad unit ID created on TP platform");
```

### **6. Display Hidden Ads**
```csharp
PubeasyBanner.Instance().DisplayBanner("Ad unit ID created on TP platform");
```

### **7. Destroy Ads**
```csharp
// Destroy Ad
PubeasyBanner.Instance().DestroyBanner("Ad unit ID created on TP platform");
```

### **8. Listen for callbacks**
**Parameter Description**

+ adInfo: Ad unit ID, third-party ad platform, eCPM, and other information. Please refer to [<font style="color:#117CEE;">Android Callback Information</font>](https://www.yuque.com/keyness/sdk/kwg1uzhhh7d3glx2) and [<font style="color:#117CEE;">iOS Callback Information</font>](https://www.yuque.com/keyness/sdk/pwuyex55cgit8np9).

#### **Common Callbacks**
```csharp
// Banner loaded successfully
PubeasyBanner.Instance().OnBannerLoaded += OnlLoaded;
// Banner load failed
PubeasyBanner.Instance().OnBannerLoadFailed += OnLoadFailed;
// Banner impression (display) successful
PubeasyBanner.Instance().OnBannerImpression += OnImpression;
// Banner display failed
PubeasyBanner.Instance().OnBannerShowFailed += OnShowFailed;
// Banner clicked
PubeasyBanner.Instance().OnBannerClicked += OnClicked;
// Banner closed
PubeasyBanner.Instance().OnBannerClosed += OnClosed;
// Callback when each layer of waterfall fails to load
PubeasyBanner.Instance().OnBannerOneLayerLoadFailed += OnOneLayerLoadFailed;

void OnlLoaded(string adunit, Dictionary<string, object> adInfo)
{
   // Banner loaded successfully
   // Starting from v1.1.2, the callback method is optimized. One loadAd corresponds to one loaded callback, and it will not be called if not used.
}

void OnLoadFailed(string adunit, Dictionary<string, object> error)
{
   // Banner load failed
}

void OnImpression(string adunit, Dictionary<string, object> adInfo)
{
   // Banner impression (display) successful
}

void OnShowFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
{
   // Banner display failed
}

void OnClicked(string adunit, Dictionary<string, object> adInfo)
{
   // Banner clicked
}

void OnClosed(string adunit, Dictionary<string, object> adInfo)
{
   // Banner closed
}

void OnOneLayerLoadFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
{
   // Callback when each layer of the waterfall fails to load
}
```





#### SplashAds
## Prerequisites
+ Android V10.0.1.1 / iOS 9.7.0 (Unity plugin V1.3.3) supports splash ad integration.
+ When using multiple caches, developers need to control the consecutive display.
+ Developers need to configure the splash activity in the manifest file.

```java
<activity
    android:name="com.tradplus.unity.plugin.splash.TPSplashShowActivity"
    android:screenOrientation="portrait"
    android:theme="@android:style/Theme.NoTitleBar.Fullscreen" />
```

### **1. Request Ads**
```csharp
using PubeasySDK.Api;

// Traffic segmentation
Dictionary<string, string> customMap = {};
// Local custom Map, Android only
Dictionary<string, string> localParams = {};

// Set additional parameter extra
TPSplashExtra extra = new TPSplashExtra();
extra.customMap = customMap;
extra.localParams = localParams;
extra.openAutoLoadCallback = false;

// Request ads
PubeasySplash.Instance().LoadSplashAd("Ad unit ID created on TP platform", extra);
```

**Parameter Description**

##### unitId: Ad unit ID created on Pubeasy backend
+ Developers need to fill in correctly, for example: if there are spaces before and after unitId, the ad request will fail because the configuration cannot be retrieved.



### **2. Check for Available Ads**
+ It is recommended to call this API to check if there are available ads before displaying an ad. Only call the show method if there are ads available.
+ true indicates that there are available ads, false indicates that there are no available ads at the moment.

```csharp
bool isReady = PubeasySplash.Instance().SplashAdReady("Ad unit ID created on TP platform");
```

### **3. Enter Ad Scene**
```csharp
PubeasySplash.Instance().EntrySplashAdScenario("Ad unit ID created on TP platform", "sceneId");
```

**Parameter Description**

##### sceneId: Ad scene ID (recommended)
+ Developers can create it on the Pubeasy backend, located in: App Management - Ad Scenario.
+ When entering the ad scene, pass in the sceneId, and when displaying the ad, sceneId should also be passed in, otherwise it will affect statistics.

### **4. Display Ads**
```csharp
// Check if there is an ad before calling show
bool isReady = PubeasySplash.Instance().SplashAdReady("Ad unit ID created on TP platform");
if(isReady)
{
    // Display the ad
    PubeasySplash.Instance().ShowSplashAd("Ad unit ID created on TP platform", "sceneId");
}
```

### **5. Listen for Callbacks**
**Parameter Description**

+ adInfo: Ad unit ID, third-party ad platform, eCPM, and other information. Please refer to [<font style="color:#117CEE;">Android Callback Information</font>](https://www.yuque.com/keyness/sdk/kwg1uzhhh7d3glx2) and [<font style="color:#117CEE;">iOS Callback Information</font>](https://www.yuque.com/keyness/sdk/pwuyex55cgit8np9).

#### **Common Callbacks**
```csharp
// Ad loaded successfully
PubeasySplash.Instance().OnSplashLoaded += OnlLoaded;
// Ad loading failed
PubeasySplash.Instance().OnSplashLoadFailed += OnLoadFailed;
// Ad displayed successfully
PubeasySplash.Instance().OnSplashImpression += OnImpression;
// Ad display failed
PubeasySplash.Instance().OnSplashShowFailed += OnShowFailed;
// Ad clicked
PubeasySplash.Instance().OnSplashClicked += OnClicked;
// Ad closed
PubeasySplash.Instance().OnSplashClosed += OnClosed;
// Callback when each layer of waterfall fails to load
PubeasySplash.Instance().OnSplashOneLayerLoadFailed += OnOneLayerLoadFailed;

void OnlLoaded(string adunit, Dictionary<string, object> adInfo)
{
    // Ad loaded successfully
    // One loaded callback for each loadAd call, it will not be called if not invoked.
}

void OnLoadFailed(string adunit, Dictionary<string, object> error)
{
    // Ad loading failed
}

void OnImpression(string adunit, Dictionary<string, object> adInfo)
{
    // Ad displayed successfully
}

void OnShowFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
{
    // Ad display failed
}

void OnClicked(string adunit, Dictionary<string, object> adInfo)
{
    // Ad clicked
}

void OnClosed(string adunit, Dictionary<string, object> adInfo)
{
    // Ad closed
}

void OnOneLayerLoadFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
{
    // Callback when each layer of waterfall fails to load
}
```

#### **Ad Source Dimension Callback Listeners** (optional)
```csharp
// Callback returned each time the load method is called
PubeasySplash.Instance().OnSplashStartLoad += OnStartLoad;
// Bidding starts (callback once for each bidding ad source)
PubeasySplash.Instance().OnSplashBiddingStart += OnBiddingStart;
// Bidding loading ends (callback once for each bidding ad source)
PubeasySplash.Instance().OnSplashBiddingEnd += OnBiddingEnd;
// Callback when each layer of waterfall starts loading
PubeasySplash.Instance().OnSplashOneLayerStartLoad += OnOneLayerStartLoad;
// Callback when each layer of waterfall is loaded successfully
PubeasySplash.Instance().OnSplashOneLayerLoaded += OnOneLayerLoaded;
// Video playback starts
PubeasySplash.Instance().OnSplashVideoPlayStart += OnVideoPlayStart;
// Video playback ends
PubeasySplash.Instance().OnSplashVideoPlayEnd += OnVideoPlayEnd;
// Load process ends
PubeasySplash.Instance().OnSplashAllLoaded += OnAllLoaded;
// If you receive this callback after calling load, it means that the ad unit is still in the loading state and cannot trigger a new round of ad loading.
PubeasySplash.Instance().OnSplashIsLoading += OnAdIsLoading;

void OnStartLoad(string adunit, Dictionary<string, object> adInfo)
{
    // Callback returned each time the load method is called
}

void OnBiddingStart(string adunit, Dictionary<string, object> adInfo)
{
    // Bidding starts (callback once for each bidding ad source)
}

void OnBiddingEnd(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
{
	// Callback when bidding loading ends (callback once for each bidding ad source)
}

void onAdIsLoading(string unitId)
{
    // If you receive this callback after calling load, it means that the ad unit is still in the loading state and cannot trigger a new round of ad loading.
}

void OnOneLayerStartLoad(string adunit, Dictionary<string, object> adInfo)
{
	// Callback when each layer of waterfall starts loading
}

void OnOneLayerLoaded(string adunit, Dictionary<string, object> adInfo)
{
	// Callback when each layer of waterfall is loaded successfully
}

void OnVideoPlayStart(string adunit, Dictionary<string, object> adInfo)
{
	// Callback when video playback starts
}

void OnVideoPlayEnd(string adunit, Dictionary<string, object> adInfo)
{
	// Callback when video playback ends
}

	
void OnAllLoaded(string adunit, bool isSuccess)
{
	// Callback when the load process ends
	// isSuccess returns true, indicating that there are successful ad sources for this request
    // isSuccess returns false, indicating that all ad sources for adUnitId under this request have failed to load
}
```



### Callback Information

#### iOS
## AdInfo Callback Explanation
Developers can obtain the information of the current ad by setting a callback listener and accessing the adInfo parameter.

Taking rewarded video as an example, the code is as follows:

```plain
// Ad impression success
PubeasyRewardVideo.Instance().OnRewardVideoImpression += OnImpression;
...
void OnImpression(string adunit, Dictionary<string, object> adInfo)
{
    // You can access the following information through the AdInfo object
}
```

#### Setting up a Global Impression Callback for App-level Display
+ To facilitate developers in performing display data statistics, the SDK provides a global impression callback API.
+ Supported versions: Unity plugin version v1.0.4+, iOS v8.5.0+, Android v8.8.0.1+
+ Android Only: Developers need to call the listener for each type in order to receive the GlobalImpressionListener callback. For example, to set the impression callback for rewarded video: `PubeasyRewardVideo.Instance().OnRewardVideoImpression += OnImpression;`

```csharp
PubeasyAds.Instance().AddGlobalAdImpression(OnGlobalAdImpression);

void OnGlobalAdImpression(Dictionary<string, object> adInfo)
{
    // Developers can obtain the impression callbacks of all ad placements through this callback
}
```

#### Field Explanation for Callback Information Returned by Each Interface
| Key | Explanation |
| --- | --- |
| adType | **Added in V7.1.0** Ad type.   native: Standard native ad   native-banner: Native banner   native-splash: Native splash   native-draw: Native draw   banner: Banner   splash: Splash   interstitial: Interstitial   rewarded-video: Rewarded video   offerwall: Offerwall |
| segment_id | **Added in V7.1.0** Traffic segment ID |
| bucket_id | **Added in V7.1.0** A/B testing group ID |
| adunit_id | Ad placement ID created in the Pubeasy backend. |
| true_adunit_id | **Added in V12.1.0** Shared ad slot-specific ad information. Used to record the ad slot where the ad was finally displayed. |
| adsource_placement_id | Ad source ID |
| adNetworkId(v7.8.0+) | Number assigned to the third-party ad network, used to distinguish different ad networks. Please refer to the table of third-party ad network IDs below. |
| adNetworkName(v7.8.0+) | Name of the third-party ad network |
| adSourceId (Added in v11.70) | Third-party ad placement ID。Before v11.70, use `placementid` |
| country_code | Country code |
| ecpm | eCPM in USD (⚠️The eCPM-related fields are of type float, as are the subsequent fields) |
| ecpm_cny | eCPM in CNY |
| ecpm_precision | **Added in V6.5.0**. Get the precision of eCPM.   "publisher_defined": eCPM defined by the developer in the Pubeasy backend;   "estimated": eCPM estimated by Pubeasy based on historical data when the developer enables the auto pricing feature for the ad source (cross-promotion eCPM also belongs to this type);   "exact": Real-time bidding price. ~~When "exact" is returned, developers need to obtain the real-time bidding price through ecpm_exact and ecpm_exact_cny.~~ |
| ~~ecpm_exact~~ | ~~**Added in V6.5.0**~~~~. Real-time bidding price in USD. When ecpm_precision returns "exact",~~~~ ~~~~**please use ecpm_exact and ecpm_exact_cny as the ad prices, which are more accurate than ecpm and ecpm_cny**~~~~.~~   **Deprecated in v7.8.0. Developers can directly obtain it through ecpm.** |
| ~~ecpm_exact_cny~~ | ~~**Added in V6.6.0**~~~~. Real-time bidding price in CNY. When ecpm_precision returns "exact",~~~~ ~~~~**please use ecpm_exact and ecpm_exact_cny as the ad prices, which are more accurate than ecpm and ecpm_cny**~~~~.~~   **Deprecated in v7.8.0. Developers can directly obtain it through ecpm_cny.** |
| ecpm_level | **Added in V6.5.0**. Get the order (priority) of the ad source in the mediation management page in the developer backend.   The default value for bidding ad sources is 0.   For non-bidding ad sources, the manual sorting starts from 1 and increases incrementally. |
| is_adapter_template_render | Whether it is a template type |
| native_ad_type | v6.9.0+. Native ad type. Default: 0 (unknown). The specific type is returned after the native ad is loaded.   0 - Unknown   1 - Self-rendered   2 - Template   3 - Video interstitial   4 - Draw native ad |
| is_bid | Whether it is a bidding ad source |
| is_c2s_bid | **Added in V7.6.0**. Whether it is a C2S Bidding ad source |
| is_auto_load | **Added in V7.6.0**. Whether it is auto-loaded |
| load_time | **Added in V7.6.0**. Loading duration in milliseconds |
| reward_info | Reward information returned by third-party (rewarded video), including advanced rewards from TikTok and Kuaishou (if available) |
| reward_name | Reward item configured in the Pubeasy backend (rewarded video) |
| reward_number | Reward quantity configured in the Pubeasy backend (rewarded video) |
| request_id | Request identifier. A new ID is generated each time the load function is called. It can be used to track the complete lifecycle of ad loading until display completion |
| waterfall_index | The position of the current ad in the waterfall |
| scene_id | Ad scene ID |
| customAdInfo | v8.3.20+. Custom data set by developers before ad display. Developers can set it through the corresponding API of each ad loading class. The custom data will be returned in relevant callbacks after ad display |
| isNative | v8.4.0+. Whether it is a native ad. From v8.4.0+, banners and splash ads support the mixed use of native ads. Developers can determine whether an ad is a native ad based on this field |
| isBottom | v8.5.0+. Whether it is a backup ad |
| placement_ad_type | v8.7.0+. Ad source ad type.   From v8.4.0+, banners and splash ads support the mixed use of native ads. From v8.7.0+, interstitial ads support the mixed use of splash ads. Developers can obtain the mixed ad types through this field.   1 - Native   2 - Interstitial   3 - Splash   4 - Banner   5 - Rewarded Video   6 - Offerwall |
| impPaidData | Impression-level revenue data returned by AdMob (v9.5.0+)   Note: The following fields are included in the data:   paid_valueMicros: value   paid_currencycode: Currency code   paid_precision: Precision    |
| impressionId | **Added in v9.6.0** Unique identifier for each ad impression |
| video_protoco | **Added in v10.0.0** Video protocol Type 1:vast, 2:vamp |
| banner_w | **Added in v10.3.0** The width of Banner configured in the Pubeasy platform. |
| banner_h | **Added in v10.3.0** The height of Banner configured in the Pubeasy platform. |




#### Android
## Android callback information
Developers can set callback listeners to obtain the current ad information through the parameter TPAdInfo.

Taking the incentive video type as an example, the code is as follows:

#### V8501 and below
```csharp
private void OnRewardedVideoAdImpression(string tpAdInfo)
{
	Debug.Log("onRewardedVideoAdImpression: " + tpAdInfo);
}
```

The returned information is as follows. Developers can directly transfer json to obtain the corresponding content:

```csharp
I/Unity: onRewardedVideoAdImpression : {"adNetworkId":"2","adSourceId":"ca-app-pub-3940256099942544/1033173712","adSourceName":"admob","adUnitId":"788E1FCB278B0D7E97282231154458B7","adViewHeight":0,"adViewWidth":0,"amount":0,"bucketId":"1516"......}
```

#### V8501+
```csharp
// Ad display successful
PubeasyRewardVideo.Instance().OnRewardVideoImpression += OnImpression;
...
void OnImpression(string adunit, Dictionary<string, object> adInfo)
{
	Debug.Log("RewardVideoUI OnImpression adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
}
```

The returned information is as follows, and developers can obtain the corresponding content:

```java
W/Unity: RewardVideoUI OnImpression adunit:702208A872E622C1729FC621025D4B1D; adInfo: {"adNetworkId":"2","adSourceId":"ca-app-pub-3940256099942544/5224354917","adSourceName":"admob","adSourcePlacementId":"10636","adUnitId":"702208A872E622C1729FC621025D4B1D","adViewHeight":0,"adViewWidth":0,"amount":100,"bucketId":"1654","channel":"tp_channel","configBean":{"placementId":"ca-app-pub-3940256099942544/5224354917"},"configString":"{\"placementId\":\"ca-app-pub-3940256099942544/5224354917\"}","currencyName":"Reward1","ecpm":"600.0","ecpmLevel":"4","ecpmPrecision":"publisher_defined","ecpmcny":"3881.94","format":"rewarded-video","height":0,"isBiddingNetwork":false,"isoCode":"CN","loadTime":4296,"networkType":"interstitial-video","requestId":"beb60983-44a3-4f23-85a1-b9ff7b18df1a","rewardName":"Reward1","rewardNumber":100,"rewardVerifyMap":{},"sceneId":"567","segmentId":"0","subChannel":"tp_sub_channel","tpAdUnitId":"702208A872E622C1729FC621025D4B1D","waterfallIndex":3,"width":0}
```

#### Setting Global Display CallBack
+ Supported from：Unity Plugin v1.0.4+，iOS v8.5.0+, Android v8.8.0.1+
+ Before V9.5.0.1, developers need to call setAdListener synchronously in order to receive GlobalImpressionListener monitoring. `PubeasyRewardVideo.Instance().OnRewardVideoImpression += OnImpression;`

```csharp
PubeasyAds.Instance().AddGlobalAdImpression(OnGlobalAdImpression);

void OnGlobalAdImpression(Dictionary<string, object> adInfo)
{
}
```

### API
| Method | Type | Description |
| --- | --- | --- |
| tpAdUnitId | String | Ad slot ID created in Pubeasy platform. |
| true_adunit_id | String | **Added in V12.4.0.1**.Shared ad slot-specific ad information. Used to record the ad slot where the ad was finally displayed. |
| adSourceName | String | Third-party ad network name. For example, Google Ads returns "Admob". |
| adNetworkId | String | The number corresponding to the three-party advertising network is used to distinguish different advertising networks. Please refer to the table of third-party ad network IDs below. |
| adSourceId | String | Third-party ad slot ID. |
| ecpm | String | Ecpm dollars. (Default)   Unit: USD. |
| ecpmcny | String | Ecpm RMB。   unit: RMB. |
| ecpmPrecision | String | **Added in V7.0.0.0**.Get eCPM accuracy.   "publisher_defined" the eCPM defined by the developer for the ad source in the Pubeasy background;   "estimated": the eCPM estimated by Pubeasy based on historical data after the developer enables the automatic price function of the ad source in the background (cross-promotion eCPM also belongs to this type );   "exact": bidding real-time price, ~~When exact is returned, the developer needs to obtain the real-time price of bidding by obtaining ecpmExact~~ |
| ~~ecpmExact~~ | String | ~~**Added in V7.0.0.0**~~~~. The real-time price of Bidding. When ecpmPrecision returns exact，~~~~**please use ecpmExact as the advertising price, which will be more accurate than using ecpm**~~~~.~~   **V8.0.0.1 is obsolete. Developers can obtain it directly through ecpm.** |
| ~~ecpmExactCny~~ | String | ~~**Added in V7.1.0.0 **.Bidding's real-time price in RMB.When ecpmPrecision returns exact，~~~~**please use ecpmExact as the advertising price, which will be more accurate than using ecpm**~~~~.~~   **V8.0.0.1 is obsolete. Developers can obtain it directly through ecpmcny.** |
| ecpmLevel | String | **Added in V7.0.0.0**。Obtain the order (priority) of ad sources on the developer background intermediary management page.   Bidding ad source is 0 by default.   For non-bidding ad sources, the manual sorting area starts from 1 and increments. |
| loadTime | long | Load time. |
| rewardName | String | Only supported for rewarded videos. Reward items configured in Pubeasy platform. |
| rewardNumber | int | Only supported for rewarded videos. The number of rewards configured in the Pubeasy platform. |
| isoCode | String | country code. |
| height | int | The height of the corresponding ad slot can be obtained for the banner ad. |
| width | int | Banner ads can get the width of the corresponding ad slot set. |
| rewardVerifyMap | Map | Only supported by Tencent incentive videos. For Tencent Youlianghui Incentive Video Server reward verification, you need to set the corresponding one in the reward callback `user_id`<br/>。 |
| isBiddingNetwork | boolean | Determine whether it is an ad network with Bidding enabled. |
| waterfallIndex | int | Get the ranking of the current ad in the waterfall. |
| requestId | String | Request ID, an id will be generated at the beginning of each call to load, and the complete life cycle of ad loading can be tracked when the final display is completed |
| subChannel | String | Get subchannel information. |
| channel | String | Get channel information. |
| sceneId | String | scene ID. |
| configBean | ConfigResponse.WaterfallBean.ConfigBean | Obtain the three-party object issued by the WatllFall policy. |
| networkType | String | **Added in V7.6.0.1**. Get the corresponding ad type.   "interstitial"、"rewarded-video"、"banner"、"Native Banner"、"Native DrawVideo"、"Native Splash" |
| bucketId | String | **Added in V7.6.0.1**.AB test group ID. |
| segmentId | String | **Added in V7.6.0.1**.SegmentId ID。 |
| isBottom | boolean | **Added in V8.8.0.1**.Whether it is a backup ad. |
| placementAdType | int | **Added in V9.0.0.1**.   V8.7.0.1 banners and open screen ads support the mixed use of native ads, and V9.0.0.1 interstitial ads support the mixed use of open screen ads. Developers can use this field to obtain mixed ad types.   Types of advertisement source advertisement:   1. Native; 2. Interstitial; 3.App Open; 4. Banner; 5. Intersitial Video; 6. OfferWall |
| impPaidData | Map | **Added in V9.8.0.1**.The display-level revenue data returned by Admob includes the following fields：   paid_valueMicros: ECPM；   paid_currencycode: currency；   paid_precision: precision |
| impressionId | String | **Added in V9.9.0.1**.A unique identifier that identifies each ad impression. |
| video_protocol | int | **Added in v10.0.0**. Video protocol Type 1:vast, 2:vamp |
| bannerW | int | **Added in v10.6.0** The width of Banner configured in the Pubeasy platform. |
| bannerH | int | **Added in v10.6.0** The height of Banner configured in the Pubeasy platform. |


  
 



### Privacy

#### Privacy Regulations
**<font style="color:rgb(68, 73, 80);">In order to protect the interests and privacy of our developers and your users, and to conduct business in compliance with relevant laws, regulations, policies, and standards, we have updated our </font>**

[**Pubeasy Privacy Policy**](https://www.pubeasy.io/privacy-policy.html)

## 1. Check Current Region
+ <font style="color:#dd0000;">Call this method before requesting ads</font>

```csharp
// Set callback listeners
// OnCurrentAreaSuccess is called when region is successfully determined
PubeasyAds.Instance().OnCurrentAreaSuccess += OnCurrentAreaSuccess;
// OnCurrentAreaFailed is called when region query fails or the region is unknown
PubeasyAds.Instance().OnCurrentAreaFailed += OnCurrentAreaFailed;

void OnCurrentAreaSuccess(bool isEu, bool isCn, bool isCa)
{
    // Region query succeeded, developers can set relevant privacy permissions based on the returned region information
    if (isEu) {
        // Indicates European Union region, set GDPR
    }
    if (isCa) {
        // Indicates California region in the United States, set CCPA
    }
}

void OnCurrentAreaFailed(string msg)
{
    // Region query failed or unknown, developers need to handle it themselves and set relevant privacy permissions
}

// Check current region
PubeasyAds.Instance().CheckCurrentArea();
```

## 2. Setting up CCPA
This section mainly introduces how to set up CCPA in the project:

The California Consumer Privacy Act (CCPA) is the first comprehensive privacy law in the United States. It was signed into law in late June 2018 and provides California consumers with a variety of privacy rights. Businesses subject to CCPA will have multiple obligations to these consumers, including information disclosure, consumer rights similar to the General Data Protection Regulation (GDPR) of the European Union, the right to "opt-out" of specific data transfers, and the right to "opt-in" for minors.

---

#### When to set up
+ <font style="color:#dd0000;">Developers need to determine the region by themselves</font>. If it is in the California region, CCPA needs to be set up.
+ <font style="color:#dd0000;">You can use the</font><font style="color:#dd0000;"> </font>`checkCurrentArea()`<font style="color:#dd0000;"> </font><font style="color:#dd0000;">method to determine the region</font> (see the previous section for details on checking the current region), and set up CCPA when the callback `isCa` returns `true`.
+ Set up before requesting ads.

#### API
| Method | Description |
| --- | --- |
| `PubeasyAds.Instance().SetCCPADoNotSell(bool);` | `false`<br/> means not selling data for California users; `true`<br/> means accepting data selling |


#### Setting up CCPA through Meta
##### According to Meta (Facebook) requirements, developers need to set up CCPA themselves to ensure that the application complies with [Meta's CCPA specification](https://developers.facebook.com/docs/marketing-apis/data-processing-options?locale=en_US).
##### Set Facebook's Limited Data Use flag before the first ad request. Example code is shown below (Android Only):
+ If you don't want to enable Limited Data Use (LDU) mode, pass an empty string array to SetDataProcessingOptions():

```csharp
string[] dataProcessingOptions = "";
AndroidJavaClass adSettings = new AndroidJavaClass("com.facebook.ads.AdSettings");
adSettings.CallStatic("setDataProcessingOptions", (object)dataProcessingOptions);
```

+ To enable LDU mode for users and specify their geographical location, call SetDataProcessingOptions() as follows:

```csharp
string[] dataProcessingOptions = "LDU";
AndroidJavaClass adSettings = new AndroidJavaClass("com.facebook.ads.AdSettings");
adSettings.CallStatic("setDataProcessingOptions", (object)dataProcessingOptions, 0, 0);
```

## 3. Setting up COPPA
This section mainly introduces how to set up COPPA in the project:

The Children's Online Privacy Protection Act (COPPA) in the United States primarily targets the collection of personal information from children under the age of 13 online.

This protection law stipulates that website operators must comply with privacy rules, specify the time and provide verifiable methods for obtaining parental consent, and protect children's online privacy and safety, including restricting sales to children under the age of 13.

---

+ ⚠️ <font style="color:#dd0000;">Set before the first ad request</font>
+ ⚠️ If the app is targeted at adults, you can directly pass `false`.

#### API
| Method | Description |
| --- | --- |
| `PubeasyAds.Instance().SetCOPPAIsAgeRestrictedUser(bool);` | `false`<br/> means not a child; `true`<br/> means a child |


## 4. Setting up GDPR
This section mainly introduces how to set up GDPR in an Android project:

The General Data Protection Regulation (GDPR) is a regulation on data protection and privacy for all citizens of the European Union (EU) and the European Economic Area (EEA). We have added privacy permission settings in the SDK. Please check the following configurations and complete the SDK integration.

After GDPR came into effect on May 25th, social networking applications such as Twitter and WhatsApp updated their user terms, stating that they will prohibit the use of these applications by individuals under the age of 16. This is because GDPR has strict regulations on the protection of personal information of children.

[Use Google UMP](https://developers.google.com/admob/unity/privacy)

#### 2. Custom Dialog for GDPR Consent
##### When to set up
+ <font style="color:#dd0000;">Developers need to determine the region by themselves</font>. If it is in the European region, GDPR needs to be set up and then be set before the first ad request.
+ <font style="color:#dd0000;">You can use the checkCurrentArea() method to determine the region</font> (see the previous section for details on checking the current region), and set up GDPR and call this API before the first ad request when the callback `isEu` returns `true`.

##### API
| Method | Description |
| --- | --- |
| `PubeasyAds.Instance().SetGDPRDataCollection(bool);` | `false`<br/> means device data collection is not allowed; `true`<br/> means device data collection is allowed |


## 5. Setting up LGPD
The Lei Geral de Proteção de Dados (LGPD) is a comprehensive Brazilian data protection law that came into effect on September 18, 2020. It provides individuals with broader rights over their personal data and increases organizations' compliance responsibilities. The core of LGPD is to give Brazilian residents stronger control over their personal data and empower the national regulatory authority with new powers to impose significant fines on organizations that violate the law. Its rights and protections are similar to those granted to European residents by GDPR.

+ ⚠️ Must be set up before requesting ads
+ ⚠️Only needs to be called in Brazil, do not set it up in regions other than Brazil

| Function | Method | Description |
| --- | --- | --- |
| Set LGPD consent level | `PubeasyAds.Instance().SetLGPDConsent(bool);` | Whether to allow data collection: `true`<br/> allows device data collection, `false`<br/> does not allow device data collection |
| Get LGPD consent level | `PubeasyAds.Instance().GetLGPDConsent();` | Returns 0 for allowed collection, 1 for not allowed collection |


## 6. Overseas Privacy-related APIs
| Function | Method | Description |
| --- | --- | --- |
| Set GDPR consent level | `PubeasyAds.Instance().SetGDPRDataCollection(bool);` | `false`<br/> means device data collection is not allowed; `true`<br/> means device data collection is allowed |
| Get GDPR consent level | `PubeasyAds.Instance().GetGDPRDataCollection();` | Returns 0 for allowed collection, 1 for not allowed collection, 2 for not set |
| Use GDPR consent dialog | `PubeasyAds.Instance().ShowGDPRDialog();` | Needs to listen to the callback `OnDialogClosed`<br/> for dialog closing and status |
| Set CCPA consent level | `PubeasyAds.Instance().SetCCPADoNotSell(bool);` | `false`<br/> means not selling data for California users; `true`<br/> means accepting data selling |
| Get CCPA consent level | `PubeasyAds.Instance().GetCCPADoNotSell();` | Returns 0 for allowed data selling, 1 for not allowed data selling, 2 for not set |
| Set COPPA consent level | `PubeasyAds.Instance().SetCOPPAIsAgeRestrictedUser(bool);` | `false`<br/> means not a child; `true`<br/> means a child |
| Get COPPA consent level | `PubeasyAds.Instance().GetCOPPAIsAgeRestrictedUser();` | Returns 0 for child, 1 for not a child, 2 for not set |
| Check if in the EU region | `PubeasyAds.Instance().IsEUTraffic();` | |
| Check if in the California region | `PubeasyAds.Instance().IsCalifornia();` | |
| Check if it is the user's first choice | `PubeasyAds.Instance().IsFirstShowGDPR();` | Default is `false`<br/> (no choice made); `true`<br/> means the user has made a choice |
| Record the user's choice | `PubeasyAds.Instance().SetFirstShowGDPR(true);` | `true`<br/> means the user has made a choice. |


## 7. Data Privacy Settings for iOS App Submission
+ In order to improve ad revenue and play ads that are more suitable for users, third-party SDKs will attempt to obtain the IDFA, which is the Device ID listed in privacy information. Also, if the app obtains Crash information through Bugly or Firebase, please select the relevant options.
+ Please follow the instructions in the screenshots below.

![](https://cdn.nlark.com/yuque/0/2025/png/57821747/1757839181744-0362b159-39cc-45b7-bc5f-f9c847900d38.png) ![](https://cdn.nlark.com/yuque/0/2025/jpeg/57821747/1757839181746-e52336d1-98d1-49f5-ae1e-37b370d9768d.jpeg) ![](https://cdn.nlark.com/yuque/0/2025/jpeg/57821747/1757839181725-60575f05-dc57-492b-bfcf-eb44a2b95378.jpeg) After saving, continue to set the purpose of collecting privacy data for advertising playback. The final settings should be like the following screenshot: ![](https://cdn.nlark.com/yuque/0/2025/jpeg/57821747/1757839181732-c6878203-1014-45b0-9847-fbbb3b9e0aa7.jpeg)





