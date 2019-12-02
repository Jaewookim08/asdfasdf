using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Netrunner.ModuleComponents {
    public class DispenseAction: ModuleAction {
        public GameObject CanPrefab;
        public int DrinkCount = 2;

        private void Update() {
            if (GameInput.GetKeyDown(player, ActionKey) && DrinkCount > 0)
            {
                float dir = GetComponent<ChangeFacingDirectionAction>().FacingDir;
                GameObject g = Instantiate(CanPrefab);
                Rigidbody2D r2d = g.GetComponent<Rigidbody2D>();
                g.transform.position = transform.position + new Vector3(dir * 1, -.5f, 0);
                r2d.velocity = new Vector2(5f * dir, 1f);
                DrinkCount--;
            }
        }
        
    }
}