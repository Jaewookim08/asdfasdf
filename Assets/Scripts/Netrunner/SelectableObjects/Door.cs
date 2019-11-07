using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Netrunner.SelectableObjects {
    public class Door : TintSelectableTarget
    {
        bool Open = true;
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
            Open = !Open;
        }

        public override bool IsSelectable()
        {
            return true;
        }
    }
}
