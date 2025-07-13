namespace SDK
{
    public enum TTEventName
    {
        TTEventNameAchieveLevel,
        TTEventNameAddPaymentInfo,
        TTEventNameCompleteTutorial,
        TTEventNameCreateGroup,
        TTEventNameCreateRole,
        TTEventNameGenerateLead,
        TTEventNameImpressionLevelAdRevenue,
        TTEventNameInAppADClick,
        TTEventNameInAppADImpr,
        TTEventNameInstallApp,
        TTEventNameJoinGroup,
        TTEventNameLaunchAPP,
        TTEventNameLoanApplication,
        TTEventNameLoanApproval,
        TTEventNameLoanDisbursal,
        TTEventNameLogin,
        TTEventNameRate,
        TTEventNameRegistration,
        TTEventNameSearch,
        TTEventNameSpendCredits,
        TTEventNameStartTrial,
        TTEventNameSubscribe,
        TTEventNameUnlockAchievement
    }
    
    public enum TTCurrency
    {
        TTCurrencyAED,
        TTCurrencyARS,
        TTCurrencyAUD,
        TTCurrencyBDT,
        TTCurrencyBGN,
        TTCurrencyBHD,
        TTCurrencyBIF,
        TTCurrencyBOB,
        TTCurrencyBRL,
        TTCurrencyCAD,
        TTCurrencyCHF,
        TTCurrencyCLP,
        TTCurrencyCNY,
        TTCurrencyCOP,
        TTCurrencyCRC,
        TTCurrencyCZK,
        TTCurrencyDKK,
        TTCurrencyDZD,
        TTCurrencyEGP,
        TTCurrencyEUR,
        TTCurrencyGBP,
        TTCurrencyGTQ,
        TTCurrencyHKD,
        TTCurrencyHNL,
        TTCurrencyHUF,
        TTCurrencyIDR,
        TTCurrencyILS,
        TTCurrencyINR,
        TTCurrencyIQD,
        TTCurrencyISK,
        TTCurrencyJOD,
        TTCurrencyJPY,
        TTCurrencyKES,
        TTCurrencyKHR,
        TTCurrencyKRW,
        TTCurrencyKWD,
        TTCurrencyKZT,
        TTCurrencyLBP,
        TTCurrencyMAD,
        TTCurrencyMOP,
        TTCurrencyMXN,
        TTCurrencyMYR,
        TTCurrencyNGN,
        TTCurrencyNIO,
        TTCurrencyNOK,
        TTCurrencyNZD,
        TTCurrencyOMR,
        TTCurrencyPEN,
        TTCurrencyPHP,
        TTCurrencyPKR,
        TTCurrencyPLN,
        TTCurrencyPYG,
        TTCurrencyQAR,
        TTCurrencyRON,
        TTCurrencyRUB,
        TTCurrencySAR,
        TTCurrencySEK,
        TTCurrencySGD,
        TTCurrencyTHB,
        TTCurrencyTRY,
        TTCurrencyTWD,
        TTCurrencyTZS,
        TTCurrencyUAH,
        TTCurrencyUSD,
        TTCurrencyVES,
        TTCurrencyVND,
        TTCurrencyZAR
    }

    public static class TikTokEventConstants
    {
        public static string GetEventName(TTEventName name)
        {
            string nameString = "";
            switch (name)
            {
                case TTEventName.TTEventNameAchieveLevel:
                    nameString = "AchieveLevel";
                    break;
                case TTEventName.TTEventNameAddPaymentInfo:
                    nameString = "AddPaymentInfo";
                    break;
                case TTEventName.TTEventNameCompleteTutorial:
                    nameString = "CompleteTutorial";
                    break;
                case TTEventName.TTEventNameCreateGroup:
                    nameString = "CreateGroup";
                    break;
                case TTEventName.TTEventNameCreateRole:
                    nameString = "CreateRole";
                    break;
                case TTEventName.TTEventNameGenerateLead:
                    nameString = "GenerateLead";
                    break;
                case TTEventName.TTEventNameImpressionLevelAdRevenue:
                    nameString = "ImpressionLevelAdRevenue";
                    break;
                case TTEventName.TTEventNameInAppADClick:
                    nameString = "InAppADClick";
                    break;
                case TTEventName.TTEventNameInAppADImpr:
                    nameString = "InAppADImpr";
                    break;
                case TTEventName.TTEventNameInstallApp:
                    nameString = "InstallApp";
                    break;
                case TTEventName.TTEventNameJoinGroup:
                    nameString = "JoinGroup";
                    break;
                case TTEventName.TTEventNameLaunchAPP:
                    nameString = "LaunchAPP";
                    break;
                case TTEventName.TTEventNameLoanApplication:
                    nameString = "LoanApplication";
                    break;
                case TTEventName.TTEventNameLoanApproval:
                    nameString = "LoanApproval";
                    break;
                case TTEventName.TTEventNameLoanDisbursal:
                    nameString = "LoanDisbursal";
                    break;
                case TTEventName.TTEventNameLogin:
                    nameString = "Login";
                    break;
                case TTEventName.TTEventNameRate:
                    nameString = "Rate";
                    break;
                case TTEventName.TTEventNameRegistration:
                    nameString = "Registration";
                    break;
                case TTEventName.TTEventNameSearch:
                    nameString = "Search";
                    break;
                case TTEventName.TTEventNameSpendCredits:
                    nameString = "SpendCredits";
                    break;
                case TTEventName.TTEventNameStartTrial:
                    nameString = "StartTrial";
                    break;
                case TTEventName.TTEventNameSubscribe:
                    nameString = "Subscribe";
                    break;
                case TTEventName.TTEventNameUnlockAchievement:
                    nameString = "UnlockAchievement";
                    break;
            }

            return nameString;
        }

        public static string GetCurrency(TTCurrency currency)
        {
            string currencyString = "";
            switch (currency)
            {
                case TTCurrency.TTCurrencyAED:
                    currencyString = "AED";
                    break;
                case TTCurrency.TTCurrencyARS:
                    currencyString = "ARS";
                    break;
                case TTCurrency.TTCurrencyAUD:
                    currencyString = "AUD";
                    break;
                case TTCurrency.TTCurrencyBDT:
                    currencyString = "BDT";
                    break;
                case TTCurrency.TTCurrencyBGN:
                    currencyString = "BGN";
                    break;
                case TTCurrency.TTCurrencyBHD:
                    currencyString = "BHD";
                    break;
                case TTCurrency.TTCurrencyBIF:
                    currencyString = "BIF";
                    break;
                case TTCurrency.TTCurrencyBOB:
                    currencyString = "BOB";
                    break;
                case TTCurrency.TTCurrencyBRL:
                    currencyString = "BRL";
                    break;
                case TTCurrency.TTCurrencyCAD:
                    currencyString = "CAD";
                    break;
                case TTCurrency.TTCurrencyCHF:
                    currencyString = "CHF";
                    break;
                case TTCurrency.TTCurrencyCLP:
                    currencyString = "CLP";
                    break;
                case TTCurrency.TTCurrencyCNY:
                    currencyString = "CNY";
                    break;
                case TTCurrency.TTCurrencyCOP:
                    currencyString = "COP";
                    break;
                case TTCurrency.TTCurrencyCRC:
                    currencyString = "CRC";
                    break;
                case TTCurrency.TTCurrencyCZK:
                    currencyString = "CZK";
                    break;
                case TTCurrency.TTCurrencyDKK:
                    currencyString = "DKK";
                    break;
                case TTCurrency.TTCurrencyDZD:
                    currencyString = "DZD";
                    break;
                case TTCurrency.TTCurrencyEGP:
                    currencyString = "EGP";
                    break;
                case TTCurrency.TTCurrencyEUR:
                    currencyString = "EUR";
                    break;
                case TTCurrency.TTCurrencyGBP:
                    currencyString = "GBP";
                    break;
                case TTCurrency.TTCurrencyGTQ:
                    currencyString = "GTQ";
                    break;
                case TTCurrency.TTCurrencyHKD:
                    currencyString = "HKD";
                    break;
                case TTCurrency.TTCurrencyHNL:
                    currencyString = "HNL";
                    break;
                case TTCurrency.TTCurrencyHUF:
                    currencyString = "HUF";
                    break;
                case TTCurrency.TTCurrencyIDR:
                    currencyString = "IDR";
                    break;
                case TTCurrency.TTCurrencyILS:
                    currencyString = "ILS";
                    break;
                case TTCurrency.TTCurrencyINR:
                    currencyString = "INR";
                    break;
                case TTCurrency.TTCurrencyIQD:
                    currencyString = "IQD";
                    break;
                case TTCurrency.TTCurrencyISK:
                    currencyString = "ISK";
                    break;
                case TTCurrency.TTCurrencyJOD:
                    currencyString = "JOD";
                    break;
                case TTCurrency.TTCurrencyJPY:
                    currencyString = "JPY";
                    break;
                case TTCurrency.TTCurrencyKES:
                    currencyString = "KES";
                    break;
                case TTCurrency.TTCurrencyKHR:
                    currencyString = "KHR";
                    break;
                case TTCurrency.TTCurrencyKRW:
                    currencyString = "KRW";
                    break;
                case TTCurrency.TTCurrencyKWD:
                    currencyString = "KWD";
                    break;
                case TTCurrency.TTCurrencyKZT:
                    currencyString = "KZT";
                    break;
                case TTCurrency.TTCurrencyLBP:
                    currencyString = "LBP";
                    break;
                case TTCurrency.TTCurrencyMAD:
                    currencyString = "MAD";
                    break;
                case TTCurrency.TTCurrencyMOP:
                    currencyString = "MOP";
                    break;
                case TTCurrency.TTCurrencyMXN:
                    currencyString = "MXN";
                    break;
                case TTCurrency.TTCurrencyMYR:
                    currencyString = "MYR";
                    break;
                case TTCurrency.TTCurrencyNGN:
                    currencyString = "NGN";
                    break;
                case TTCurrency.TTCurrencyNIO:
                    currencyString = "NIO";
                    break;
                case TTCurrency.TTCurrencyNOK:
                    currencyString = "NOK";
                    break;
                case TTCurrency.TTCurrencyNZD:
                    currencyString = "NZD";
                    break;
                case TTCurrency.TTCurrencyOMR:
                    currencyString = "OMR";
                    break;
                case TTCurrency.TTCurrencyPEN:
                    currencyString = "PEN";
                    break;
                case TTCurrency.TTCurrencyPHP:
                    currencyString = "PHP";
                    break;
                case TTCurrency.TTCurrencyPKR:
                    currencyString = "PKR";
                    break;
                case TTCurrency.TTCurrencyPLN:
                    currencyString = "PLN";
                    break;
                case TTCurrency.TTCurrencyPYG:
                    currencyString = "PYG";
                    break;
                case TTCurrency.TTCurrencyQAR:
                    currencyString = "QAR";
                    break;
                case TTCurrency.TTCurrencyRON:
                    currencyString = "RON";
                    break;
                case TTCurrency.TTCurrencyRUB:
                    currencyString = "RUB";
                    break;
                case TTCurrency.TTCurrencySAR:
                    currencyString = "SAR";
                    break;
                case TTCurrency.TTCurrencySEK:
                    currencyString = "SEK";
                    break;
                case TTCurrency.TTCurrencySGD:
                    currencyString = "SGD";
                    break;
                case TTCurrency.TTCurrencyTHB:
                    currencyString = "THB";
                    break;
                case TTCurrency.TTCurrencyTRY:
                    currencyString = "TRY";
                    break;
                case TTCurrency.TTCurrencyTWD:
                    currencyString = "TWD";
                    break;
                case TTCurrency.TTCurrencyTZS:
                    currencyString = "TZS";
                    break;
                case TTCurrency.TTCurrencyUAH:
                    currencyString = "UAH";
                    break;
                case TTCurrency.TTCurrencyUSD:
                    currencyString = "USD";
                    break;
                case TTCurrency.TTCurrencyVES:
                    currencyString = "VES";
                    break;
                case TTCurrency.TTCurrencyVND:
                    currencyString = "VND";
                    break;
                case TTCurrency.TTCurrencyZAR:
                    currencyString = "ZAR";
                    break;
            }

            return currencyString;
        }
    }
}