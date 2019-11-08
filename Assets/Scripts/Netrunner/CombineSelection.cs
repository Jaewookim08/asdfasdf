using System.Collections;
using System.Collections.Generic;
using Netrunner.SelectableObjects;
using UnityEngine;

namespace Netrunner.ModuleComponents
{
    public class CombineSelection : Selection
    {
        CombineAction.Part part;
        bool up, down;
        bool combining;

        public void StartSelecting(CombineAction comb, int player, string tag, float range)
        {
            combining = true;
            part = comb.part;
            if (part == CombineAction.Part.Upper) CalculateUpDown(comb);
            base.StartSelecting(player, tag, range);
            if (comb.Upper != null)
            {
                Targets.Add(comb.Upper.gameObject);
                comb.Upper.GetComponent<SelectableTarget>().SetGlow(player, true);
            }
            if (comb.Lower != null)
            {
                Targets.Add(comb.Lower.gameObject);
                comb.Lower.GetComponent<SelectableTarget>().SetGlow(player, true);
            }
        }
        public override void StartSelecting(int player, string tag, float range)
        {
            combining = false;
            base.StartSelecting(player, tag, range);
        }
        public void CalculateUpDown(CombineAction comb)
        {
            up = comb.Upper == null;
            down = comb.Lower == null;
        }

        protected override void UpdateTargets(string tag, float range2)  //modules, selectableTargets must be at z=0;
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
            int count = targets.Length;
            SelectableTarget[] selectables = new SelectableTarget[count];

            for (int i = 0; i < count; i++)
                selectables[i] = targets[i].GetComponent<SelectableTarget>();
            for (int i = 0; i < count; i++)
            {
                if (selectables[i] == null)
                {
                    targets[i] = targets[i].transform.parent.gameObject;
                    selectables[i] = targets[i].GetComponent<SelectableTarget>();
                }
            }
            if (combining)
            {
                count = 0;
                for (int i = 0; i < targets.Length; i++)
                {
                    if (targets[i].GetComponent<CombineAction>().Combinable(part, up, down))
                    {
                        selectables[count] = selectables[i];
                        targets[count++] = targets[i];
                    }
                }
            }

            for (int i = 0; i < count; i++)
            {
                if ((transform.position - targets[i].transform.position).sqrMagnitude <= range2)
                {
                    if (!Targets.Contains(targets[i])) //add targets that newly came into range
                    {
                        if (!selectables[i].IsSelectable(PlayerInside, tag)) continue;
                        Targets.Add(targets[i]);
                        selectables[i].SetGlow(PlayerInside, true);
                    }
                    else if (!selectables[i].IsSelectable(PlayerInside, tag)) Targets.Remove(targets[i]);
                }
                else //out of range
                {
                    if (Targets.Remove(targets[i])) //remove targets that went out of range
                    {
                        targets[i].GetComponent<SelectableTarget>().SetGlow(PlayerInside, false);
                    }
                }
            }
        }
    }
}
