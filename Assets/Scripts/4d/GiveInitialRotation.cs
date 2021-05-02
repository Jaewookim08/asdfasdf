using System;
using UnityEngine;

namespace _4d
{
    public class GiveInitialRotation: MonoBehaviour
    {
        [SerializeField] private float _initialAngularVelocity;

        private void Start()
        {
            GetComponent<Rigidbody2D>().angularVelocity = _initialAngularVelocity;
        }
    }
}