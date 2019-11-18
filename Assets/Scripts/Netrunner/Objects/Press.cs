using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Netrunner.ModuleComponents;

namespace Netrunner.SelectableObjects
{

    public class Press : MonoBehaviour, SelectableTarget
    {
        public Animation anim;
        public BoxCollider2D boxReal, boxTrig;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ModuleManager mm = collision.GetComponent<ModuleManager>();
            if (mm != null)
            {
                collision.transform.localScale = Vector3.Scale(collision.transform.localScale, new Vector3(1f, 0.2f, 1f));
                mm.ModuleDestroy();
            }
        }

        public void Act()
        {
            boxTrig.enabled = true;
            anim.Play();
        }

        public void Act(int player)
        {
            Act();
        }

        public void Recover()
        {
            boxTrig.enabled = false;
        }

        void SelectableTarget.SetGlow(int player, bool enabled)
        {
            throw new System.NotImplementedException();
        }

        void SelectableTarget.SetHighlight(int player, bool enabled)
        {
            throw new System.NotImplementedException();
        }

        bool SelectableTarget.IsSelectable(int player, string tag)
        {
            throw new System.NotImplementedException();
        }
    }

}
