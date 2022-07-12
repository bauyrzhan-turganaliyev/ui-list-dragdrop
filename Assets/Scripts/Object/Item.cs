namespace TB
{
    [System.Serializable]
    public class Item
    {
        private readonly int _id;
        private readonly string _title;
        private ListType _listType;
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
        public ListType ListType
        {
            get
            {
                return _listType;
            }
        }
        public Item(int ID, string title, ListType listType)
        {
            _id = ID;
            _title = title;
            _listType = listType;
        }
    }
}
