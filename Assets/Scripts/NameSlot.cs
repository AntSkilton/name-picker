using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NamePicker
{
    /// <summary>
    /// This view object is responsible for dispatching events and sending
    /// them off to the Main Manager to handle data updates before repainting the GUI.
    /// </summary>
    public class NameSlot : MonoBehaviour
    {
        public TextMeshProUGUI nameLabel;
        public TMP_InputField inputField;
        public Button editBtn;
        public Button removeBtn;

        [HideInInspector] public string recordId = String.Empty;
        private bool m_isInEditMode;

        private void OnEnable()
        {
            inputField.onEndEdit.AddListener(OnEndEdit);
            editBtn.onClick.AddListener(OnStartEdit);
            removeBtn.onClick.AddListener(OnRemoveSlot);
        }

        private void OnDisable()
        {
            inputField.onEndEdit.RemoveListener(OnEndEdit);
            editBtn.onClick.RemoveListener(OnStartEdit);
            removeBtn.onClick.RemoveListener(OnRemoveSlot);
        }

        public void OnRemoveSlot()
        {
            MainManager.Current.DeleteRecord(recordId);
        }

        private void EditSlotState(bool inEditMode) 
        {
            m_isInEditMode = inEditMode;
            
            switch (m_isInEditMode)
            {
                case true:
                    inputField.gameObject.SetActive(true);
                    inputField.ActivateInputField();
                    nameLabel.gameObject.SetActive(false);
                    editBtn.gameObject.SetActive(false);
                    removeBtn.gameObject.SetActive(false);

                    inputField.text = nameLabel.text;
                    break;
                
                case false:
                    inputField.gameObject.SetActive(false);
                    inputField.DeactivateInputField();
                    nameLabel.gameObject.SetActive(true);
                    editBtn.gameObject.SetActive(true);
                    removeBtn.gameObject.SetActive(true);
                    break;
            }
        }

        public void OnStartEdit()
        {
            EditSlotState(true);
        }

        private void OnEndEdit(string newName)
        { 
            EditSlotState(false);
            MainManager.Current.UpdateRecord(recordId, newName);
        }
    }
}

