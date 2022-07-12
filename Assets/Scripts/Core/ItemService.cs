using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TB
{
    public class ItemService: MonoBehaviour
    {
        [SerializeField] private List<UIList> _lists;

        [SerializeField] private GameObject _itemPrefab;

        [SerializeField] private Transform _mainParent;
        [SerializeField] private Transform _secondaryParent;

        [SerializeField] private List<ItemInfo> _items;
        [SerializeField] private List<Transform> _itemsTransform;

        private InputService _inputService;
        private GameData _gameData;
        public void Setup(InputService inputService, SortingService sortingService, GameData gameData, CreateItemService createItemService)
        {
            _inputService = inputService;
            _gameData = gameData;

            LoadData();

            inputService.Init(_items);
            sortingService.Init(gameData);
            createItemService.Init(gameData);

            for (int i = 0; i < _lists.Count; i++)
            {
                _lists[i].OnDropAction += TransferItemTo;
            }

            createItemService.OnClickAddAction += CreateItem;
        }

        public void CreateItem(Item item)
        {
            var itemObject = Instantiate(_itemPrefab);
            switch (item.ListType)
            {
                case ListType.Main:
                    itemObject.transform.SetParent(_mainParent);
                    break;
                case ListType.Secondary:
                    itemObject.transform.SetParent(_secondaryParent);
                    break;
            }
            var itemComponent = itemObject.GetComponent<ItemInfo>();

            itemComponent.Init(item);

            _items.Add(itemComponent);
            _itemsTransform.Add(itemComponent.transform);

            itemComponent.OnEndDragAction += _inputService.OnEndDrag;

            _gameData.AddNewItem(itemComponent, item);
        }

        private void TransferItemTo(ListType transferTo, ItemInfo item)
        {
            print(transferTo);
            switch (transferTo)
            {
                case ListType.Main:
                    _gameData.TransferToSecondary(item);
                  
                    break;
                case ListType.Secondary:
                    _gameData.TransferToMain(item);
                    break;
            }
        }

        private void LoadData()
        {
            for (int i = 0; i < _gameData.LoadedItems.Count; i++)
            {
                CreateItem(_gameData.LoadedItems[i]);
            }
        }
    }
}