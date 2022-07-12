namespace TB
{
    [System.Serializable]
    public class Item
    {
        public int ID;
        public string Title;
        public ListType ListType;
        
        public Item(int ID, string title, ListType listType)
        {
            this.ID = ID;
            this.Title = title;
            this.ListType = listType;
        }
    }
}
