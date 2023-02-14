using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NamePicker
{
    public class NameSlot : MonoBehaviour, IPointerClickHandler
    {
        public TextMeshProUGUI nameLabel;
        public Button removeBtn;

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
            Debug.Log("OnRemoveRecord NameSlot");
            Destroy(gameObject);
        }
		
        public void OnPointerClick(PointerEventData eventData)
        {
            OnRemoveRecord(); 
        }
    }
}

