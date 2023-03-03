using UnityEngine;

namespace NamePicker
{
    public class RosterBox : MonoBehaviour
    {
        public GameObject NameSlotPrefab;
        public GameObject Content;
        public bool ShouldShowEditButtons;

        public void PopulateRoster(int randomIdxToUse)
        {
            // Use the pre-calculated random ints from the source data to populate this roster in order
            var newSlot = NameSlotPrefab;
            var component = newSlot.GetComponent<NameSlot>();

            component.recordId = MainManager.Current.PersonData[randomIdxToUse].Id;
            component.nameLabel.text = MainManager.Current.PersonData[randomIdxToUse].Name;
            component.editBtn.gameObject.SetActive(ShouldShowEditButtons);
            component.removeBtn.gameObject.SetActive(ShouldShowEditButtons);
            
            Instantiate(newSlot, Content.transform);
        }

        public void InitRoster()
        {
            gameObject.SetActive(true);
            var children = Content.transform.GetComponentsInChildren<NameSlot>();
            for (int i = 0; i < children.Length; i++)
            {
                Destroy(children[i].gameObject);
            }
        }
    }
}