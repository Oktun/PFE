using UnityEngine;

namespace DivinityGaz.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private bool settingsUIEnable = false;
        [SerializeField] private bool inventoryUIEnable = false;

        public bool SettingsUIEnable
        {
            get { return settingsUIEnable; }
            set { settingsUIEnable = value; CursorBehaviour(); }
        }

        public bool InventoryUIEnable
        {
            get { return inventoryUIEnable; }
            set { inventoryUIEnable = value; CursorBehaviour(); }
        }

        public bool IsAbleToMove
        {
            get
            {
                return !settingsUIEnable && !inventoryUIEnable;
            }
        }

        private void Start ()
        {
            CursorBehaviour();
        }

        private void CursorBehaviour ()
        {
            if (!settingsUIEnable && !inventoryUIEnable)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            } else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }
}