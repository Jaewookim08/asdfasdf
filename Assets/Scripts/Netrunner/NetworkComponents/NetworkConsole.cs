using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Netrunner.ModuleComponents;

namespace Netrunner.Network
{
    public class NetworkConsole : NetworkNode, HackableObject, SelectableObjects.SelectableTarget
    {
        public SpriteRenderer[] TintSprites = new SpriteRenderer[3];

        bool[] Glow = new bool[3];

        public void Act(int player)
        {
        }

        public void HackIn(int player)
        {
            MoveIn(player);
        }

        public bool IsSelectable(int player, string tag)
        {
            return !Player[player];
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


        [ContextMenu("InitializeConsole")]
        void EditorInitialize()
        {
            for (int i = 1; i < 3; i++)
                if (TintSprites[i] == null)
                {
                    GameObject g = Instantiate(Resources.Load<GameObject>("Builder/Tint"), transform);
                    g.name = "P" + i + "Tint";
                    g.layer = LayerMask.NameToLayer("Player" + i + "View");
                    TintSprites[i] = g.GetComponent<SpriteRenderer>();
                }

            List<NodeAction> actions = new List<NodeAction>(GetComponents<NodeAction>());
            Actions.Clear();

            NodeAction na = actions.Find(m => m is NetworkHackAction);
            if (na == null)
            {
                na = gameObject.AddComponent<NetworkHackAction>();
            }
            else actions.Remove(na);
            Actions.Add(na);
            foreach (NodeAction a in actions)
                Actions.Add(a);
            List<Selection> s = new List<Selection>(GetComponentsInChildren<Selection>());
            if (Actions.Any(a => a is NetworkHackAction))
            {
                while (s.Count < 2)
                {
                    GameObject g = Instantiate(Resources.Load<GameObject>("Builder/Selection"), transform);
                    s.Add(g.GetComponent<Selection>());
                }
            }
            foreach (NodeAction a in Actions)
                if (a is NetworkHackAction) ((NetworkHackAction)a).selections = s.ToArray();
        }
    }
}

