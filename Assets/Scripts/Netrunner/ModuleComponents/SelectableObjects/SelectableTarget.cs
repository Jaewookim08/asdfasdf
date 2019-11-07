using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Netrunner.ModuleComponents.SelectableObjects {
    public abstract class SelectableTarget : MonoBehaviour //interface로 바꿔도 될 것 같네요
    {
        protected bool[] Glow = new bool[3];

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public abstract void Act(int player);

        public abstract void SetGlow(int player, bool enabled);

        public abstract void SetHighlight(int player, bool enabled);
    }
}