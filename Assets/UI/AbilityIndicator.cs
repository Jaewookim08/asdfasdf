using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Netrunner.UI
{
    public class AbilityIndicator : MonoBehaviour
    {
        public Image icon;
        public Text text;

        public void Init(Sprite sprite, KeyCode key, int player, int pos)
        {
            icon.sprite = sprite;
            Debug.Log(key);
            text.text = new string((char)key, 1);
            RectTransform rt = (RectTransform)transform;
            if (player == 1)
                rt.anchoredPosition = new Vector2(80f * pos, 0f);
            else
            {
                rt.anchorMin = new Vector2(1f, 0f);
                rt.anchorMax = new Vector2(1f, 0f);
                rt.pivot = new Vector2(1f, 0f);
                rt.anchoredPosition = new Vector2(-80f * pos, 0f);
            }
        }
    }
}