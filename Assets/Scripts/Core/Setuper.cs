using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TB
{
    public class Setuper : MonoBehaviour
    {
        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private Transform _itemParent;

        [SerializeField] private List<ItemInfo> _items;
        [SerializeField] private List<Transform> _itemsTransform;

        private InputService _inputService;
        public void Setup(AppSetupCreator appSetup, InputService inputService, SortingService sortingService)
        {
            _inputService = inputService;

            SubscribeServices(appSetup.SetupItemsData);

            inputService.Init(_items);
            sortingService.Init(_items);
        }

        private void SubscribeServices(List<SetupItemData> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                var item = new Item(items[i].ID, items[i].Title);
                var itemObject = Instantiate(_itemPrefab, _itemParent);
                var itemComponent = itemObject.GetComponent<ItemInfo>();

                itemComponent.Init(item);

                _items.Add(itemComponent);
                _itemsTransform.Add(itemComponent.transform);

                itemComponent.OnEndDragAction += _inputService.OnEndDrag;
            }
        }
    }
}