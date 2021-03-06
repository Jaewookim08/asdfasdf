using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Netrunner.UI;

namespace Netrunner.Network
{
    public class NetworkHackAction : NodeAction
    {
        public Selection[] selections = new Selection[2];

        private void Update()
        {
            if (player[1])
            {
                if (GameInput.GetKeyDown(1, ActionKey)) //hack started
                {
                    Node.Movable[1] = false;
                    selections[0].StartSelecting(1, "Hackable", 6f);
                }
                if (GameInput.GetKeyUp(1, ActionKey))
                {
                    GameObject g = selections[0].StopSelecting();
                    Node.Movable[1] = true;
                    if (g != null) Hack(1, g);
                }
            }
            if (player[2])
            {
                if (GameInput.GetKeyDown(2, ActionKey)) //hack started
                {
                    Node.Movable[2] = false;
                    selections[1].StartSelecting(2, "Hackable", 6f);
                }
                if (GameInput.GetKeyUp(2, ActionKey))
                {
                    GameObject g = selections[1].StopSelecting();
                    Node.Movable[2] = true;
                    if (g != null) Hack(2, g);
                }
            }
            
        }

        private void Hack(int player, GameObject hackedObject)
        {
            if (gameObject == hackedObject)
                return;
            var hackableObj = hackedObject.GetComponent<HackableObject>();
            hackedObject.GetComponent<SelectableObjects.SelectableTarget>().Act(player);
            PlayerPacket.Instances[player].HackTo(transform.position, hackableObj, hackedObject.transform);
            UIManager.current.ClearAbilities(player);
            Node.MoveOut(player);
        }
    }

}
