using System;
using System.Collections.Generic;

namespace NamePicker.Data
{
    [Serializable]
    public class PersonDatas
    {
        public List<PersonData> PersonDataList = new();
    }
    
    [Serializable]
    public class PersonData
    {
        public string Id;
        public string Name;
    }
}