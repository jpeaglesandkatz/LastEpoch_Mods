using HarmonyLib;
using Il2Cpp;

namespace LastEpoch_Hud.Scripts.Mods.Items
{
    internal class Items_AutoStore_OnPickup
    {
        //AutoStore Materials, Keys, Woven echoes
        [HarmonyPatch(typeof(ItemContainersManager), "attemptToPickupItem")]
        public class ItemContainersManager_attemptToPickupItem
        {
            [HarmonyPostfix]
            static void Postfix(ref ItemContainersManager __instance, bool __result, ItemData __0, UnityEngine.Vector3 __1)
            {
                if ((__result) && (!Save_Manager.instance.IsNullOrDestroyed()) && (!Refs_Manager.InventoryPanelUI.IsNullOrDestroyed()))
                {
                    if ((Save_Manager.instance.data.Items.Pickup.Enable_AutoStore_OnDrop) &&
                        ((ItemList.isCraftingItem(__0.itemType)) || (Item.isKey(__0.itemType)) || (__0.itemType == 107)))
                    {
                        Refs_Manager.InventoryPanelUI.StoreMaterialsButtonPress();
                    }
                }
            }
        }
    }
}
