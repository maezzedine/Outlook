namespace backend.Models.Interfaces
{
    interface IMember
    {
        public int NumberOfArticles { get; set; }
        
        public string Name { get; set; }
        
        public Position Position { get; set; }
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
}
