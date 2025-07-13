using System;
using UnityEngine;

namespace SDK
{
    public class TikTokLogger
    {
        public static string TikTokiOSLog = "[TikTok]v: ";
        public static string TikTokAndroidLog = "level:verbose ";
        public static string TikTokEditorLog = "TikTok verbose log: ";
        public static bool IsLogOpen = false;
        public static void Verbose(String message)
        {
            if (!IsLogOpen) return;
            if (message == null) return;
            String log = "";
#if UNITY_ANDROID
            log = TikTokAndroidLog + message;
#elif UNITY_IOS
            log = TikTokiOSLog + message;
#else
            log = TikTokEditorLog + message;
#endif
            Debug.Log(log);

        }
    }
}