using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Netrunner.ModuleComponents {
    public class HackAction : SelectingAction
    {
        private void Update()
        {
            if (GameInput.GetKeyDown(player, ActionKey)) //hack started
            {
                MovementAction movement = GetComponent<MovementAction>();
                if (movement != null) movement.enabled = false;
                selection.StartSelecting(player, "Hackable", 6f);
            }
            if (GameInput.GetKeyUp(player, ActionKey))
            {
                GameObject g = selection.StopSelecting();
                MovementAction movement = GetComponent<MovementAction>();
                if (movement != null) movement.enabled = true;
                if (g != null) Hack(g);
            }
        }

        private void Hack(GameObject hackedObject) {
            if (gameObject == hackedObject)
                return;
            var hackableObj = hackedObject.GetComponent<HackableObject>();
            hackedObject.GetComponent<SelectableObjects.SelectableTarget>().Act(Module.PlayerInside);
            Network.PlayerPacket.Instances[Module.PlayerInside].HackTo(hackableObj, hackedObject.transform);
            Module.HackOut();
        }
    }
}

