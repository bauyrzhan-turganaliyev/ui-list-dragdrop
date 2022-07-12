using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TB
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private ItemService _itemService;
        [SerializeField] private CreateItemService _createItemService;
        [SerializeField] private InputService _inputService;
        [SerializeField] private SortingService _sortingService;
        [SerializeField] private GameData _gameData;

        private void Start()
        {
            _gameData.Init();
            _itemService.Setup(_inputService, _sortingService, _gameData, _createItemService);
        }

    }
}
