using System;
using System.Collections.Generic;
using System.IO;
using NamePicker.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace NamePicker
{
    /// <summary>
    /// Entry point for the application.
    /// Provides access to the data with methods on updating it.
    /// </summary>
    public class MainManager : MonoBehaviour
    { 
        public static MainManager Current;
        public UiController UiController;
        [HideInInspector] public List<PersonData> PersonData = new();
        
        private string m_dataPath;

        private void Awake()
        {
            Current = this;
            m_dataPath = Application.dataPath + "/PersonDataFile.json";
            
            if (!File.Exists(m_dataPath))
            {
                File.Create(m_dataPath);
            }

            LoadRecordsFromJson();
        }

        public void AddRecord(string newName)
        {
            PersonData item = new PersonData()
           {
               Id = Guid.NewGuid().ToString(),
               Name = newName,
           };
           
           PersonData.Add(item);
           SaveRecordsToJson();
           
           // Repaint GUI
           UiController.PopulateRoster(null);
        }
        
        public void UpdateRecord(string recordId, string newName)
        {
            for (int i = 0; i < PersonData.Count; i++)
            {
                if (PersonData[i].Id == recordId)
                {
                    PersonData[i].Name = newName;
                }
            }
            
            SaveRecordsToJson();
            
            // Repaint GUI
            UiController.PopulateRoster(null);
        }
        
        public void DeleteRecord(string newName)
        {
            PersonData item = new PersonData()
            {
                Id = Guid.NewGuid().ToString(),
                Name = newName,
            };
           
            PersonData.Add(item);
            SaveRecordsToJson();
            
            // Repaint GUI
            UiController.PopulateRoster(null);
        }

        public void DeleteAllRecords()
        {
            PersonData.Clear();
            SaveRecordsToJson();
            UiController.PopulateRoster(null);
        }

        private void LoadRecordsFromJson()
        {
            string json = File.ReadAllText(m_dataPath);
            var data = JsonConvert.DeserializeObject<PersonDatas>(json);
            
            PersonData.Clear();
            for (int i = 0; i < data.PersonData.Count; i++)
            {
                PersonData.Add(data.PersonData[i]);    
            }
        }

        private void SaveRecordsToJson()
       {
           var datas = new PersonDatas();
           for (int i = 0; i < PersonData.Count; i++)
           {
               var data = new PersonData();
               data.Id = PersonData[i].Id;
               data.Name = PersonData[i].Name;
               datas.Add(data);
           }

           string json = JsonUtility.ToJson(datas, true);
           File.WriteAllText(m_dataPath, json);
        }
    }
}