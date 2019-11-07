using System.Collections;
using System.Collections.Generic;
using Netrunner.SelectableObjects;
using UnityEngine;

namespace Netrunner
{
    public class Selection : MonoBehaviour
    {
        public GameObject Cursor;

        private const float CursorSpeed = 10f;
        private const float CancelTime = 0.5f;

        private List<GameObject> Targets = new List<GameObject>();
        //private List<SelectableTarget> Components = new List<SelectableTarget>();
        Coroutine coroutine;
        float range;
        GameObject CurrentTarget;
        bool Joystick = false;
        Vector2 Offset;
        float StartTime;
        int PlayerInside = 0;

        public void StartSelecting(int player, string tag, float range)
        {
            PlayerInside = player;
            this.range = range;
            Joystick = false;
            Cursor.transform.localPosition = Offset = Vector3.zero;
            Cursor.layer = PlayerInside == 1 ? LayerMask.NameToLayer("Player1View") : LayerMask.NameToLayer("Player2View");
            Cursor.SetActive(true);
            CurrentTarget = null;
            StartTime = Time.time;

            coroutine = StartCoroutine(UpdateCoroutine(tag, range));
        }
        public GameObject StopSelecting()
        {
            foreach(GameObject g in Targets)
                g.GetComponent<SelectableTarget>().SetGlow(PlayerInside, false);

            Targets.Clear();
            //Components.Clear();
            StopCoroutine(coroutine);

            Cursor.SetActive(false);
            return CurrentTarget;
        }

        IEnumerator UpdateCoroutine(string tag, float range)
        {
            float range2 = range * range;
            while (true)
            {
                UpdateTargets(tag, range2);

                Vector2 joystick = GameInput.GetLeftJoystickVector(PlayerInside);
                Joystick |= joystick.sqrMagnitude > 0f;

                if (Joystick)
                {
                    Offset = joystick * range;
                }
                else {

                    Offset += GameInput.GetDirection(PlayerInside) * (CursorSpeed * Time.deltaTime);
                    if (Offset.sqrMagnitude > range2) Offset *= Mathf.Sqrt(range2 / Offset.sqrMagnitude);
                }

                Cursor.transform.position = transform.position + (Vector3)Offset;
                ChangeTarget();
                yield return null;
            }
        }

        private void UpdateTargets(string tag, float range2)  //modules, selectableTargets must be at z=0;
        {
            GameObject[] tagTargets = GameObject.FindGameObjectsWithTag(tag);
            SelectableTarget[] selectables = new SelectableTarget[tagTargets.Length];
            GameObject[] targets = new GameObject[tagTargets.Length];

            for (int i = 0; i < targets.Length; i++)
                selectables[i] = tagTargets[i].GetComponent<SelectableTarget>();
            for (int i = 0; i < targets.Length; i++)
            {
                if (selectables[i] == null) targets[i] = tagTargets[i].transform.parent.gameObject;
                else targets[i] = tagTargets[i];
            }                    

            for(int i=0; i<targets.Length; i++)
            {
                if((transform.position - targets[i].transform.position).sqrMagnitude <= range2)
                {
                    SelectableTarget selectable = targets[i].GetComponent<SelectableTarget>();
                    if (!Targets.Contains(targets[i])) //add targets that newly came into range
                    {
                        if (!selectable.IsSelectable(PlayerInside)) continue;
                        Targets.Add(targets[i]);
                        selectable.SetGlow(PlayerInside, true);
                    }
                    else if (!selectable.IsSelectable(PlayerInside)) Targets.Remove(targets[i]);
                }
                else //out of range
                {
                    if (Targets.Remove(targets[i])) //remove targets that went out of range
                    {
                        targets[i].GetComponent<SelectableTarget>().SetGlow(PlayerInside, false);
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
            if (Time.time - StartTime > CancelTime && Offset.magnitude < min) g = null;
            if (g != CurrentTarget)
            {
                if(CurrentTarget!=null) CurrentTarget.GetComponent<SelectableTarget>().SetHighlight(PlayerInside, false);
                if(g != null) g.GetComponent<SelectableTarget>().SetHighlight(PlayerInside, true);
                CurrentTarget = g;
            }
        }
    }
}
