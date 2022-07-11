namespace TB
{
    [System.Serializable]
    public class Item
    {
        private readonly int _id;
        private readonly string _title;

        public int ID
        {
            get
            {
                return _id;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
        }
        public Item(int ID, string title)
        {
            _id = ID;
            _title = title;
        }
    }
}
