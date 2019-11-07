using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Netrunner.ModuleComponents;

namespace Netrunner{
    [RequireComponent(typeof (HackableObject))]
    public class PlayerInitializer : MonoBehaviour {
        public int PlayerNum;

        // Start is called before the first frame update
        void Start() {
            GetComponent<HackableObject>().HackIn(PlayerNum);
        }
    }
}
