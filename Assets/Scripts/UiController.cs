using System.Collections.Generic;
using UnityEngine;

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
        
        public RosterBox SourceRoster;
        public RosterBox RandomOrderedRoster;
        public List<RosterBox> TeamRosters = new();

        private RosterMode m_currentRosterMode = RosterMode.RandomOrdered;
        private List<int> m_randomIntList = new();
        //private List<int> m_teamQuantities = new();

        private void OnEnable()
        {
            DrawRosters(); // Initialise view from data
        }

        public void DrawRosters(RosterMode _rosterMode = default)
        {
            // Any CRUD to the data source will safely re-randomise everything
            m_randomIntList = RandomGenerator.SetupRandomIntList();

            InitNonSourceRosters(); // Init: Disable rosters
            SourceRoster.InitRoster();
            
            for (int i = 0; i < m_randomIntList.Count; i++)
            {
                // Redraw source roster
                SourceRoster.PopulateRoster(i);
            }

            switch (m_currentRosterMode)
            {
                case RosterMode.RandomOrdered:
                {
                    RandomOrderedRoster.InitRoster();
                    for (int i = 0; i < m_randomIntList.Count; i++)
                    {
                        RandomOrderedRoster.PopulateRoster(m_randomIntList[i]);
                    }
                    break;
                }
                case RosterMode.RedBlue:
                    PopulateTeamRosters(new List<RosterBox> { TeamRosters[0], TeamRosters[1] });
                    break;

                case RosterMode.RedBlueGreen:
                    PopulateTeamRosters(new List<RosterBox> { TeamRosters[0], TeamRosters[1], TeamRosters[2] });
                    break;
                
                case RosterMode.RedBlueGreenYellow:
                    PopulateTeamRosters(new List<RosterBox> { TeamRosters[0], TeamRosters[1], TeamRosters[2], TeamRosters[3] });
                    break;
            }
        }

        public void PopulateTeamRosters(List<RosterBox> rostersToPopulate)
        {
            for (int i = 0; i < rostersToPopulate.Count; i++)
            {
                rostersToPopulate[i].InitRoster();
            }

            var rosterIdx = 0;
            var randomIntListIdx = 0;
            while (randomIntListIdx < m_randomIntList.Count)
            {
                rostersToPopulate[rosterIdx].PopulateRoster(m_randomIntList[randomIntListIdx]);
                randomIntListIdx++;
                rosterIdx++;

                if (rosterIdx >= rostersToPopulate.Count)
                {
                    rosterIdx = 0;
                }
            }
        }

        private void InitNonSourceRosters()
        {
            for (int i = 0; i < TeamRosters.Count; i++)
            {
                TeamRosters[i].gameObject.SetActive(false);
            }
            RandomOrderedRoster.gameObject.SetActive(false);
        }

        public void CycleRosterTypes()
        {
            m_currentRosterMode++;
            if (m_currentRosterMode > (RosterMode)3)
            {
                m_currentRosterMode = 0;
            }

            DrawRosters(m_currentRosterMode);
        }
        
        public void RandomiseNames() // For Unity to see this event as it takes a non built-in argument
        {
            DrawRosters(m_currentRosterMode);
        }
    }   
}