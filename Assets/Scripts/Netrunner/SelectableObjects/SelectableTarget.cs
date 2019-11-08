using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Netrunner.SelectableObjects {
    public interface SelectableTarget
    {
        void Act(int player);

        void SetGlow(int player, bool enabled);

        void SetHighlight(int player, bool enabled);

        bool IsSelectable(int player);
    }
}