using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TB
{
    public class ItemInfo : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private int _id;
        [SerializeField] private string _title;
        [SerializeField] private ListType _listType;

        [SerializeField] private TMP_Text _idText;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private CanvasGroup _canvasGroup;

        public Action<ItemInfo> OnBeginDragAction;
        public Action<ItemInfo> OnDragAction;
        public Action OnEndDragAction; 

        public bool dragOnSurfaces = true;

        public int ID { get { return _id; } }
        public string Title { get { return _title; } }

        public ListType ListType { get { return _listType; } }

        private GameObject _draggingIcon;
        private RectTransform _draggingPlane;
        private Transform _oldParent;

        public void Init(Item item)
        {
            _id = item.ID;
            _title = item.Title;
            _listType = item.ListType;
            
            SetupTexts();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var canvas = FindInParents<Canvas>(gameObject);
            if (canvas == null)
                return;

            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0.5f;

            // We have clicked something that can be dragged.
            // What we want to do is create an icon for this.
            _draggingIcon = gameObject;
            _oldParent = _draggingIcon.transform.parent;

            _draggingIcon.transform.SetParent(canvas.transform, transform);
            _draggingIcon.transform.SetAsLastSibling();

            var image = GetComponent<Image>();

            image.sprite = GetComponent<Image>().sprite;
            image.SetNativeSize();

            if (dragOnSurfaces)
                _draggingPlane = transform as RectTransform;
            else
                _draggingPlane = canvas.transform as RectTransform;

            SetDraggedPosition(eventData);
        }

        public void OnDrag(PointerEventData data)
        {
            if (_draggingIcon != null)
                SetDraggedPosition(data);
        }

        private void SetDraggedPosition(PointerEventData data)
        {
            var rt = _draggingIcon.GetComponent<RectTransform>();
            Vector3 globalMousePos;

            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_draggingPlane, data.position, data.pressEventCamera, out globalMousePos))
            {
                rt.position = globalMousePos;
                rt.rotation = _draggingPlane.rotation;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            print("End Drag");
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1f;
            if (!_draggingIcon.transform.parent.CompareTag("Content")) _draggingIcon.transform.SetParent(_oldParent);
            OnEndDragAction?.Invoke();
        }

        public void SwitchListType()
        {
            switch (_listType)
            {
                case ListType.Main:
                    _listType = ListType.Secondary;
                    break;
                case ListType.Secondary:
                    _listType = ListType.Main;
                    break;
            }
        }

        static public T FindInParents<T>(GameObject go) where T : Component
        {
            if (go == null) return null;
            var comp = go.GetComponent<T>();

            if (comp != null)
                return comp;

            Transform t = go.transform.parent;
            while (t != null && comp == null)
            {
                comp = t.gameObject.GetComponent<T>();
                t = t.parent;
            }
            return comp;
        }
        private void SetupTexts()
        {
            _idText.text = _id + ".";
            _titleText.text = _title;
        }

    }
}
