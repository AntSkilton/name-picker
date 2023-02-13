using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NamePicker
{
    public class NameSlot : MonoBehaviour
    {
        public TextMeshProUGUI nameLabel;
        public Button removeBtn;

        
        
        private void Something()
        {
            removeBtn.onClick.Invoke();
        }
    }
}

