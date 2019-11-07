using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Netrunner.ModuleComponents;

namespace Netrunner.Network
{
    public class NetworkConsole : NetworkNode, HackableObject, SelectableObjects.SelectableTarget
    {
        public SpriteRenderer[] TintSprites;

        bool[] Glow = new bool[3];

        public void Act(int player)
        {
        }

        public void HackIn(int player)
        {
            MoveIn(player);
        }

        public bool IsSelectable()
        {
            return true;
        }

        public void SetGlow(int player, bool enabled)
        {
            Glow[player] = enabled;
            Color color = TintSprites[player].color;
            if (enabled) color.a = 0.2f;
            else color.a = 0f;
            TintSprites[player].color = color;
        }

        public void SetHighlight(int player, bool enabled)
        {
            if (!Glow[player]) return;
            Color color = TintSprites[player].color;
            if (enabled) color.a = 0.5f;
            else color.a = 0.2f;
            TintSprites[player].color = color;
        }
    }
}

