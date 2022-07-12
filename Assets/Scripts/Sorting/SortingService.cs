using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TB
{
    public class SortingService : MonoBehaviour
    {
        [SerializeField] private GameObject _sortButton;
        [SerializeField] private bool _isToUp = false;
        [SerializeField] private SortingByType _sortingByType = SortingByType.ByID;
        private GameData _gameData;
        private ItemInfo[] itemsInfo;
        public void Init(GameData gameData)
        {
            _gameData = gameData;
        }
        public void SortByID()
        {
            _sortingByType = SortingByType.ByID;
        }

        public void SortByTitle()
        {
            _sortingByType = SortingByType.ByTitle;
        }

        public void OnClickSort()
        {
            switch (_sortingByType)
            {
                case SortingByType.ByID:
                    if (_isToUp) itemsInfo = _gameData.MainPanelItems.OrderBy(go => go.ID).ToArray();
                    else itemsInfo = _gameData.MainPanelItems.OrderByDescending(go => go.ID).ToArray();
                    break;
                case SortingByType.ByTitle:
                    if (_isToUp) itemsInfo = _gameData.MainPanelItems.OrderBy(go => go.Title).ToArray();
                    else itemsInfo = _gameData.MainPanelItems.OrderByDescending(go => go.Title).ToArray();
                    break;
            }

            Sort();

            if (_isToUp) _isToUp = false;
            else _isToUp = true;

            if (_sortButton.transform.localScale.y == 1)_sortButton.transform.localScale = new Vector3(1, -1, 1);
            else _sortButton.transform.localScale = new Vector3(1, 1, 1);
        }

        private void Sort()
        {
            for (int i = 0; i < itemsInfo.Length; i++)
            {
                itemsInfo[i].transform.SetSiblingIndex(i);
            }
        }
    }


    public enum SortingByType
    {
        ByID,
        ByTitle
    }
}
