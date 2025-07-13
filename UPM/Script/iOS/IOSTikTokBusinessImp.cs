using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System;

namespace SDK
{
#if UNITY_IOS

    public class IOSTikTokBusinessImp : ITikTokBusiness
    {
                
        private delegate void TikTokBusinessInitHandler(bool result, int code, string message);
        private delegate void TikTokBusinessIDFAHandler(UInt64 status);

        private static Action<bool, int, string> _initHandler;
        private static Action<string, int, string> _deeplinkHandler;
        private static Action<UInt64> _IDFAHandler;

        
        // 初始化接口
        public void InitializeSdk(TikTokConfig config)
        {
            InitializeSdkFromUnityInvoke(config.GetParamsStringFromUnityConfig());
        }
        
        // 初始化接口
        public void InitializeSdk(TikTokConfig config, Action<bool, int, string> completionHandler)
        {
            _initHandler = completionHandler;
            InitializeSdkWithHandlerFromUnityInvoke(config.GetParamsStringFromUnityConfig(), TikTokBusinessInitHandlerMethod);
        }

        // 获取 deeplink 接口
        public void FetchDeferredDeeplink(Action<string, int, string> completionHandler)
        {
            _deeplinkHandler = completionHandler;
            FetchDeferredDeeplinkWithHandlerFromUnityInvoke(TikTokBusinessDeeplinkHandlerMethod);
        }
        
        // Identify 接口
        public void Identify(string externalId, string externalUserName, string phoneNumber, string email)
        {
            IdentifyFromUnityInvoke(externalId, externalUserName, phoneNumber, email);
        }
     
        // Logout 接口
        public void Logout()
        {
            LogoutFromUnityInvoke();
        }
        
        // 事件上报接口
        public void TrackTTEvent(TikTokBaseEvent baseEvent)
        {
            Dictionary<string, string> eventParams = baseEvent.EventParams;
            TrackTTEventFromUnityInvoke(eventParams["eventName"],eventParams["eventId"],eventParams["properties"]);
        }
        
        public void StartTrack()
        {
            StartTrackFromUnityInvoke();
        }

        public void IOS_requestTrackingAuthorization(Action<UInt64> completionHandler)
        {
            _IDFAHandler = completionHandler;
            IOS_requestTrackingAuthorizationFromUnityInvoke(TikTokBusinessIDFAHandlerMethod);
        }

        public string IOS_GetAdvertisingIdentifier()
        {
            return IOS_GetAdvertisingIdentifierFromUnityInvoke();
        }
        
        // ###################### 与C ｜ C++  代码交互 start #######################

        //通过C ｜ C++与OC交互
        [DllImport("__Internal")]
        private static extern void InitializeSdkFromUnityInvoke(string configParams);
        [DllImport("__Internal")]
        private static extern void InitializeSdkWithHandlerFromUnityInvoke(string configParams, Action<bool, int, string> completionHandler);
        [DllImport("__Internal")]
        private static extern void FetchDeferredDeeplinkWithHandlerFromUnityInvoke(Action<string, int, string> completionHandler);
        [DllImport("__Internal")]
        private static extern void IdentifyFromUnityInvoke(string externalId, string externalUserName, string phoneNumber, string email);
        [DllImport("__Internal")]
        private static extern void LogoutFromUnityInvoke();
        
        [DllImport("__Internal")]
        private static extern void TrackTTEventFromUnityInvoke(string eventName,string eventId ,string properties);
        
        [DllImport("__Internal")]
        private static extern void StartTrackFromUnityInvoke();

        [DllImport("__Internal")]
        private static extern void IOS_requestTrackingAuthorizationFromUnityInvoke(Action<UInt64> completionHandler);
        [DllImport("__Internal")]
        private static extern string IOS_GetAdvertisingIdentifierFromUnityInvoke();
        [AOT.MonoPInvokeCallback(typeof(TikTokBusinessInitHandler))]
        private static void TikTokBusinessInitHandlerMethod(bool result, int code, string message)
        {
            if (_initHandler != null)
            {
                _initHandler.Invoke(result,code,message);
            }
        }
        [AOT.MonoPInvokeCallback(typeof(TikTokBusinessIDFAHandler))]
        private static void TikTokBusinessDeeplinkHandlerMethod(string url, int code, string message)
        {
            if (_deeplinkHandler != null)
            {
                _deeplinkHandler.Invoke(url,code,message);
            }
        }
        [AOT.MonoPInvokeCallback(typeof(TikTokBusinessIDFAHandler))]
        private static void TikTokBusinessIDFAHandlerMethod(UInt64 status)
        {
            if (_IDFAHandler != null)
            {
                _IDFAHandler.Invoke(status);
            }
        }

        // ###################### 与C ｜ C++  代码交互 end #######################
    }
    #endif
}