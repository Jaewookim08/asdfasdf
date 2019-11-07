using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Netrunner.Network
{
    public class NetworkDoorNode : NodeAction
    {
        public SelectableObjects.SelectableTarget target;
        private void Update()
        {
            if (player[1] && GameInput.GetKeyDown(1, ActionKey))
                target.Act(1);
            if (player[0] && GameInput.GetKeyDown(0, ActionKey))
                target.Act(0);
        }
    }

}