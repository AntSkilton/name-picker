using System;
using System.Collections.Generic;
using UnityEngine;

namespace NamePicker
{
    /// <summary>
    ///Business controller logic for record updates with view handling.
    /// </summary>
    
    public class UiController : MonoBehaviour
    {
        public Transform RostersVerticalLayout;
        public GameObject NameSlotPrefab;

        public List<NameSlot> AllNameSlots = new();
        public GameObject Content;
        
        public RosterBox OrderedRoster;
        public List<RosterBox> TeamRosters = new();
        private int m_rosterMode = 0; // 0=Ordered, 1=Red, 2= +Blue, 3= +Green, 4= +Yellow

        private void OnEnable()
        {
            // Initialise view from data
            PopulateRoster(OrderedRoster);

            m_rosterMode = 1; // this doesnt work but the mode needs initialising too
        }

        public void PopulateRoster(RosterBox roster)
        {
            // Init and reset
            var children = Content.transform.GetComponentsInChildren<NameSlot>();
            for (int i = 0; i < children.Length; i++)
            {
                Destroy(children[i].gameObject);
            }
            
            // Instantiate
            for (int i = 0; i < MainManager.Current.PersonData.Count; i++)
            {
                var newSlot = NameSlotPrefab;
                var component = newSlot.GetComponent<NameSlot>();
                component.recordId = MainManager.Current.PersonData[i].Id;
                component.nameLabel.text = MainManager.Current.PersonData[i].Name;
                Instantiate(newSlot, Content.transform);
            }
        }

        private void DeactivateAllRosters()
        {
            for (int i = 0; i < TeamRosters.Count; i++)
            {
                TeamRosters[i].gameObject.SetActive(false);
            }
            OrderedRoster.gameObject.SetActive(false);
        }

        public void CycleRosterTypes()
        {
            DeactivateAllRosters();
            m_rosterMode++;

            switch (m_rosterMode)
            {
                case 0 or > 3: // Ordered
                {
                    OrderedRoster.gameObject.SetActive(true);
                    m_rosterMode = 0;
                    return;
                }

                case 1: // Red and Blue
                {
                    for (int i = 0; i < 2; i++)
                    {
                        TeamRosters[i].gameObject.SetActive(true);
                    }

                    return;
                }

                case 2: // Red, Blue and Green
                {
                    for (int i = 0; i < 3; i++)
                    {
                        TeamRosters[i].gameObject.SetActive(true);
                    }

                    return;
                }

                case 3: // Red, Blue, Green and Yellow
                {
                    for (int i = 0; i < 4; i++)
                    {
                        TeamRosters[i].gameObject.SetActive(true);
                    }

                    return;
                }
            }
        }

        /*
        public void NamePick()
        {
            if (NameSlots.Count!=0){
                int randIndex = Random.Range(0, NameSlots.Count);
                ChosenNames.Add(SquadNames[randIndex]);
                ChosenNamesText.SetText(string.Join(", ", ChosenNames));
                
                SquadNames.RemoveAt(randIndex);
                NamesAvailableText.SetText(string.Join(", ", SquadNames));
                
            }
            else{
                Debug.LogError("List is empty. Reset names.");
                return;
            }
        }*/
    }   
}