using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TB
{
    public class UIList : MonoBehaviour, IDropHandler
    {
        [SerializeField] private Transform _content;
        public ListType ListType;

        public Action<ListType, ItemInfo> OnDropAction;
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.gameObject.TryGetComponent<ItemInfo>(out ItemInfo component))
            {
                print("end Drop");
                var otherItemTransform = eventData.pointerDrag.transform;
                component = otherItemTransform.gameObject.GetComponent<ItemInfo>();

                if (component.ListType == ListType)
                {
                    otherItemTransform.SetParent(_content);
                }
                else
                {
                    otherItemTransform.SetParent(_content);
                    OnDropAction?.Invoke(ListType, component);
                }
            }
        }
    }

    public enum ListType
    {
        Main,
        Secondary
    }
}
