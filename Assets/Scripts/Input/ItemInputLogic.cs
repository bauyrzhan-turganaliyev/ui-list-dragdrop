using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TB
{
    public class ItemInputLogic : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Action OnEndDragAction;

        private GameObject _draggingIcon;
        private RectTransform _draggingPlane;
        private Transform _oldParent;
        private CanvasGroup _canvasGroup;
        private Image _image;

        private bool dragOnSurfaces = true;

        public void Init(GameObject draggingIcon, RectTransform draggingPlane, CanvasGroup canvasGroup, Image image)
        {
            _draggingIcon = draggingIcon;
            _draggingIcon.transform.localScale = Vector3.one;
            _draggingPlane = draggingPlane;
            _canvasGroup = canvasGroup;
            _image = image;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            var canvas = FindInParents<Canvas>(gameObject);
            if (canvas == null)
                return;

            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0.5f;

            _draggingIcon = gameObject;
            _oldParent = _draggingIcon.transform.parent;

            _draggingIcon.transform.SetParent(canvas.transform, transform);
            _draggingIcon.transform.SetAsLastSibling();


            _image.sprite = GetComponent<Image>().sprite;
            _image.SetNativeSize();

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
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1f;
            if (!_draggingIcon.transform.parent.CompareTag("Content")) _draggingIcon.transform.SetParent(_oldParent);
            OnEndDragAction?.Invoke();
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
    }
}
