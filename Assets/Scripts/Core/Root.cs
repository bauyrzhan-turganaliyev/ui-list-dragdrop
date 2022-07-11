using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TB
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private Setuper _setuper;
        [SerializeField] private InputService _inputService;
        [SerializeField] private SortingService _sortingService;
        [SerializeField] private AppSetupCreator _appSetup;

        private void Start()
        {
            _setuper.Setup(_appSetup, _inputService, _sortingService);

        }

    }
}
