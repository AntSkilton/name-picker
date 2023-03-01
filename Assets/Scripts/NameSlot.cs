using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NamePicker
{
    public class NameSlot : MonoBehaviour
    {
        public TextMeshProUGUI nameLabel;
        public TMP_InputField inputField;
        public Button editBtn;
        public Button removeBtn;

        private bool m_isInEditMode = false;

        public void SwapState(bool setInEditMode) 
        {
            m_isInEditMode = setInEditMode;
            
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

        public void OnEndEdit()
        { 
            SwapState(false);
            nameLabel.text = inputField.text; // Todo: remove this as it'll be auto popped
            
            // send event to main to say this has been
            // updated and redraw the view from there
        }
    }
}

