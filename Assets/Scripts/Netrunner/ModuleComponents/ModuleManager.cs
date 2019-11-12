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
        public List<ModuleAction> Actions;


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

    }
}
