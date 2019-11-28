using System;
using UnityEngine;

namespace Netrunner
{
    [RequireComponent(typeof(Collider2D))]
    public class Grabbable : MonoBehaviour
    {
        public Collider2D Collider { get; private set; }

        private void Start()
        {
            Collider = GetComponent<Collider2D>();
            if (Collider == null)
                throw new Exception("GameObject with Grabbable needs to have Collider");
        }
    }
}