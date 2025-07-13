using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SDK
{
    public class TikTokInnerManager : MonoBehaviour
    {
        private static TikTokInnerManager _instance;

        private long _lastTimeStamp;

        private string _currentRegexString;

        public static Dictionary<string, object> _configParams = new Dictionary<string, object>();

        [RuntimeInitializeOnLoadMethod]
        private static void CreateUIEventListener()
        {
            var m = new GameObject("TikTokInnerManager");
            TikTokBusinessEventListener listener = m.AddComponent<TikTokBusinessEventListener>();
            listener.enabled = true;
            TikTokInnerManager manager = m.AddComponent<TikTokInnerManager>();
            manager.enabled = true;
            _instance = manager;
            DontDestroyOnLoad(m);
        }

        public string SensigFiltering(string originString)
        {
            if (_currentRegexString == null || _currentRegexString =="")
            {
                string TempString = PlayerPrefs.GetString("localRegexString");
                if (TempString == null || TempString == "")
                {
                    string localRegexString = "([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\\.[a-zA-Z0-9._-]+)|(\\+?0?86-?)?1[3-9]\\d{9}|(\\+\\d{1,2}\\s?)?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}";
;                   PlayerPrefs.SetString("localRegexString",localRegexString);
                    int localRegexVersion = 0;
                    PlayerPrefs.SetInt("localRegexVersion",localRegexVersion);
                    _currentRegexString = localRegexString;
                }
                else
                {
                    _currentRegexString = TempString;
                }
            }
            string input = originString;
            string pattern = _currentRegexString;
            MatchCollection matches = Regex.Matches(input, pattern);
            foreach (Match match in matches)
            {
                string hashString = CalculateSHA256Hash(match.Value);
                input = input.Replace(match.Value, hashString);
            }

            return input;
        }
        
        private string CalculateSHA256Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // 将字符串转换为字节数组
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                // 计算哈希值
                byte[] hashBytes = sha256Hash.ComputeHash(bytes);
                // 将字节数组转换为十六进制字符串
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        
        public static TikTokInnerManager Instance()
        {
            return _instance ?? (_instance = new TikTokInnerManager());
        }
        
        public void UpdateConfigFromNative(string message)
        {
            string jsonString = message;
            Dictionary<string, object> dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
            ProcessDictionary(dict);
            if (dict != null)
            {
                _configParams = dict;
            }
            
            UpdateRegexListIfNeeded();

        }

        private void ProcessDictionary(Dictionary<string, object> dictionary)
        {
            Dictionary<string, object> newDictionary = new Dictionary<string, object>();
            foreach (var kvp in dictionary)
            {
                if (kvp.Value is Newtonsoft.Json.Linq.JObject jObject)
                {
                    newDictionary[kvp.Key] = JsonConvert.DeserializeObject<Dictionary<string, object>>(jObject.ToString());
                    ProcessDictionary(newDictionary[kvp.Key] as Dictionary<string, object>);
                }
                else if (kvp.Value is Dictionary<string, object> innerDictionary)
                {
                    ProcessDictionary(innerDictionary);
                    newDictionary[kvp.Key] = innerDictionary;
                }
                else
                {
                    newDictionary[kvp.Key] = kvp.Value;
                }
            }
            dictionary.Clear();
            foreach (var kvp in newDictionary)
            {
                dictionary[kvp.Key] = kvp.Value;
            }
        }
        private void UpdateRegexListIfNeeded()
        {
            int localRegexVersion = PlayerPrefs.GetInt("localRegexVersion");
            string localRegexString = PlayerPrefs.GetString("localRegexString");
            _currentRegexString = localRegexString;

            // 检查是否需要更新规则
            Dictionary<string, object> EDPConfig = CurrentUnityEDPConfig();
            long onlineVersion = 0;
            string onlineRegexString = "";
            if (EDPConfig.ContainsKey("sensig_filtering_regex_version"))
            {
                object value = EDPConfig["sensig_filtering_regex_version"];
                if (value is long version)
                {
                    onlineVersion = version;
                }
            }
            
            if (EDPConfig.ContainsKey("sensig_filtering_regex_list"))
            {
                object value = EDPConfig["sensig_filtering_regex_list"];
                if (value is JArray)
                {
                    JArray jArray = (JArray)value;
                    if (jArray.Count > 0)
                    {
                        onlineRegexString = (string)jArray[0];
                    }
                }
            }

            if (onlineRegexString.Length > 0)
            {
                _currentRegexString = onlineRegexString;
            }

            if (onlineVersion > localRegexVersion)
            {
                // 更新配置
                PlayerPrefs.SetString("localRegexString",onlineRegexString);
                PlayerPrefs.SetInt("localRegexVersion",(int)onlineVersion);
            }
        }

        public void PrintDictionary(Dictionary<string, object> dictionary)
        {
            foreach (var pair in dictionary)
            {
                Debug.Log($"{pair.Key}: {pair.Value}");
                if (pair.Value is Dictionary<string, object>)
                {
                    PrintDictionary((Dictionary<string, object>)pair.Value);
                }
            }
        }

        public bool IsTTSDKEnable()
        {
            string specificKey = "business_sdk_config";
            if (!_configParams.ContainsKey(specificKey))
            {
                return false;
            }

            object value = _configParams[specificKey];
            if (value is Dictionary<string, object> commonConfig)
            {

                if (commonConfig.ContainsKey("enable_sdk"))
                {
                    return (bool)commonConfig["enable_sdk"];
                }
            }

            return false;
        }
 
        public bool IsUnityEDPEnable()
        {
            if (!IsTTSDKEnable()) return false;
            Dictionary<string, object> EDPConfig = CurrentUnityEDPConfig();

            if (EDPConfig.ContainsKey("enable_sdk"))
            {
                return (bool)EDPConfig["enable_sdk"];
            }
            return false;
        }
        
        public bool IsUnityEDPClickTrackEnable()
        {
            if (!IsUnityEDPEnable()) return false;

            Dictionary<string, object> EDPConfig = CurrentUnityEDPConfig();
            if (EDPConfig.ContainsKey("enable_click_track"))
            {
                return (bool)EDPConfig["enable_click_track"];
            }
            return false;
        }
        
        public bool IsUnityEDPResourceTrackEnable()
        {
            if (!IsUnityEDPEnable()) return false;

            Dictionary<string, object> EDPConfig = CurrentUnityEDPConfig();
            if (EDPConfig.ContainsKey("enable_resource_track"))
            {
                return (bool)EDPConfig["enable_resource_track"];
            }
            return false;
        }
        
        public bool IsUnityEDPSceneTrackEnable()
        {
            if (!IsUnityEDPEnable()) return false;

            Dictionary<string, object> EDPConfig = CurrentUnityEDPConfig();
            if (EDPConfig.ContainsKey("enable_scene_track"))
            {
                return (bool)EDPConfig["enable_scene_track"];
            }
            return false;
        }

        public long PageDeepCount()
        {
            if (!IsUnityEDPEnable()) return 0;
            Dictionary<string, object> EDPConfig = CurrentUnityEDPConfig();
            if (EDPConfig.ContainsKey("page_detail_upload_deep_count"))
            {
                return (long)EDPConfig["page_detail_upload_deep_count"];
            }
            return 0;
        }
        
        public double ClickTimeDiff()
        {
            if (!IsUnityEDPEnable()) return 0;
            Dictionary<string, object> EDPConfig = CurrentUnityEDPConfig();
            if (EDPConfig.ContainsKey("time_diff_frequency_control"))
            {
                object result = EDPConfig["time_diff_frequency_control"];
                try
                {
                    return Convert.ToDouble(result);

                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }
        
        public double ReportTimeDiff()
        {
            if (!IsUnityEDPEnable()) return 1;
            Dictionary<string, object> EDPConfig = CurrentUnityEDPConfig();
            if (EDPConfig.ContainsKey("report_frequency_control"))
            {
                object result = EDPConfig["report_frequency_control"];
                try
                {
                    return Convert.ToDouble(result);

                }
                catch
                {
                    return 1;
                }
            }
            return 0;
        }

        public bool IsButtonInBlackList(string buttonClassName)
        {
            if (!IsUnityEDPEnable()) return false;
            Dictionary<string, object> EDPConfig = CurrentUnityEDPConfig();
            if (EDPConfig.ContainsKey("button_black_list"))
            {
                object result = EDPConfig["button_black_list"];

                if (result is JArray jArray)
                {
                    string[] blackList = jArray.ToObject<string[]>();
                    if (blackList.Contains(buttonClassName))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ShouldReportClickEvent()
        {
            if (!IsUnityEDPEnable()) return false;

            if (!IsUnityEDPClickTrackEnable()) return false;

            // 先判断点击间隔
            double diffCount = ClickTimeDiff() * 1000;
            long currentTimestampInMilliseconds = System.DateTime.UtcNow.Ticks / 10000;
            double result = currentTimestampInMilliseconds - _lastTimeStamp;
            double currentTimeGap = currentTimestampInMilliseconds - _lastTimeStamp;
            if (currentTimeGap < diffCount)
            {
                return false;
            }
            _lastTimeStamp = currentTimestampInMilliseconds;
            // 在判断是否抽样
            double reportDiff = ReportTimeDiff();
            double randomValue = UnityEngine.Random.value;
            if (randomValue < reportDiff)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public Dictionary<string, object> CurrentUnityEDPConfig()
        {
            string specificKey = "business_sdk_config";
            if (!_configParams.ContainsKey(specificKey))
            {
                return new Dictionary<string, object>();
            }
            object value = _configParams[specificKey];
            if (value is Dictionary<string, object> commonConfig)
            {
                if (commonConfig.ContainsKey("enhanced_data_postback_unity_config"))
                {
                    Dictionary<string, object> EDPConfig = commonConfig["enhanced_data_postback_unity_config"] as Dictionary<string, object>;
                    if (EDPConfig != null)
                    {
                        return EDPConfig;
                    }
                    else
                    {
                        return new Dictionary<string, object>();
                    }
                }
                else
                {
                    return new Dictionary<string, object>();
                }
            }
            else
            {
                return new Dictionary<string, object>();
            }
        }
    }
}