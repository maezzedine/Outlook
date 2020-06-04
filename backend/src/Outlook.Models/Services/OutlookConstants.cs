namespace Outlook.Models.Services
{
    public class OutlookConstants
    {
        public static Position[] ArabicPositions = { Position.المحرر, Position.رئيس_تحرير, Position.رئيس_قسم, Position.عضو_سابق, Position.كاتب_صحفي, Position.مدقق_الموقع, Position.مدقق_النسخة, Position.مدقق_لغوي, Position.نائب_المحرر };
        public static Position[] EnglishPositions = { Position.Editor_In_Chief, Position.Senior_Editor, Position.Associate_Editor, Position.Junior_Editor, Position.Proofreader, Position.Copy_Editor, Position.Web_Editor, Position.Former_Member, Position.Staff_Writer };
        public static Position[] NonBoardMembers = { Position.Staff_Writer, Position.Former_Member, Position.كاتب_صحفي, Position.عضو_سابق };

        public const string MigrationsAssembly = "Outlook.Models";

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

        public class Urls
        {
            // Todo: Add Production Urls and Switch to them
            public class Development
            {
                public const string Api = "https://localhost:5000";
                public const string Server = "https://localhost:5001";
                public const string Client = "http://192.168.50.104:8080;http://192.168.50.101:8080;http://192.168.50.102:8080;http://localhost:8080";
            }
        }


        public class OpenSource
        {
            public const string Version = "1.0.0";
            public const string LicenseUrl = "https://github.com/mezdn/Outlook/blob/master/LICENSE";
        }
    }
}
