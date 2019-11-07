using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Netrunner.ModuleComponents.SelectableObjects {
    public abstract class TintSelectableTarget : SelectableTarget //interface로 바꿔도 될 것 같네요
    {
        public SpriteRenderer[] TintSprites;

        public override void SetGlow(int player, bool enabled) {
            Glow[player] = enabled;
            Color color = TintSprites[player].color;
            if (enabled) color.a = 0.2f;
            else color.a = 0f;
            TintSprites[player].color = color;
        }

        public override void SetHighlight(int player, bool enabled) {

            if (!Glow[player]) return;
            Color color = TintSprites[player].color;
            if (enabled) color.a = 0.5f;
            else color.a = 0.2f;
            TintSprites[player].color = color;
        }
    }
}