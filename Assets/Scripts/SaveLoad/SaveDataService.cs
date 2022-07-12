using System.Collections.Generic;
using UnityEngine;

namespace TB
{
    public class SaveDataService : MonoBehaviour
    {
        const string FILENAME_ITEMS = "itemsList.json";

        private GameData _gameData;
        public void Init(GameData gameData)
        {
            _gameData = gameData;
        }

        void OnApplicationQuit()
        {
            SaveDataF();
        }

        public void SaveDataF()
        {
            var itemsList = new List<Item>();
            for (int i = 0; i < _gameData.AllItemsInfo.Count; i++)
            {
                itemsList.Add(_gameData.AllItemsInfo[i].Item);
            }
            FileHandler.SaveToJson<Item>(itemsList, FILENAME_ITEMS);
        }

        public List<Item> GetItemsList()
        {
            return FileHandler.ReadFromJSON<Item>(FILENAME_ITEMS);
        }
    }
}