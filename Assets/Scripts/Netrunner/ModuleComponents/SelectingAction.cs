using System.Collections;
using System.Collections.Generic;
using Netrunner.ModuleComponents.SelectableObjects;
using UnityEngine;

namespace Netrunner.ModuleComponents
{
    public class SelectingAction : ModuleAction
    {
        public GameObject Cursor;

        private const float CursorSpeed = 10f;

        private List<GameObject> Targets = new List<GameObject>();
        //private List<SelectableTarget> Components = new List<SelectableTarget>();
        private Coroutine coroutine;
        private float range;
        private GameObject CurrentTarget;
        private bool Joystick = false;
        private Vector2 Offset;
        protected void StartSelecting(string tag, float range)
        {
            this.range = range;
            Joystick = false;
            Cursor.transform.localPosition = Offset = Vector3.zero;
            Cursor.layer = Module.PlayerInside == 1 ? LayerMask.NameToLayer("Player1View") : LayerMask.NameToLayer("Player2View");
            Cursor.SetActive(true);
            CurrentTarget = null;

            MovementAction movement = GetComponent<MovementAction>();
            if (movement != null)
            {
                movement.enabled = false;
            }
            coroutine = StartCoroutine(UpdateCoroutine(tag, range));
        }
        protected GameObject StopSelecting()
        {
            foreach(GameObject g in Targets)
                g.GetComponent<SelectableTarget>().SetGlow(Module.PlayerInside, false);

            Targets.Clear();
            //Components.Clear();
            StopCoroutine(coroutine);

            Cursor.SetActive(false);
            MovementAction movement = GetComponent<MovementAction>();
            if(movement != null)
            {
                movement.enabled = true;
            }
            return CurrentTarget;
        }

        IEnumerator UpdateCoroutine(string tag, float range)
        {
            float range2 = range * range;
            while (true)
            {
                UpdateTargets(tag, range2);

                Vector2 joystick = GameInput.getLeftJoystickVector2(Module.PlayerInside);
                Joystick |= joystick.sqrMagnitude > 0f;

                if (Joystick)
                {
                    Offset = joystick * range;
                }
                else {

                    Offset += GameInput.GetDirection(Module.PlayerInside) * (CursorSpeed * Time.deltaTime);
                    if (Offset.sqrMagnitude > range2) Offset *= Mathf.Sqrt(range2 / Offset.sqrMagnitude);
                }

                Cursor.transform.position = transform.position + (Vector3)Offset;
                ChangeTarget();
                yield return null;
            }
        }

        private void UpdateTargets(string tag, float range2)  //modules, selectableTargets must be at z=0;
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
            for(int i=0; i<targets.Length; i++)
            {
                if((transform.position - targets[i].transform.position).sqrMagnitude <= range2)
                {
                    if (!Targets.Contains(targets[i])) //add targets that newly came into range
                    {
                        Targets.Add(targets[i]);
                        targets[i].GetComponent<SelectableTarget>().SetGlow(Module.PlayerInside, true);
                    }
                }
                else //out of range
                {
                    if (Targets.Remove(targets[i])) //remove targets that went out of range
                    {
                        targets[i].GetComponent<SelectableTarget>().SetGlow(Module.PlayerInside, false);
                        //if(CurrentTarget == targets[i]) ChangeTarget(); //change CurrentTarget if current target went out of range
                    }
                }
            }
        }

        private void ChangeTarget()
        {
            if (Targets.Count == 0)
            {
                CurrentTarget = null;
                return;
            }

            Vector2 pos = (Vector2)transform.position + Offset;
            float min = 100000f;
            GameObject g = null;
            for(int i=0; i<Targets.Count; i++)
            {
                float sqrmag = ((Vector2)Targets[i].transform.position - pos).sqrMagnitude;
                if (sqrmag < min)
                {
                    min = sqrmag;
                    g = Targets[i];
                }
            }
            if (g != CurrentTarget)
            {
                if(CurrentTarget!=null) CurrentTarget.GetComponent<SelectableTarget>().SetHighlight(Module.PlayerInside, false);
                g.GetComponent<SelectableTarget>().SetHighlight(Module.PlayerInside, true);
                CurrentTarget = g;
            }
        }
    }
}
