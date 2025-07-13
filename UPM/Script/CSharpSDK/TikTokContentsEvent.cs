using System.Collections.Generic;

namespace SDK
{
    public class TikTokContentParams
    {
        public double price;
        public long quantity;
        public string contentId;
        public string contentCategory;
        public string contentName;
        public string brand;

        public Dictionary<string,object> DictionaryValue()
        {
            Dictionary<string,object> contentDict = new Dictionary<string,object> ();
            if (price != 0)
            {
                contentDict.Add("price",price);
            }
            if (quantity != 0)
            {
                contentDict.Add("quantity",quantity);
            }
            if (contentId != null)
            {
                contentDict.Add("content_id",contentId);
            }
            if (contentCategory != null)
            {
                contentDict.Add("content_category",contentCategory);
            }
            if (contentName != null)
            {
                contentDict.Add("content_name",contentName);
            }
            if (brand != null)
            {
                contentDict.Add("brand",brand);
            }

            return contentDict;
        }
    }
    
    public class TikTokContentsEvent : TikTokBaseEvent
    {
        public TikTokContentsEvent(string eventName, Dictionary<string, object> properties, string eventId) : base(eventName, properties, eventId)
        {
        }

        public void SetDescription(string description)
        {
            if (description != null)
            {
                base.AddProperty("description",description);
            }
        }
        
        public void SetCurrency(TTCurrency currency)
        {
            string currencyString = TikTokEventConstants.GetCurrency(currency);
            if (currencyString != null)
            {
                base.AddProperty("currency",currencyString);

            }
        }
        
        public void SetValue(double value)
        {
#if UNITY_IOS
                base.AddProperty("value",value.ToString());
#else
            base.AddProperty("value",value);
#endif
        }
        
        public void SetContentType(string contentType)
        {
            if (contentType != null)
            {
                base.AddProperty("content_type",contentType);
            }
        }
        
        public void SetContentId(string contentId)
        {
            if (contentId != null)
            {
                base.AddProperty("content_id",contentId);
            }
        }
        
        public void SetContents(TikTokContentParams[] contents)
        {
            if (contents != null)
            {
                Dictionary<string, object>[] array = new Dictionary<string, object>[contents.Length];
                for (int i = 0; i < contents.Length; i++)
                {
                    TikTokContentParams currentParam = contents[i];
                    array[i] = currentParam.DictionaryValue();
                }
                base.AddProperty("contents",array);
            }
        }
    }

    public class TikTokAddToCartEvent : TikTokContentsEvent
    {
        public TikTokAddToCartEvent(string eventName, Dictionary<string, object> properties, string eventId) : base(eventName, properties, eventId)
        {
        }
        
        public TikTokAddToCartEvent(string eventID) : base("AddToCart",null,eventID)
        {
        }
    }
    
    public class TikTokAddToWishlistEvent : TikTokContentsEvent
    {
        public TikTokAddToWishlistEvent(string eventName, Dictionary<string, object> properties, string eventId) : base(eventName, properties, eventId)
        {
        }
        
        public TikTokAddToWishlistEvent(string eventID) : base("AddToWishlist",null,eventID)
        {
        }
    }
    
    public class TikTokCheckoutEvent : TikTokContentsEvent
    {
        public TikTokCheckoutEvent(string eventName, Dictionary<string, object> properties, string eventId) : base(eventName, properties, eventId)
        {
        }
        
        public TikTokCheckoutEvent(string eventID) : base("Checkout",null,eventID)
        {
        }
    }
    
    public class TikTokPurchaseEvent : TikTokContentsEvent
    {
        public TikTokPurchaseEvent(string eventName, Dictionary<string, object> properties, string eventId) : base(eventName, properties, eventId)
        {
        }
        
        public TikTokPurchaseEvent(string eventID) : base("Purchase",null,eventID)
        {
        }
    }
    
    public class TikTokViewContentEvent : TikTokContentsEvent
    {
        public TikTokViewContentEvent(string eventName, Dictionary<string, object> properties, string eventId) : base(eventName, properties, eventId)
        {
        }
        
        public TikTokViewContentEvent(string eventID) : base("ViewContent",null,eventID)
        {
        }
    }
}