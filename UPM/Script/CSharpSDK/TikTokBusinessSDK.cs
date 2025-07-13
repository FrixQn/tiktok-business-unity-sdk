using System;

namespace SDK
{
    public static class TikTokBusinessSDK
    {
        public static string TikTokUnitySDKVersion = "1.4.1";
        // Initialization method
        public static void InitializeSdk(TikTokConfig config)
        {
            TikTokBusinessImpPlatfromSummary.Instance().GetTiktokBusiness().InitializeSdk(config);
            TikTokLogger.Verbose("Unity InitializeSdk call");
        }
        // Initialization method,support passing in a callback for initialization completion.
        public static void InitializeSdk(TikTokConfig config, Action<bool, int, string> completionHandler)
        {
            if (completionHandler == null)
            {
                TikTokLogger.Verbose("Unity InitializeSdkWithCompletionHandler without completionHandler");
                TikTokBusinessImpPlatfromSummary.Instance().GetTiktokBusiness().InitializeSdk(config);
            }
            else
            {
                TikTokLogger.Verbose("Unity InitializeSdkWithCompletionHandler with completionHandler");
                TikTokBusinessImpPlatfromSummary.Instance().GetTiktokBusiness().InitializeSdk(config,completionHandler);
            }
        }
        // Fetch deferred deeplink
        public static void FetchDeferredDeeplink(Action<string, int, string> completionHandler)
        {
            TikTokBusinessImpPlatfromSummary.Instance().GetTiktokBusiness().FetchDeferredDeeplink(completionHandler);
            TikTokLogger.Verbose("Unity FetchDeferredDeeplink with completionHandler");
        }

        // Use this method once user has logged in or registered
        public static void Identify(string externalId, string externalUserName, string phoneNumber, string email)
        {
            string _externalId = (externalId == null) ? "" : externalId;
            string _externalUserName = (externalUserName == null) ? "" : externalUserName;
            string _phoneNumber = (phoneNumber == null) ? "" : phoneNumber;
            string _email = (email == null) ? "" : email;

            TikTokBusinessImpPlatfromSummary.Instance().GetTiktokBusiness().Identify(_externalId,_externalUserName,_phoneNumber,_email);
            TikTokLogger.Verbose("Unity Identify call :externalId:" + externalId + " externalUserName:" + externalUserName + " phoneNumber:" + phoneNumber + " email:" + email);
        }
        // Call this method when user has logged out
        public static void Logout()
        {
            TikTokBusinessImpPlatfromSummary.Instance().GetTiktokBusiness().Logout();
            TikTokLogger.Verbose("Unity Logout call");
        }
        // This method should be called whenever an event needs to be tracked
        public static void TrackTTEvent(TikTokBaseEvent baseEvent)
        {
            TikTokBusinessImpPlatfromSummary.Instance().GetTiktokBusiness().TrackTTEvent(baseEvent);
            TikTokLogger.Verbose("Unity TrackTTEvent call");
        }
        public static void StartTrack()
        {
            TikTokBusinessImpPlatfromSummary.Instance().GetTiktokBusiness().StartTrack();
            TikTokLogger.Verbose("Unity StartTrack call");
        }

        public static void IOS_requestTrackingAuthorization(Action<UInt64> completionHandler)
        {
            TikTokLogger.Verbose("Unity IOS_requestTrackingAuthorization call");
            TikTokBusinessImpPlatfromSummary.Instance().GetTiktokBusiness().IOS_requestTrackingAuthorization(completionHandler);
        }

        public static string IOS_GetAdvertisingIdentifier()
        {
            TikTokLogger.Verbose("Unity IOS_GetAdvertisingIdentifier call");
            return TikTokBusinessImpPlatfromSummary.Instance().GetTiktokBusiness().IOS_GetAdvertisingIdentifier();
        }
    }
}