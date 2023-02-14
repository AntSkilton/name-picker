using System;
using UnityEngine;

namespace NamePicker
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Current;
        private void Awake()
        {
            Current = this;
        }
        
        public event Action OnRemoveRecord;
        public void RemoveRecord() {
            OnRemoveRecord?.Invoke();
        }
    }
}