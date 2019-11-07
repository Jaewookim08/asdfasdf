using System.Collections;
using System.Collections.Generic;
using Netrunner.SelectableObjects;
using UnityEngine;

namespace Netrunner.ModuleComponents {
    public class ArmAction : SelectingAction {

        private void Update()
        {
            if (GameInput.GetKeyDown(player, ActionKey)) //arm selection started
            {
                MovementAction movement = GetComponent<MovementAction>();
                if (movement != null) movement.enabled = false;
                selection.StartSelecting(player, "Interactable", 3f);
            }
            if (GameInput.GetKeyUp(player, ActionKey))
            {
                GameObject g = selection.StopSelecting();
                if (g != null) g.GetComponent<SelectableTarget>().Act(Module.PlayerInside);
                MovementAction movement = GetComponent<MovementAction>();
                if (movement != null) movement.enabled = true;
            }
        }
    }
}
