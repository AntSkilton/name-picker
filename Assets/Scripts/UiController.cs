using System.Collections.Generic;
using UnityEngine;

namespace NamePicker
{
    public class UiController : MonoBehaviour
    {
        public Transform RostersVerticalLayout;
        public GameObject NameSlotPrefab;
        
        public List<NameSlot> AllNameSlots { get; set; }
        public GameObject Content;
        
        public RosterBox OrderedRoster;
        public List<RosterBox> TeamRosters = new();
        private int m_rosterMode = 0; // 0=Ordered, 1=Red, 2= +Blue, 3= +Green, 4= +Yellow

        private void OnEnable()
        {
            // Initialise content - messes with instantiation though
            for (int i = 0; i < Content.transform.childCount; i++)
            {
                //Destroy(Content.transform.GetChild(i).gameObject);
            }
            
            // Load previous name data from memory here?
            AllNameSlots = new List<NameSlot>();
            //Application.persistentDataPath
            m_rosterMode = 1; // this doesnt work but the mode needs initialising too
            PopulateRoster(OrderedRoster);
        }

        private void PopulateRoster(RosterBox roster)
        {
            var some = new NameSlot();
            AllNameSlots.Add(some);
            AllNameSlots.Add(some);
            AllNameSlots.Add(some);

            for (int i = 0; i < AllNameSlots.Count; i++)
            {
                Instantiate(NameSlotPrefab, Content.transform);
                var slot = Content.gameObject.transform.GetChild(i).GetComponent<NameSlot>();
                slot.nameLabel.text = "Ant Skilton";
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
                    for (int i = 0; i < 2; i++) { TeamRosters[i].gameObject.SetActive(true); }
                    return;
                }
                
                case 2: // Red, Blue and Green
                {
                    for (int i = 0; i < 3; i++) { TeamRosters[i].gameObject.SetActive(true); }
                    return;
                }
                
                case 3: // Red, Blue, Green and Yellow
                {
                    for (int i = 0; i < 4; i++) { TeamRosters[i].gameObject.SetActive(true); }
                    return;
                }
            }
        }

        private void Prime()
        {    
            /*AllNames.AddRecord("Ant");
            AllNames.AddRecord("Ant 1");
            AllNames.AddRecord("Ant 2");
            AllNames.AddRecord("Ant 3");*/
           
            //AllNames.Sort();
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
        public void ResetNames()
        {
           // ChosenNames.Clear();
            //SquadNames.Clear();
            Prime();
        }


        public void OnAddRecord()
        {
            var newRecord = TouchScreenKeyboard.Open(name);
            Debug.Log(newRecord.text);
        }
    }   
}