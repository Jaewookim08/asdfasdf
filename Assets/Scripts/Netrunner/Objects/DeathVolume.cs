using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Netrunner.ModuleComponents;

namespace Netrunner.Objects
{
    public class DeathVolume : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("ASdf");
            ModuleManager mm = collision.GetComponent<ModuleManager>();
            if (mm != null)
            {
                SpriteRenderer sr = collision.GetComponentInChildren<SpriteRenderer>(false);
                if (sr != null) sr.color = sr.color * 0.6f;
                mm.ModuleDestroy();
            }
        }
    }

}