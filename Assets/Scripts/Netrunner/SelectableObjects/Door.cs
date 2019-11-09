using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Netrunner.SelectableObjects {
    public class Door : TintSelectableTarget
    {
        public Collider2D collider;

        bool Open = false;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public override void Act(int player)
        {
            if(Open) transform.localScale = new Vector3(0.3f, 3f, 1f);
            else transform.localScale = new Vector3(1f, 3f, 1f);
            collider.enabled = Open;
            Open = !Open;
        }

        public override bool IsSelectable(int player, string tag)
        {
            return true;
        }
    }
}
