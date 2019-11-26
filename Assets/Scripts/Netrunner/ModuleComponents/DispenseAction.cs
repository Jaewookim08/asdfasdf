using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Netrunner.ModuleComponents {
    public class DispenseAction: ModuleAction {
        public GameObject CanPrefab;

        private void Update() {
            if (GameInput.GetKey(player, ActionKey))
            {
                float dir = 1;
                GameObject g = Instantiate(CanPrefab);
                Rigidbody2D r2d = g.GetComponent<Rigidbody2D>();
                g.transform.position = transform.position + new Vector3(dir * 1, -.5f, 0);
                r2d.velocity = new Vector2(5f * dir, 1f);
            }
        }
        
    }
}