using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        Senior_Editor,
        Associate_Editor,
        Junior_Editor,
        Proofreader,
        Web_Editor,
        Copy_Editor,
        Staff_Writer,
        Former_Member,
        رئيس_تحرير,
        المحرر,
        نائب_المحرر,
        رئيس_قسم,
        مدقق_النسخة,
        مدقق_لغوي,
        مدقق_الموقع,
        كاتب_صحفي,
        عضو_سابق
    }
}
