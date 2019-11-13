using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Netrunner.SelectableObjects;
using Netrunner.UI;
using UnityEngine;


namespace Netrunner.ModuleComponents {
    /// <summary>
    /// In every module, manages which player is inside, enable/disable ModuleActions
    /// </summary>
    public class ModuleManager : TintSelectableTarget, HackableObject, SuspiciousObject {
        /// <summary>
        /// List of all ModuleActions in this module
        /// </summary>
        public List<ModuleAction> Actions = new List<ModuleAction>();


        /// <summary>
        /// Whether player is inside the module. 0 for none, 1, 2 for player 1, 2
        /// </summary>
        public int PlayerInside { get; private set; }

        public override void Act(int player)
        {
            PlayerInside = player;
        }

        // Start is called before the first frame update
        /*private void Start() {
            Actions = GetComponentsInChildren<ModuleAction>().ToList();
        }*/

        /// <summary>
        /// Called for going in the module
        /// </summary>
        public void HackIn(int player) {
            //enable all actions, update UI, etc
            PlayerInside = player;
            tag = "Player";
            UIManager.current.ChangeAbilities(player);
            foreach (var action in Actions) {
                action.enabled = true;
                action.Init(player);
            }
        }

        /// <summary>
        /// Called when going out of the module
        /// </summary>
        public void HackOut() {
            //disable all actions, etc
            PlayerInside = 0;
            tag = "Untagged";
            foreach (var action in Actions) {
                action.enabled = false;
            }
        }

        public override bool IsSelectable(int player, string tag)
        {
            if (tag == "Hackable") return PlayerInside == 0;
            else return true;
        }

        public float GetSuspiciousness()
        {
            return 0f;
        }

        [ContextMenu("Initialize")]
        public void EditorInitialize()
        {
            if(GetComponent<SpriteRenderer>()==null)
                gameObject.AddComponent<SpriteRenderer>();
            if (GetComponent<Rigidbody2D>() == null)
                gameObject.AddComponent<Rigidbody2D>();
            if (GetComponent<Collider2D>() == null)
                gameObject.AddComponent<BoxCollider2D>();
            for (int i=1; i<3; i++)
                if (TintSprites[i] == null)
                {
                    GameObject g = Instantiate(Resources.Load<GameObject>("Builder/Tint"), transform);
                    g.name = "P"+i+"Tint";
                    g.layer = LayerMask.NameToLayer("Player" + i + "View");
                    TintSprites[i] = g.GetComponent<SpriteRenderer>();
                }

            List<ModuleAction> actions = new List<ModuleAction>(GetComponents<ModuleAction>());
            Actions.Clear();
            ModuleAction a = actions.Find(m => m is HackAction);
            if (a == null)
            {
                a = gameObject.AddComponent<HackAction>();
            }
            else actions.Remove(a);
            Actions.Add(a);
            foreach (ModuleAction ma in actions)
                Actions.Add(ma);
            Selection s = GetComponentInChildren<CombineSelection>();
            a = Actions.Find(m => m is CombineAction);
            if (a != null)
            {
                if (s == null)
                {
                    s = GetComponentInChildren<Selection>();
                    if (s != null) Destroy(s.gameObject);
                    GameObject g = Instantiate(Resources.Load<GameObject>("Builder/CombineSelection"), transform);
                    s = g.GetComponent<CombineSelection>();
                }
                ((CombineAction)a).selection = s;
            }
            if (s == null) s = GetComponentInChildren<Selection>();
            if (s == null && Actions.Any(m=> m is SelectingAction))
            {
                GameObject g = Instantiate(Resources.Load<GameObject>("Builder/Selection"), transform);
                s = g.GetComponent<Selection>();
            }
            foreach (ModuleAction ma in Actions)
                if (ma is SelectingAction) ((SelectingAction)ma).selection = s;
        }

    }
}
