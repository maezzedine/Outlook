namespace Outlook.Models.Services
{
    public class OutlookConstants
    {
        public class Language
        {
            public const string Arabic = "Arabic";
            public const string English = "English";
        }

        public class CategoryTag
        {
            public const string Art = "Art";
            public const string Cover = "Cover";
            public const string Culture = "Culture";
            public const string Gender = "Gender";
            public const string Lifestyle = "Lifestyle";
            public const string News = "News";
            public const string Other = "Other";
            public const string Opinion = "Opinion";
            public const string Personality = "Personality";
            public const string Society = "Society";
        }

        public enum Position
        {
            Editor_In_Chief,
            رئيس_تحرير,
            Senior_Editor,
            المحرر,
            Associate_Editor,
            نائب_المحرر,
            Junior_Editor,
            رئيس_قسم,
            Proofreader,
            مدقق_لغوي,
            Web_Editor,
            مدقق_الموقع,
            Copy_Editor,
            مدقق_النسخة,
            Staff_Writer,
            كاتب_صحفي,
            Former_Member,
            عضو_سابق
        }

        public enum UserRate
        {
            None, Up, Down
        }
    }
}
