using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Netrunner.Misc
{
    public class PlayerTagCheckActInvoker : MonoBehaviour
    {
        public GameObject target;
        public ModuleComponents.ModuleManager module;

        // Update is called once per frame
        void Update()
        {
            if (module.CompareTag("Player"))
            {
                target.GetComponent<SelectableObjects.SelectableTarget>().Act(-1);
                Destroy(gameObject);
            }
        }
    }

}