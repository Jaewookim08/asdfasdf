using System.Collections;
using System.Collections.Generic;
using Netrunner.SelectableObjects;
using UnityEngine;

namespace Netrunner.ModuleComponents {
    /// <summary>
    /// Have to be later than MovementAction in ModuleManager.Actions
    /// </summary>
    public class CombineAction : SelectingAction {
        public enum Part {  Lower = 1, Upper, Top }

        public float Range = 2f;
        public Part part;
        public Transform UpperAttachPoint, LowerAttachPoint;
        public Joint2D UpperJoint, LowerJoint;
        public CombineAction Upper, Lower;
        public float Mass, SupportableMass;

        /// <summary>
        /// if negative, no movement if higher priority module is combined
        /// </summary>
        public int MovementPriority;

        bool Movement = true;
        bool Selecting = false;

        //bool Movement

        /// <summary>
        /// Mass of the module, and the module on top if it.
        /// </summary>
        /// <returns></returns>
        public float GetMass()
        {
            if (Upper == null) return Mass;
            else return Mass + Upper.GetMass();
        }

        public bool Combinable(Part p, bool up, bool down)
        {
            if (p == part) return false;
            switch (p)
            {
                case Part.Lower: return Lower == null && (part == Part.Upper || part == Part.Top);
                case Part.Top: return Upper == null && (part == Part.Upper || part == Part.Lower);
                case Part.Upper:
                    if (part == Part.Lower) return Upper == null && down;
                    else if (part == Part.Top) return Lower == null && up;
                    else return false;
                default: return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="up">this module is on top of caller</param>
        /// <param name="comb"></param>
        public void Combined(bool up, CombineAction comb)
        {
            if (up)
            {
                Lower = comb;
                LowerJoint.enabled = true;
                LowerJoint.connectedBody = comb.GetComponent<Rigidbody2D>();
            }
            else
            {
                Upper = comb;
                UpperJoint.enabled = true;
                UpperJoint.connectedBody = comb.GetComponent<Rigidbody2D>();
                if (comb.GetMass() > SupportableMass) Break();
            }

            if (Selecting)
            {
                if(part == Part.Upper)
                {
                    //if(Upper!=null && Lower&&null)
                }
            }
            UpdateMovement();
        }

        public void Separated(bool up)
        {
            if (up)
            {
                LowerJoint.connectedBody = null;
                LowerJoint.enabled = false;
                Lower = null;
            }
            else
            {
                UpperJoint.connectedBody = null;
                UpperJoint.enabled = false;
                Upper = null;
            }
            UpdateMovement();
        }

        public override void Init(int player)
        {
            base.Init(player);
            if (Movement) return;
            MovementAction move = GetComponent<MovementAction>();
            if (move != null) move.enabled = false;
        }

        void UpdateMovement()
        {
            int highest = int.MinValue;
            if (Upper != null)
            {
                if (highest < Upper.MovementPriority) highest = Upper.MovementPriority;
                if (Upper.Upper != null && highest < Upper.Upper.MovementPriority) highest = Upper.Upper.MovementPriority;
            }
            if (Lower != null)
            {
                if (highest < Lower.MovementPriority) highest = Lower.MovementPriority;
                if (Lower.Lower != null && highest < Lower.Lower.MovementPriority) highest = Lower.Lower.MovementPriority;
            }
            if (MovementPriority < 0 && highest > MovementPriority)
            {
                Movement = false;
                MovementAction move = GetComponent<MovementAction>();
                if (move != null) move.enabled = false;
            }
            else
            {
                Movement = true;
                MovementAction move = GetComponent<MovementAction>();
                if (Module != null && move != null && Module.PlayerInside > 0) move.enabled = true;
            }
        }

        void Break()
        {
            Debug.Log(string.Format("Module {0} broke!", gameObject.name));
        }

        private void Update()
        {
            if (GameInput.GetKeyDown(player, ActionKey))
            {
                MovementAction movement = GetComponent<MovementAction>();
                Selecting = true;
                if (movement != null) movement.enabled = false;
                ((CombineSelection)selection).StartSelecting(this, player, "Combinable", Range);
            }
            if (GameInput.GetKeyUp(player, ActionKey))
            {
                Selecting = false;
                GameObject g = selection.StopSelecting();
                if (g != null)
                {
                    if (Upper != null && g == Upper.gameObject)
                    {
                        UpperJoint.connectedBody = null;
                        UpperJoint.enabled = false;
                        Upper.Separated(true);
                        Upper = null;
                    }
                    else if (Lower != null && g == Lower.gameObject)
                    {
                        LowerJoint.connectedBody = null;
                        LowerJoint.enabled = false;
                        Lower.Separated(false);
                        Lower = null;
                    }
                    else
                    {
                        CombineAction comb = g.GetComponent<CombineAction>();
                        if (comb.part > part)
                        {
                            comb.transform.position = comb.transform.position + (UpperAttachPoint.position - comb.LowerAttachPoint.position);
                            UpperJoint.enabled = true;
                            UpperJoint.connectedBody = comb.GetComponent<Rigidbody2D>();
                            Upper = comb;
                            comb.Combined(true, this);
                            if (comb.GetMass() > SupportableMass) Break();
                        }
                        else
                        {
                            transform.position = transform.position + (comb.UpperAttachPoint.position - LowerAttachPoint.position);
                            LowerJoint.enabled = true;
                            LowerJoint.connectedBody = comb.GetComponent<Rigidbody2D>();
                            Lower = comb;
                            comb.Combined(false, this);
                        }
                    }
                    UpdateMovement();
                }
                if (Movement)
                {
                    MovementAction movement = GetComponent<MovementAction>();
                    if (movement != null) movement.enabled = true;
                }
            }
        }
    }
}
