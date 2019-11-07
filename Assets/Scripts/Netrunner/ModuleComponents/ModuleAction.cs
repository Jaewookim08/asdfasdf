using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Netrunner.UI;

namespace Netrunner.ModuleComponents {
    [RequireComponent(typeof (ModuleManager))]
    public abstract class ModuleAction : MonoBehaviour {
        public Sprite Icon;

        /// <summary>
        /// 들어있는 모듈의 ModuleManager
        /// </summary>
        public ModuleManager Module {get; private set;}
        
        protected int player => Module.PlayerInside;

        [SerializeField] protected GameInput.Key ActionKey;

        
        /// <summary>
        /// Called when player gets in the module
        /// </summary>
        /// <param name="player"></param>
        public void Init(int player) {
            Module = GetComponent<ModuleManager>();
            if (ActionKey != GameInput.Key.None) UIManager.current.AddAbility(Icon, GameInput.GetRealKey(Module.PlayerInside, ActionKey, 0));
        }
    }
}
