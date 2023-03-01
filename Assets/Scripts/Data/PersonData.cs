using System;
using System.Collections.Generic;

namespace NamePicker.Data
{
    [Serializable]
    public class PersonDatas
    {
        public List<PersonData> PersonData = new();

        public void Add(PersonData data)
        {
            PersonData.Add(data);
        }
    }
    
    [Serializable]
    public class PersonData
    {
        public string Id;
        public string Name;
    }
}