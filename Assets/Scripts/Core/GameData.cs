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
        public List<ItemInfo> AllItemsInfo;
        public List<Item> AllItems;
        public List<Item> LoadedItems;

        private SaveDataService _saveDataService;

        private Dictionary<int, bool> _idStores;

        private int _nextID = 0;
        public void Init(SaveDataService saveDataService)
        {
            _saveDataService = saveDataService;
            _saveDataService.Init(this);

            _idStores = new Dictionary<int, bool>();
            AllItems = new List<Item>();
            LoadedItems = new List<Item>();
            AllItemsInfo = new List<ItemInfo>();
            MainPanelItems = new List<ItemInfo>();
            SecondaryPanelItems = new List<ItemInfo>();

            LoadData();
        }
        public void AddNewItem(ItemInfo itemInfo, Item item)
        {
            switch (itemInfo.ListType)
            {
                case ListType.Main:
                    MainPanelItems.Add(itemInfo);
                    break;
                case ListType.Secondary:
                    SecondaryPanelItems.Add(itemInfo);
                    break;
            }

            AllItems.Add(item);
            AllItemsInfo.Add(itemInfo);

            UpdateTexts();
        }

        public void TransferToMain(ItemInfo item)
        {

            item.SwitchListType();

            SecondaryPanelItems.Add(item);

            MainPanelItems.Remove(item);


            UpdateTexts();
        }
        public void TransferToSecondary(ItemInfo item)
        {
            item.SwitchListType();

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
            return AllItemsInfo.Count;
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
            } 
            else
            {
                _idStores.Add(id, true);
                return id;
            }
        }

        private void LoadData()
        {
            LoadedItems = _saveDataService.GetItemsList();

            for (int i = 0; i < LoadedItems.Count; i++)
            {
                if (_idStores.ContainsKey(LoadedItems[i].ID))
                {
                    LoadedItems.RemoveAt(i);
                }
                else
                {
                    _idStores.Add(LoadedItems[i].ID, true);
                }
            }
            _nextID = LoadedItems.Count;
        }
    }
}
