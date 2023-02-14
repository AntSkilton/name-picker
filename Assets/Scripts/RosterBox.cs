using System;
using System.Collections.Generic;
using UnityEngine;

namespace NamePicker
{
    public class RosterBox : MonoBehaviour
    {
        public NameSlot NameSlotPrefab;
        public List<NameSlot> NameSlots;
        public bool CanRemoveRecord = false;
        public Transform ScrollBoxContent;

        private void OnEnable()
        {
            EventManager.Current.OnRemoveRecord += OnRemoveRecord;
        }

        private void OnDisable()
        {
            EventManager.Current.OnRemoveRecord -= OnRemoveRecord;
        }

        private void OnRemoveRecord()
        {
           // NameSlots[0].nameLabel.SetText("working");
        }

        public void AddRecord(String newName)
        {
            var prefab = Instantiate(NameSlotPrefab);
            prefab.nameLabel.SetText(newName);
            NameSlots.Add(prefab);
        }
    }
}