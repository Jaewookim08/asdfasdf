using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Netrunner.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager current;

        public GameObject AbilityPrefab;
        public RectTransform Abilities1, Abilities2;

        List<AbilityIndicator>[] AbilityPanels = new List<AbilityIndicator>[3] { null, new List<AbilityIndicator>(), new List<AbilityIndicator>() };
        int CurrentlyAddingPlayer;

        private void Start()
        {
            current = this;
        }

        public void ChangeAbilities(int player)
        {
            CurrentlyAddingPlayer = player;
            ClearAbilities(player);
        }
        public void ClearAbilities(int player)
        {
            foreach (AbilityIndicator a in AbilityPanels[player])
                Destroy(a.gameObject);
            AbilityPanels[player].Clear();
        }

        public AbilityIndicator AddAbility(Sprite sprite, KeyCode key)
        {
            GameObject g = Instantiate(AbilityPrefab, CurrentlyAddingPlayer == 1 ? Abilities1 : Abilities2);
            AbilityIndicator ind = g.GetComponent<AbilityIndicator>();
            ind.Init(sprite, key, CurrentlyAddingPlayer, AbilityPanels[CurrentlyAddingPlayer].Count);
            AbilityPanels[CurrentlyAddingPlayer].Add(ind);
            return ind;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
