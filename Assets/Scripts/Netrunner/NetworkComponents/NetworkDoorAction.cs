using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Netrunner.Network
{
    public class NetworkDoorAction : NodeAction
    {
        public GameObject target;

        private void Update()
        {
            if (player[1] && GameInput.GetKeyDown(1, ActionKey))
                target.GetComponent< SelectableObjects.SelectableTarget>().Act(1);
            if (player[2] && GameInput.GetKeyDown(2, ActionKey))
                target.GetComponent< SelectableObjects.SelectableTarget>().Act(2);
        }
    }

}