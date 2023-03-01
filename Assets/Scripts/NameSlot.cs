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
            editBtn.onClick.AddListener(OnStartEdit);
            inputField.onEndEdit.AddListener(OnEndEdit);
        }

        private void OnDisable()
        {
            editBtn.onClick.RemoveListener(OnStartEdit);
            inputField.onEndEdit.RemoveListener(OnEndEdit);
        }

        public void EditSlotState(bool inEditMode) 
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

        private void OnStartEdit()
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

