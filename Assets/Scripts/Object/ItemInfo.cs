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

        [SerializeField] private TMP_Text _idText;
        [SerializeField] private TMP_Text _titleText;

        public Action<ItemInfo> OnBeginDragAction;
        public Action<ItemInfo> OnDragAction;
        public Action OnEndDragAction; 

        public bool dragOnSurfaces = true;

        public int ID { get { return _id; } }
        public string Title { get { return _title; } }


        private GameObject m_DraggingIcon;
        private RectTransform m_DraggingPlane;

        public void Init(Item item)
        {
            _id = item.ID;
            _title = item.Title;

            SetupTexts();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var canvas = FindInParents<Canvas>(gameObject);
            if (canvas == null)
                return;

            // We have clicked something that can be dragged.
            // What we want to do is create an icon for this.
            m_DraggingIcon = gameObject;

            m_DraggingIcon.transform.SetAsLastSibling();

            var image = GetComponent<Image>();

            image.sprite = GetComponent<Image>().sprite;
            image.SetNativeSize();

            if (dragOnSurfaces)
                m_DraggingPlane = transform as RectTransform;
            else
                m_DraggingPlane = canvas.transform as RectTransform;

        }

        public void OnDrag(PointerEventData data)
        {
            if (m_DraggingIcon != null)
                SetDraggedPosition(data);
        }

        private void SetDraggedPosition(PointerEventData data)
        {
            var rt = m_DraggingIcon.GetComponent<RectTransform>();
            Vector3 globalMousePos;

            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
            {
                rt.position = globalMousePos;
                rt.rotation = m_DraggingPlane.rotation;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
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
        private void SetupTexts()
        {
            _idText.text = _id + ".";
            _titleText.text = _title;
        }

    }
}
