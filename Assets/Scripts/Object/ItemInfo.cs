using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TB
{
    public class ItemInfo : MonoBehaviour
    {
        [SerializeField] private ItemInputLogic _itemInputLogic;
        private int _id;
        private string _title;
        private ListType _listType;

        [SerializeField] private TMP_Text _idText;
        [SerializeField] private TMP_Text _titleText;

        [SerializeField] private CanvasGroup _canvasGroup;

        public Item Item;

        public int ID { get { return _id; } }
        public string Title { get { return _title; } }
        public ListType ListType { get { return _listType; } }
        public ItemInputLogic ItemInputLogic { get { return _itemInputLogic; } }


        public void Init(Item item)
        {
            Item = item;

            _id = item.ID;
            _title = item.Title;
            _listType = item.ListType;

            _itemInputLogic.Init(gameObject, GetComponent<RectTransform>(), _canvasGroup, GetComponent<Image>());
            
            SetupTexts();
        }

        public void SwitchListType()
        {
            switch (_listType)
            {
                case ListType.Main:
                    _listType = ListType.Secondary;
                    Item.ListType = ListType.Secondary;
                    break;
                case ListType.Secondary:
                    _listType = ListType.Main;
                    Item.ListType = ListType.Main;
                    break;
            }
        }

        private void SetupTexts()
        {
            _idText.text = _id + ".";
            _titleText.text = _title;
        }

    }
}
