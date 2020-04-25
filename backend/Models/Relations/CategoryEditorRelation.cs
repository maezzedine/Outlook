namespace backend.Models.Relations
{
    public class CategoryEditorRelation
    {
        public int ID { get; set; }
        
        public int CategoryID { get; set; }
        
        public Category Category { get; set; }

        public int MemberID { get; set; }
        
        public Member Member { get; set; }
    }
}
