using System;
using System.Collections.Generic;
using Netrunner.ModuleComponents;
using UnityEngine;
using UnityEngine.Animations;

namespace Netrunner
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Grabbable : MonoBehaviour
    {
        public static IReadOnlyCollection<Grabbable> All => _all;
        [NonSerialized] public bool isGrabbed = false;
        
        public Vector3 Position => transform.position;
        public Rigidbody2D Rigidbody { get; private set; }

        private PositionConstraint _positionConstraint;

        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            _positionConstraint = GetComponent<PositionConstraint>();
        }

        private static readonly HashSet<Grabbable> _all = new HashSet<Grabbable>();
        private void OnEnable()
        {
            _all.Add(this);
        }

        private void OnDisable()
        {
            _all.Remove(this);
        }
    }
}