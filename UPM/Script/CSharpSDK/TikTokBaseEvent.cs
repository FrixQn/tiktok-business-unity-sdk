using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SDK
{
    public class TikTokBaseEvent
    {
        public Dictionary<string, string> EventParams = new Dictionary<string, string>();

        public TikTokBaseEvent(string eventName, Dictionary<string,object> properties, string eventId)
        {
            string _eventName = (eventName == null) ? "" : eventName;
            EventParams.Add("eventName",_eventName);

            string _eventID = (eventId == null) ? "" : eventId;
            EventParams.Add("eventId",_eventID);

            string platformInfo = "unity@" + TikTokBusinessSDK.TikTokUnitySDKVersion;
            Dictionary<string, object> tempDict = properties;
            if (tempDict == null)
            {
                tempDict = new Dictionary<string, object>();
            }
            tempDict.Add("api_platform",platformInfo);
            EventParams.Add("properties",JsonConvert.SerializeObject(tempDict));

        }

        public void AddProperty(string key, object value)
        {
            if (key == null || value == null) return;
            
            if (EventParams.ContainsKey("properties"))
            {
                string propertyString = EventParams["properties"];
                Dictionary<string, object> propertyDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(propertyString);
                if (propertyDict.ContainsKey(key))
                {
                    propertyDict[key] = value;
                }
                else
                {
                    propertyDict.Add(key,value);
                }
                EventParams["properties"] = JsonConvert.SerializeObject(propertyDict);
            }
            else
            {
                Dictionary<string, object> propertyDict = new Dictionary<string, object>();
                propertyDict.Add(key,value);
                EventParams.Add("properties",JsonConvert.SerializeObject(propertyDict));
            }
        }

        public Dictionary<string, string> getEventParams()
        {
            return EventParams;
        }
    }
}