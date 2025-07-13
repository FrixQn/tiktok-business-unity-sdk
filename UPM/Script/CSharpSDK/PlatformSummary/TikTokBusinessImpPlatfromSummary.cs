namespace SDK
{
    
    public class TikTokBusinessImpPlatfromSummary
    {
        private static TikTokBusinessImpPlatfromSummary _instance;

        private ITikTokBusiness _iTikTokBusiness;

        private TikTokBusinessImpPlatfromSummary()
        {
#if UNITY_ANDROID
            _iTikTokBusiness = new AndroidTikTokBusinessImp();
#elif UNITY_IOS
            _iTikTokBusiness = new IOSTikTokBusinessImp();
#else
            _iTikTokBusiness = new EditorTikTokBusinessImp();
#endif
        }

        public static TikTokBusinessImpPlatfromSummary Instance()
        {
            return _instance ?? (_instance = new TikTokBusinessImpPlatfromSummary());
        }

        // 获取窗体的实现类
        public ITikTokBusiness GetTiktokBusiness()
        {
            return _iTikTokBusiness;
        }
    }
}