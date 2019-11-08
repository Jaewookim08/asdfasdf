using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Netrunner.SelectableObjects {
    public abstract class TintSelectableTarget : MonoBehaviour, SelectableTarget //interface로 바꿔도 될 것 같네요
    {
        public SpriteRenderer[] TintSprites;
        public bool[] Glow = new bool[3];

        public abstract void Act(int player);
        public abstract bool IsSelectable(int player);

        public void SetGlow(int player, bool enabled) {
            Glow[player] = enabled;
            Color color = TintSprites[player].color;
            if (enabled) color.a = 0.2f;
            else color.a = 0f;
            TintSprites[player].color = color;
        }

        public void SetHighlight(int player, bool enabled) {

            if (!Glow[player]) return;
            Color color = TintSprites[player].color;
            if (enabled) color.a = 0.5f;
            else color.a = 0.2f;
            TintSprites[player].color = color;
        }
    }
}