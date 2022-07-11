using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TB
{
    [CreateAssetMenu(fileName = "App Setup", menuName = "ScriptableObjects/AppSetup", order = 1)]
    public class AppSetupCreator : ScriptableObject
    {
        public List<SetupItemData> SetupItemsData;
    }

    [System.Serializable]
    public class SetupItemData
    {
        public int ID;
        public string Title;
    }
}
