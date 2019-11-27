using System.Collections;
using System.Collections.Generic;
using Netrunner.SelectableObjects;
using UnityEngine;

namespace Netrunner
{
    public class Selection : MonoBehaviour
    {
        public GameObject Cursor;
        public GameObject RangeInd;

        protected const float CursorSpeed = 10f;
        protected const float CancelTime = 0.5f;

        protected List<GameObject> Targets = new List<GameObject>();
        //private List<SelectableTarget> Components = new List<SelectableTarget>();
        protected Coroutine coroutine;
        protected float range;
        protected GameObject CurrentTarget;
        protected bool Joystick = false;
        protected Vector2 Offset;
        protected float StartTime;
        protected int PlayerInside = 0;

        public virtual void StartSelecting(int player, string tag, float range)
        {
            PlayerInside = player;
            this.range = range;
            Vector3 sc = transform.lossyScale;
            RangeInd.transform.localScale = new Vector3(range/sc.x, range/sc.x, 1);
            RangeInd.SetActive(true);
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
            if(coroutine!=null) StopCoroutine(coroutine);
            coroutine = null;

            Cursor.SetActive(false);
            RangeInd.SetActive(false);

            GameObject temp = CurrentTarget;
            CurrentTarget = null;
            return temp;
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

        protected virtual void UpdateTargets(string tag, float range2)  //modules, selectableTargets must be at z=0;
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

            for(int i=0; i<count; i++)
            {
                if((transform.position - targets[i].transform.position).sqrMagnitude <= range2)
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
