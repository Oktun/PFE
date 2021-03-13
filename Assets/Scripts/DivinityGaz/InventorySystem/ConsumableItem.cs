using System.Text;
using UnityEngine;

namespace DivinityGaz.InventorySystem
{
    [CreateAssetMenu(fileName = "New Consumable Item", menuName = "Inventory System/Items/Consumable")]
    public class ConsumableItem : InventoryItem
    {
        [Header("Consumable Information")]
        [SerializeField] private string useText = "Does something?";

        public override string GetInfoDisplayString ()
        {
            var builder = new StringBuilder();

            builder.Append(Name).AppendLine();
            builder.Append("<color=green>Use: ").Append(useText).Append("</color>").AppendLine();
            builder.Append("Max Stack: ").Append(MaxStack).AppendLine();

            return builder.ToString();
        }
        
        public void Use ()
        {

        }
    }

}