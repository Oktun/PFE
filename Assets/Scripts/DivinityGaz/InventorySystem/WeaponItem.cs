using System.Text;
using UnityEngine;

namespace DivinityGaz.InventorySystem
{
    [CreateAssetMenu(fileName = "New Weapon Item", menuName = "Inventory System/Items/Weapon")]
    public class WeaponItem : InventoryItem
    {
        [Header("Consumable Information")]
        [SerializeField] private string useText = "Does something?";
        [SerializeField] private DefaultWeapon weaponScript = null;

        public DefaultWeapon WeaponScript { get { return weaponScript; } }

        public override string GetInfoDisplayString ()
        {
            var builder = new StringBuilder();

            builder.Append(Name).AppendLine();
            builder.Append("<color=green>Use: ").Append(useText).Append("</color>").AppendLine();
            builder.Append("Max Stack: ").Append(MaxStack).AppendLine();

            return builder.ToString();
        }

        public void Use (InputSystem inputSystem)
        {
            inputSystem.SwitchSatesFunction(this);
        }
    }

}