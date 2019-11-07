using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Netrunner.UI;

namespace Netrunner.ModuleComponents {
    [RequireComponent(typeof (ModuleManager))]
    public abstract class SelectingAction : ModuleAction {
        public Selection selection;

    }
}
