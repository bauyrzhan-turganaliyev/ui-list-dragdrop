using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace TB
{
    public class CreateItemService : MonoBehaviour
    {
        [SerializeField] private GameObject _createItemPanel;
        [SerializeField] private TMP_InputField _setIDIF;
        [SerializeField] private TMP_InputField _setTitleIF;
        [SerializeField] private TMP_Dropdown _setListTypeDD;

        public Action<Item> OnClickAddAction;
        public Action OnCancelAdd;

        private GameData _gameData;
        public void Init(GameData gameData)
        {
            _gameData = gameData;
        }
        public void OnClickAdd()
        {
            _createItemPanel.SetActive(false);
  
            OnClickAddAction?.Invoke(InitItem());
        }
        public void OnClickCancel()
        {

            _createItemPanel.SetActive(false);
        }

        public void OnClickCreateItem()
        {
            _createItemPanel.SetActive(true);
        }

        private Item InitItem()
        {
            int id = 0;

            if (int.TryParse(_setIDIF.text, out int result))
            {
                id = _gameData.GetAvailbaleID(result);
            } else
            {
                id = _gameData.GetAvailbaleID(result);
            }

            ListType listType = ListType.Main;
            switch (_setListTypeDD.value)
            {
                case 0:
                    listType = ListType.Main;
                    break;
                case 1:
                    listType = ListType.Secondary;
                    break;
            }

            string title = _setTitleIF.text;
            var item = new Item(id, title, listType);
            return item;
        }
    }
}
