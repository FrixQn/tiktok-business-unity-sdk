using System;
using System.Collections.Generic;
using System.Text;

namespace SDK
{
    
    public enum TiktokLogLevel
    {
        None,
        Info,
        Warn,
        Debug,
        Verbose
    }
    
    public class TikTokConfig
    {
        private Dictionary<string, string> _configParams = new Dictionary<string, string>();
        public TikTokConfig(string iOS_accessToken,string iOS_appId, string iOS_tiktokAppId,string Android_accessToken,string Android_appId, string Android_tiktokAppId)
        {
            var appId = iOS_appId;
            var tiktokAppId = iOS_tiktokAppId;
            var accessToken = iOS_accessToken;
#if UNITY_ANDROID
            appId = Android_appId;
            tiktokAppId = Android_tiktokAppId;
            accessToken = Android_accessToken;
#endif
            if (!string.IsNullOrEmpty(appId))
            {
                _configParams.Add("appId",appId);
            }
            if (!string.IsNullOrEmpty(tiktokAppId))
            {
                _configParams.Add("tiktokAppId",tiktokAppId);
            }
            if (!string.IsNullOrEmpty(accessToken))
            {
                _configParams.Add("accessToken",accessToken);
            }
        }
        
        public TikTokConfig(string iOS_appId, string iOS_tiktokAppId,string Android_appId, string Android_tiktokAppId)
        {
            var appId = iOS_appId;
            var tiktokAppId = iOS_tiktokAppId;
#if UNITY_ANDROID
            appId = Android_appId;
            tiktokAppId = Android_tiktokAppId;
#endif
            if (!string.IsNullOrEmpty(appId))
            {
                _configParams.Add("appId",appId);
            }
            if (!string.IsNullOrEmpty(tiktokAppId))
            {
                _configParams.Add("tiktokAppId",tiktokAppId);
            }
        }

        public void DisableTrack()
        {
            _configParams.Add("disableTrack","1");
        }

        public void DisableAutoTrack()
        {
            _configParams.Add("disableAutoTrack","1");
        }
        
        public void DisableRetentionTrack()
        {
            _configParams.Add("disableRetentionTrack","1");

        }
        
        public void DisablePayTrack()
        {
            _configParams.Add("disablePayTrack","1");
        }
        
        public void OpenDebugMode()
        {
            _configParams.Add("openDebugMode","1");
        }
        
        public void DisableEnhancedDataPostbackTrack()
        {
            _configParams.Add("disableEDPTrack","1");
        }
        
        public void SetIsLowPerformanceDevice(bool isLow)
        {
            if (isLow == true)
            {
                _configParams.Add("SetIsLowPerformanceDevice","1");
            }
            else
            {
                _configParams.Add("SetIsLowPerformanceDevice","0");
            }
        }
        
        public void OpenLimitedDataUse()
        {
            _configParams.Add("OpenLimitedDataUse","1");
        }
        
        public void DisableAppAdTrack()
        {
            _configParams.Add("disableAppAdTrack","1");
        }

        public void IOS_DisableSKAdNetworkSupport()
        {
            _configParams.Add("iOS_disableSKAdNetworkSupport","1");
        }
        
        public void IOS_SetDelayForATTUserAuthorizationInSeconds(long seconds)
        {
            _configParams.Add("iOS_setDelayForATTUserAuthorizationInSeconds",seconds.ToString());
        }

        public void SetLogLevel(TiktokLogLevel logLevel)
        {
            if (logLevel != TiktokLogLevel.None)
            {
                TikTokLogger.IsLogOpen = true;
            }
            else
            {
                TikTokLogger.IsLogOpen = false;
            }
            _configParams.Add("setLogLevel",logLevel.ToString());
        }
        
        public void DisableInstallTrack()
        {
            _configParams.Add("disableInstallTrack","1");
        }

        public void DisableLaunchTrack()
        {
            _configParams.Add("disableLaunchTrack","1");
        }

        public string GetParamsStringFromUnityConfig()
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{");

            foreach (var pair in _configParams)
            {
                jsonBuilder.Append("\"" + pair.Key + "\":\"" + pair.Value + "\",");
            }

            if (jsonBuilder.Length > 1)
            {
                jsonBuilder.Length--; // Remove the last comma
            }

            jsonBuilder.Append("}");

            return jsonBuilder.ToString();
        }
        
        public Dictionary<string, string> getConfigParams()
        {
            return _configParams;
        }
    }
}