using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NamePicker
{
    /// <summary>
    ///Business controller logic for record updates with view handling.
    /// </summary>
    public class UiController : MonoBehaviour
    {
        public enum RosterMode
        {
            RandomOrdered,
            RedBlue,
            RedBlueGreen,
            RedBlueGreenYellow
        }

        public GameObject NameSlotPrefab;

        public RosterBox SourceRoster;
        public RosterBox RandomOrderedRoster;
        public List<RosterBox> TeamRosters = new();

        private RosterMode m_rosterMode = RosterMode.RandomOrdered;
        private List<int> m_randomIntList = new();

        private void OnEnable()
        {
            DrawRosters(); // Initialise view from data
        }

        public void DrawRosters(RosterMode _rosterMode = default)
        {
            SetupRandomIntList(); // Any CRUD to the data source will safely re-randomise everything
            //AssignRandomNumbersToRosters(); // Each roster will have a unique random number 
            DeactivateNonSourceRosters(); // Init: Disable rosters
            PopulateSourceRoster(); // Redraw source roster
            
            switch (_rosterMode)
            {
                case RosterMode.RandomOrdered:
                    PopulateRosters(new List<RosterBox> { RandomOrderedRoster });
                    break;
                
                case RosterMode.RedBlue:
                    PopulateRosters(new List<RosterBox> { TeamRosters[0], TeamRosters[1] });
                    break;

                case RosterMode.RedBlueGreen:
                    PopulateRosters(new List<RosterBox> { TeamRosters[0], TeamRosters[1], TeamRosters[2] });
                    break;
                
                case RosterMode.RedBlueGreenYellow:
                    PopulateRosters(new List<RosterBox> { TeamRosters[0], TeamRosters[1], TeamRosters[2], TeamRosters[3] });
                    break;
            }
        }

        private void PopulateRosters(List<RosterBox> _rosters = default)
        {
            _rosters ??= new List<RosterBox> { SourceRoster };

            // Initialise and reset
            for (int i = 0; i < _rosters.Count; i++)
            {
                _rosters[i].gameObject.SetActive(true);
                var children = _rosters[i].Content.transform.GetComponentsInChildren<NameSlot>();
                
                for (int j = 0; j < children.Length; j++)
                {
                    Destroy(children[j].gameObject);
                }
            }

            // For each team
            for (int i = 0; i < _rosters.Count; i++)
            {
                var newSlot = NameSlotPrefab;
                var component = newSlot.GetComponent<NameSlot>();

                component.recordId = MainManager.Current.PersonData[i].Id;
                component.nameLabel.text = MainManager.Current.PersonData[i].Name;
                Instantiate(newSlot, _rosters[i].Content.transform);
                
                /*// Instantiate the assigned random record
                for (int j = 0; j < _rosters[i].RandomNumbersForTeam.Count; j++)
                {
                    var newSlot = NameSlotPrefab;
                    var component = newSlot.GetComponent<NameSlot>();

                    component.recordId = MainManager.Current.PersonData[i].Id;
                    //component.recordId = MainManager.Current.PersonData[_rosters[i].RandomNumbersForTeam[j]].Id;
                    component.nameLabel.text = MainManager.Current.PersonData[j].Name;
                    //component.nameLabel.text = MainManager.Current.PersonData[m_randomIntList[i]].Name;

                    Instantiate(newSlot, _rosters[i].Content.transform);
                    //component.SetEditableButtons(_roster.ShouldShowEditButtons);  
                }*/
            }
        }

        private void PopulateSourceRoster()
        {
            // Init and reset
            var children = SourceRoster.Content.transform.GetComponentsInChildren<NameSlot>();
            for (int i = 0; i < children.Length; i++)
            {
                Destroy(children[i].gameObject);
            }
            
            // Instantiate for each team
            for (int i = 0; i < MainManager.Current.PersonData.Count; i++)
            {
                var newSlot = NameSlotPrefab;
                var component = newSlot.GetComponent<NameSlot>();

                component.recordId = MainManager.Current.PersonData[i].Id;
                component.nameLabel.text = MainManager.Current.PersonData[i].Name;

                Instantiate(newSlot, SourceRoster.Content.transform);
                //component.SetEditableButtons(_roster.ShouldShowEditButtons);  
            }
        }

        private void AssignRandomNumbersToRosters()
        {
            var teamQuantities = CalculateTeamQuantities(MainManager.Current.PersonData.Count, m_rosterMode);
            var idx = 0;

            if (teamQuantities.Count == 1)
            {
                RandomOrderedRoster.RandomNumbersForTeam.AddRange(m_randomIntList);
                return;
            }
            
            // For each team, assign the random number across the teams needed
            for (int i = 0; i < teamQuantities.Count; i++)
            {
                TeamRosters[teamQuantities[i]].RandomNumbersForTeam.Add(m_randomIntList[idx]);
                idx++;
            }
        }


        private List<int> CalculateTeamQuantities(int totalListQuantity, RosterMode teamQuantity)
        {
            teamQuantity += 1;
            var teamQuantities = new List<int>();
            
            var remainder = totalListQuantity % (int)teamQuantity;
            for (int i = 0; i < (int)teamQuantity; i++)
            {
                teamQuantities.Add((totalListQuantity - remainder) / (int)teamQuantity);
            }
            
            var idx = 0;
            while (remainder > 0)
            {
                teamQuantities[idx] += 1;
                idx++;
                remainder--;
            }

            return teamQuantities;
        }

        private void SetupRandomIntList()
        {
            m_randomIntList.Clear();
            var availableInts = new List<int>();
            
            for (int i = 0; i < MainManager.Current.PersonData.Count; i++)
            {
                availableInts.Add(i);
            }
            
            for (int i = 0; i < MainManager.Current.PersonData.Count; i++)
            {
                int randomIdx = Random.Range(0, availableInts.Count);
                m_randomIntList.Add(availableInts[randomIdx]);
                availableInts.RemoveAt(randomIdx);
            }
        }

        private void DeactivateNonSourceRosters()
        {
            for (int i = 0; i < TeamRosters.Count; i++)
            {
                TeamRosters[i].gameObject.SetActive(false);
            }
            RandomOrderedRoster.gameObject.SetActive(false);
        }

        public void CycleRosterTypes()
        {
            m_rosterMode++;
            if (m_rosterMode > (RosterMode)3)
            {
                m_rosterMode = 0;
            }

            DrawRosters(m_rosterMode);
        }
        
        public void RandomiseNames() // For Unity to see this event as it takes a non built-in argument
        {
            DrawRosters(m_rosterMode);
        }
    }   
}