using System;
using System.Collections.Generic;
using System.IO;
using NamePicker.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace NamePicker
{
    public class Main : MonoBehaviour
    {
       private string m_dataPath;
        private List<PersonData> m_personData = new();

        private void Start()
        {
            m_dataPath = Application.dataPath + "/PersonDataFile.json";
            if (!File.Exists(m_dataPath))
            {
                File.Create(m_dataPath);
            }
            
            AddRecord();
        }

        public void AddRecord()
        {
            PersonData data1 = new PersonData()
           {
               Id = Guid.NewGuid().ToString(),
               Name = "John Doe"//personName.text,
           };
           
           m_personData.Add(data1);
       }

       public void SaveRecordsToJson()
       {
           var data = new PersonData();
           var datas = new PersonDatas();
           for (int i = 0; i < m_personData.Count; i++)
           {
               data.Id = m_personData[i].Id;
               data.Name = m_personData[i].Name;
               datas.Add(data);
           }

           string json = JsonUtility.ToJson(datas, true);
           File.WriteAllText(m_dataPath, json);
        }

        public void LoadRecordsFromJson()
        {
            string json = File.ReadAllText(m_dataPath);
            var data = JsonConvert.DeserializeObject<PersonDatas>(json);
            
            m_personData.Clear();
            for (int i = 0; i < data.PersonData.Count; i++)
            {
                m_personData.Add(data.PersonData[i]);    
            }
        }
    }
}