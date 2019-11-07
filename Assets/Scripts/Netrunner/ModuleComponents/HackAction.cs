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
                Debug.Log("Hack" + Module.PlayerInside);
                StartSelecting("Hackable", 6f);
            }
            if (GameInput.GetKeyUp(player, ActionKey))
            {
                GameObject g = StopSelecting();
                if (g != null) Hack(g);
            }
        }

        private void Hack(GameObject hackedObject) {
            if (gameObject == hackedObject)
                return;
            var hackedModuleManager = hackedObject.GetComponent<ModuleManager>();
            if (hackedModuleManager == null)
                throw new Exception("The object being hacked doesn't have ModuleManager");
            if (hackedModuleManager.PlayerInside != 0) return;
            hackedModuleManager.HackIn(Module.PlayerInside);
            Module.HackOut();
        }
    }
}

