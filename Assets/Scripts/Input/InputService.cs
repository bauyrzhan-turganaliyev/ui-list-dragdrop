using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TB
{
    public class InputService : MonoBehaviour
    {
        private List<ItemInfo> _items;
        public void Init(List<ItemInfo> items)
        {
            _items = new List<ItemInfo>();

            for (int i = 0; i < items.Count; i++)
            {
                _items.Add(items[i]);
            }
        }
        public void OnEndDrag()
        {
            var objects = _items.OrderByDescending(go => go.transform.position.y).ToArray();

            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].transform.SetSiblingIndex(i);
            }
        }
    }
}
