using UnityEngine;
using System;

namespace SDK
{
#if UNITY_EDITOR

    public class EditorTikTokBusinessImp : ITikTokBusiness
    {
        // 初始化接口
        public void InitializeSdk(TikTokConfig config)
        {
        }
        
        // 初始化接口
        public void InitializeSdk(TikTokConfig config, Action<bool, int, string> completionHandler)
        {
            if (completionHandler != null)
            {
                completionHandler.Invoke(true,0,"");
            }
        }
        
        // 获取 deeplink 接口
        public void FetchDeferredDeeplink(Action<string, int, string> completionHandler)
        {
            if (completionHandler != null)
            {
                completionHandler.Invoke("www.baidu.com",0,"");
            }
        }

        // Identify 接口
        public void Identify(string externalId, string externalUserName, string phoneNumber, string email)
        {
        }
     
        // Logout 接口
        public void Logout()
        {
        }
        
        // 事件上报接口
        public void TrackTTEvent(TikTokBaseEvent baseEvent)
        {
            // foreach (var pair in baseEvent.getEventParams())
            // {
            //     Debug.Log($"Key: {pair.Key}, Value: {pair.Value}");
            // }

        }
        
        public void StartTrack()
        {
        }

        public void IOS_requestTrackingAuthorization(Action<UInt64> completionHandler)
        {
            if (completionHandler != null)
            {
                completionHandler.Invoke(3);
            }
        }

        public string IOS_GetAdvertisingIdentifier()
        {
            return "test";
        }

    }
#endif

}