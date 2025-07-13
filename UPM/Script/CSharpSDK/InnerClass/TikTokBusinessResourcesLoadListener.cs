using System;
using System.Collections.Generic;
using System.Diagnostics;
using SDK;
using UnityEngine;
using Object = UnityEngine.Object;
using Debug = UnityEngine.Debug;

public class ResourcesLoadListener : ResourcesAPI
{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        ResourcesAPI.overrideAPI = new ResourcesLoadListener();
    }

    protected override Object Load(string path, Type systemTypeInstance)
    {    
        Object results = base.Load(path, systemTypeInstance);
        if (TikTokInnerManager.Instance().IsUnityEDPResourceTrackEnable())
        {
            // 记录资源 path（包含资源名称）
            Dictionary<string,object> loadInfo = new Dictionary<string, object>();
            loadInfo.Add("platform","unity");
            loadInfo.Add("monitor_type","enhanced_data_postback");
            loadInfo.Add("path",$"{path}");
            loadInfo.Add("type",$"{systemTypeInstance}");
            TikTokBusinessSDK.TrackTTEvent(new TikTokBaseEvent("load",loadInfo,""));
            TikTokLogger.Verbose("Unity edp load");
        }
        return results;
    }

    protected override Object[] LoadAll(string path, Type systemTypeInstance)
    {
        Object[] results = base.LoadAll(path, systemTypeInstance);
        if (TikTokInnerManager.Instance().IsUnityEDPResourceTrackEnable())
        {
            // 记录资源 path（包含资源名称） 
            Dictionary<string,object> loadInfo = new Dictionary<string, object>();
            loadInfo.Add("platform","unity");
            loadInfo.Add("monitor_type","enhanced_data_postback");
            loadInfo.Add("path",$"{path}");
            loadInfo.Add("type",$"{systemTypeInstance}");
            TikTokBusinessSDK.TrackTTEvent(new TikTokBaseEvent("load",loadInfo,""));
            TikTokLogger.Verbose("Unity edp loadAll");
        }
        return results;
    }
}