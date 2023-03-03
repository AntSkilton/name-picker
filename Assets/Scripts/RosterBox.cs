using System.Collections.Generic;
using UnityEngine;

namespace NamePicker
{
    public class RosterBox : MonoBehaviour
    {
        public GameObject Content;
        public bool ShouldShowEditButtons;
        [HideInInspector] public List<int> RandomNumbersForTeam = new();
    }
}