using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Netrunner.Network
{
    public class NetworkConsole : NetworkNode, HackableObject, SelectableObjects.SelectableTarget
    {
        public void Act(int player)
        {
            throw new System.NotImplementedException();
        }

        public virtual void HackIn(int player)
        {
            Player[player] = true;
        }

        public bool IsSelectable()
        {
            throw new System.NotImplementedException();
        }

        public void SetGlow(int player, bool enabled)
        {
            throw new System.NotImplementedException();
        }

        public void SetHighlight(int player, bool enabled)
        {
            throw new System.NotImplementedException();
        }
    }
}

