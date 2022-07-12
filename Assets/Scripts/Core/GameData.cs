using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TB
{
    public class GameData : MonoBehaviour
    {
        [SerializeField] private TMP_Text _mainItemsCountText;
        [SerializeField] private TMP_Text _secondaryItemsCountText;
        public List<ItemInfo> MainPanelItems;
        public List<ItemInfo> SecondaryPanelItems;

        private List<ItemInfo> _allItems;
        private Dictionary<int, bool> _idStores;

        private int _nextID = 0;
        public void Init()
        {
            _idStores = new Dictionary<int, bool>();
            _allItems = new List<ItemInfo>();
            MainPanelItems = new List<ItemInfo>();
            SecondaryPanelItems = new List<ItemInfo>();
        }
        public void AddNewItem(ItemInfo item)
        {
            switch (item.ListType)
            {
                case ListType.Main:
                    MainPanelItems.Add(item);
                    break;
                case ListType.Secondary:
                    SecondaryPanelItems.Add(item);
                    break;
            }
            _allItems.Add(item);
            UpdateTexts();
        }

        public void TransferToMain(ItemInfo item)
        {
            item.SwitchListType();

            _allItems.Add(item);
            SecondaryPanelItems.Add(item);

            MainPanelItems.Remove(item);


            UpdateTexts();
        }
        public void TransferToSecondary(ItemInfo item)
        {
            item.SwitchListType();

            _allItems.Add(item);
            MainPanelItems.Add(item);

            SecondaryPanelItems.Remove(item);


            UpdateTexts();
        }

        private void UpdateTexts()
        {
            if (MainPanelItems.Count > 1) _mainItemsCountText.text = $"{MainPanelItems.Count} Items";
            else _mainItemsCountText.text = $"{MainPanelItems.Count} Item";

            if (SecondaryPanelItems.Count > 1) _secondaryItemsCountText.text = $"{SecondaryPanelItems.Count} Items";
            else _secondaryItemsCountText.text = $"{SecondaryPanelItems.Count} Item";
        }

        public int GetItemsCount()
        {
            return _allItems.Count;
        }

        public int GetAvailbaleID(int id)
        {
            if (_idStores.ContainsKey(id))
            {
                while (true)
                {
                    if (_idStores.ContainsKey(_nextID))
                    {
                        _nextID++;
                    } else
                    {
                        _idStores.Add(_nextID, true);
                        return _nextID;
                    }
                }
            } else
            {
                _idStores.Add(id, true);
                return id;
            }
        }
    }
}
