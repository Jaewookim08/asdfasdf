using System;
using UnityEngine;

namespace _4d
{
    public class WindMillElement : MonoBehaviour
    {
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            transform.rotation = Quaternion.identity;
        }

        private Rigidbody2D _rigidbody;
    }
}