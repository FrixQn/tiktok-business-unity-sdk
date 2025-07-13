using System;

namespace SDK
{
    public interface ITikTokBusiness
    {
        void InitializeSdk(TikTokConfig config);
        
        void InitializeSdk(TikTokConfig config,Action<bool,int,string> completionHandler);
        
        void FetchDeferredDeeplink(Action<string,int,string> completionHandler);

        void Identify(string externalId, string externalUserName, string phoneNumber, string email);
     
        void Logout();

        void TrackTTEvent(TikTokBaseEvent baseEvent);
        
        void StartTrack();

        void IOS_requestTrackingAuthorization(Action<UInt64> completionHandler);

        string IOS_GetAdvertisingIdentifier();
    }
    
}

