using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDK
{
#if UNITY_ANDROID

    public class AndroidTikTokBusinessImp : ITikTokBusiness
    {
        private static AndroidJavaObject tikTokBusinessSdk;
        // 初始化接口
        public void InitializeSdk(TikTokConfig config, Action<bool, int, string> completionHandler)
        {
            initTikTokBusinessSdk();
            AndroidJavaObject ttConfig;
            var token = getAcceceToken(config.getConfigParams());
            if (string.IsNullOrEmpty(token) )
            {
                ttConfig = new AndroidJavaObject("com.tiktok.TikTokBusinessSdk$TTConfig", GetGameActivity());
            }
            else
            {
                ttConfig = new AndroidJavaObject("com.tiktok.TikTokBusinessSdk$TTConfig", GetGameActivity(), token);
            }
            ttConfig.Call<AndroidJavaObject>("setAppId", getAppId(config.getConfigParams()));
            ttConfig.Call<AndroidJavaObject>("setTTAppId", getTTAppId(config.getConfigParams()));
            if (disableInstallTrack(config.getConfigParams()))
            {
                ttConfig.Call<AndroidJavaObject>("disableInstallLogging");
            }
            if (disableLaunchTrack(config.getConfigParams()))
            {
                ttConfig.Call<AndroidJavaObject>("disableLaunchLogging");
            }
            if (disableAutoTrack(config.getConfigParams()))
            {
                ttConfig.Call<AndroidJavaObject>("disableAutoEvents");
            }
            if (disableTrack(config.getConfigParams()))
            {
                ttConfig.Call<AndroidJavaObject>("disableAutoStart");
            }
            if (disableRetentionTrack(config.getConfigParams()))
            {
                ttConfig.Call<AndroidJavaObject>("disableRetentionLogging");
            }
            if (!disablePayTrack(config.getConfigParams()))
            {
                ttConfig.Call<AndroidJavaObject>("enableAutoIapTrack");
            }
            if (openDebugMode(config.getConfigParams()))
            {
                ttConfig.Call<AndroidJavaObject>("openDebugMode");
            }
            if (OpenLimitedDataUse(config.getConfigParams()))
            {
                ttConfig.Call<AndroidJavaObject>("enableLimitedDataUse");
            }
            if (disableAppAdTrack(config.getConfigParams()))
            {
                ttConfig.Call<AndroidJavaObject>("disableAdvertiserIDCollection");
            }
            if (disableEDPTrack(config.getConfigParams()))
            {
                ttConfig.Call<AndroidJavaObject>("disableAutoEnhancedDataPostbackEvent");
            }
            if (isLowPerformanceDevice(config.getConfigParams()))
            {
                ttConfig.Call<AndroidJavaObject>("setIsLowPerformanceDevice", true);
            }

            var logLevel = getLogLevel(config.getConfigParams());
            AndroidJavaClass logLevelAndroidJavaClass= new AndroidJavaClass("com.tiktok.TikTokBusinessSdk$LogLevel");
            AndroidJavaObject logLevelAndroidJavaObject = logLevelAndroidJavaClass.GetStatic<AndroidJavaObject>("NONE");
            if (TiktokLogLevel.None.ToString().Equals(logLevel))
            {
                logLevelAndroidJavaObject = logLevelAndroidJavaClass.GetStatic<AndroidJavaObject>("NONE");
            } else if(TiktokLogLevel.Debug.ToString().Equals(logLevel))
            {
                logLevelAndroidJavaObject = logLevelAndroidJavaClass.GetStatic<AndroidJavaObject>("DEBUG");
            } else if(TiktokLogLevel.Info.ToString().Equals(logLevel))
            {
                logLevelAndroidJavaObject = logLevelAndroidJavaClass.GetStatic<AndroidJavaObject>("INFO");
            } else if(TiktokLogLevel.Warn.ToString().Equals(logLevel))
            {
                logLevelAndroidJavaObject = logLevelAndroidJavaClass.GetStatic<AndroidJavaObject>("WARN");
            }
            ttConfig.Call<AndroidJavaObject>("setLogLevel", logLevelAndroidJavaObject);
            if (completionHandler != null)
            {
                tikTokBusinessSdk.CallStatic(
                    "initializeSdk", ttConfig, new InitCallBack(completionHandler));
            }
            else
            {
                tikTokBusinessSdk.CallStatic(
                    "initializeSdk", ttConfig);
            }

        }
        
        public void InitializeSdk(TikTokConfig config)
        {
            InitializeSdk(config, null);
        }
        
        private static void initTikTokBusinessSdk()
        {
            if (tikTokBusinessSdk == null) {
                tikTokBusinessSdk = new AndroidJavaClass(
                    "com.tiktok.TikTokBusinessSdk");
            }
        }

        // 获取 Deeplink 接口
        public void FetchDeferredDeeplink(Action<string, int, string> completionHandler)
        {
            initTikTokBusinessSdk();
            tikTokBusinessSdk.CallStatic("fetchDeferredDeeplinkWithCompletion", new DdlCallBack(completionHandler));
        }
        
        // Identify 接口
        public void Identify(string externalId, string externalUserName, string phoneNumber, string email)
        {
            initTikTokBusinessSdk();
            tikTokBusinessSdk.CallStatic("identify",
            externalId, externalUserName, phoneNumber, email);
        }
     
        // Logout 接口
        public void Logout()
        {
            initTikTokBusinessSdk();
            tikTokBusinessSdk.CallStatic("logout");
        }

        public void TrackTTEvent(TikTokBaseEvent baseEvent)
        {
            initTikTokBusinessSdk();
            if (baseEvent.getEventParams()["properties"] != null)
            {
                var prop = new AndroidJavaObject("org.json.JSONObject", baseEvent.getEventParams()["properties"]);
                var ttBaseEvent = new AndroidJavaObject("com.tiktok.appevents.base.TTBaseEvent", baseEvent.getEventParams()["eventName"],prop,baseEvent.getEventParams()["eventId"]);
                tikTokBusinessSdk.CallStatic("trackTTEvent", ttBaseEvent);
            }
            else
            {
                var ttBaseEvent = new AndroidJavaObject("com.tiktok.appevents.base.TTBaseEvent", baseEvent.getEventParams()["eventName"],null,baseEvent.getEventParams()["eventId"]);
                tikTokBusinessSdk.CallStatic("trackTTEvent", ttBaseEvent);
            }
        }
        
        public void StartTrack()
        {
            initTikTokBusinessSdk();
            tikTokBusinessSdk.CallStatic("startTrack");
        }

        public void IOS_requestTrackingAuthorization(Action<UInt64> completionHandler)
        {}

        public string IOS_GetAdvertisingIdentifier()
        {
            return "";
        }

        public static AndroidJavaObject GetGameActivity()
        {
            return new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        }
        
        private static string getAppId(Dictionary<string, string> configParams)
        {
            if (configParams.ContainsKey("appId"))
            {
                return configParams["appId"];
            }
            else
            {
                return "";
            }
        }
        
        private static bool disableLaunchTrack(Dictionary<string, string> configParams)
        {
            if (configParams.ContainsKey("disableLaunchTrack"))
            {
                return configParams["disableLaunchTrack"].Equals("1");
            }
            else
            {
                return false;
            }
        }
        
        private static bool disableAutoTrack(Dictionary<string, string> configParams)
        {
            if (configParams.ContainsKey("disableAutoTrack"))
            {
                return configParams["disableAutoTrack"].Equals("1");
            }
            else
            {
                return false;
            }
        }
        
        private static bool disableTrack(Dictionary<string, string> configParams)
        {
            if (configParams.ContainsKey("disableTrack"))
            {
                return configParams["disableTrack"].Equals("1");
            }
            else
            {
                return false;
            }
        }

        private static bool disableRetentionTrack(Dictionary<string, string> configParams)
        {
            if (configParams.ContainsKey("disableRetentionTrack"))
            {
                return configParams["disableRetentionTrack"].Equals("1");
            }
            else
            {
                return false;
            }
        }
        
        private static bool disablePayTrack(Dictionary<string, string> configParams)
        {
            if (configParams.ContainsKey("disablePayTrack"))
            {
                return configParams["disablePayTrack"].Equals("1");
            }
            else
            {
                return false;
            }
        }
        
        private static bool openDebugMode(Dictionary<string, string> configParams)
        {
            if (configParams.ContainsKey("openDebugMode"))
            {
                return configParams["openDebugMode"].Equals("1");
            }
            else
            {
                return false;
            }
        }
        
        private static bool OpenLimitedDataUse(Dictionary<string, string> configParams)
        {
            if (configParams.ContainsKey("OpenLimitedDataUse"))
            {
                return configParams["OpenLimitedDataUse"].Equals("1");
            }
            else
            {
                return false;
            }
        }
        
        private static bool disableEDPTrack(Dictionary<string, string> configParams)
        {
            if (configParams.ContainsKey("disableEDPTrack"))
            {
                return configParams["disableEDPTrack"].Equals("1");
            }
            else
            {
                return false;
            }
        }
        
        private static bool isLowPerformanceDevice(Dictionary<string, string> configParams)
        {
            if (configParams.ContainsKey("SetIsLowPerformanceDevice"))
            {
                return configParams["SetIsLowPerformanceDevice"].Equals("1");
            }
            else
            {
                return false;
            }
        }
        private static bool disableAppAdTrack(Dictionary<string, string> configParams)
        {
            if (configParams.ContainsKey("disableAppAdTrack"))
            {
                return configParams["disableAppAdTrack"].Equals("1");
            }
            else
            {
                return false;
            }
        }
        
        private static string getLogLevel(Dictionary<string, string> configParams)
        {
            if (configParams.ContainsKey("setLogLevel"))
            {
                string logLevel = configParams["setLogLevel"];
                if (logLevel == TiktokLogLevel.Verbose.ToString())
                {
                    return TiktokLogLevel.Debug.ToString();
                }
                else
                {
                    return configParams["setLogLevel"];
                }
            }
            else
            {
                return TiktokLogLevel.None.ToString();
            }
        }
        
        private static bool disableInstallTrack(Dictionary<string, string> configParams)
        {
            if (configParams.ContainsKey("disableInstallTrack"))
            {
                return configParams["disableInstallTrack"].Equals("1");
            }
            else
            {
                return false;
            }
        }
        private static string getAcceceToken(Dictionary<string, string> configParams)
        {
            if (configParams.ContainsKey("accessToken"))
            {
                return configParams["accessToken"];
            }
            else
            {
                return "";
            }
        }
        
        private static string getTTAppId(Dictionary<string, string> configParams)
        {
            if (configParams.ContainsKey("tiktokAppId"))
            {
                return configParams["tiktokAppId"];
            }
            else
            {
                return "";
            }
        }
        private class InitCallBack : AndroidJavaProxy
        {
            private Action<bool, int, string> completionHandler;
            public InitCallBack(Action<bool, int, string> completionHandler) : base("com.tiktok.TikTokBusinessSdk$TTInitCallback")
            {
                this.completionHandler = completionHandler;
            }


            public void success()
            {
                completionHandler?.Invoke(true, 0, "");
            }

            public void fail(int code, string msg)
            {
                completionHandler?.Invoke(false, code, msg);
            }
        }
        
        private class DdlCallBack : AndroidJavaProxy
        {
            private Action<string, int, string> completionHandler;
            public DdlCallBack(Action<string, int, string> completionHandler) : base("com.tiktok.TikTokBusinessSdk$FetchDeferredDeeplinkCompletion")
            {
                this.completionHandler = completionHandler;
            }
            
            public void completion(string url, AndroidJavaObject errorData)
            {
                if (string.IsNullOrEmpty(url))
                {
                    int code = -1;
                    string msg = "";
                    if (errorData != null)
                    {
                        code = errorData.Call<int>("getCode");
                        msg = errorData.Call<string>("getMsg");
                    }

                    completionHandler?.Invoke("", code, msg);
                }
                else
                {
                    completionHandler?.Invoke(url, 0, "");
                }
            }
        }

    }
#endif

}