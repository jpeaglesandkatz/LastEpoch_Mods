using HarmonyLib;
using Il2Cpp;
using Il2CppItemFiltering;

namespace LastEpoch_Hud.Scripts.Mods.Items
{
    public class Items_AutoPickup_Items
    {
        public static bool CanRun()
        {
            bool result = false;
            if ((Scenes.IsGameScene()) && (Save_Manager.instance is not null))
            {
                if ((Save_Manager.instance.data.Items.Pickup.Enable_AutoPickup_Keys) ||
                        (Save_Manager.instance.data.Items.Pickup.Enable_AutoPickup_Materials) ||
                        (Save_Manager.instance.data.Items.Pickup.Enable_AutoPickup_FromFilter))
                {
                    result = true;
                }
            }
            return result;
        }        
        [HarmonyPatch(typeof(GroundItemManager), "dropItemForPlayer")]
        public class dropItemForPlayer
        {
            [HarmonyPrefix]
            static bool Prefix(ref GroundItemManager __instance, ref Actor __0, ref ItemData __1, ref UnityEngine.Vector3 __2, bool __3)
            {
                if (Scenes.IsGameScene())
                {
                    //Fix unique subtype
                    if (__1.isUniqueSetOrLegendary())
                    {
                        UniqueList.Entry unique = UniqueList.getUnique(__1.uniqueID);
                        if (!unique.subTypes.Contains((byte)__1.subType))
                        {
                            ushort backup = __1.subType;
                            if (unique.subTypes.Count > 0)
                            {
                                int rand = UnityEngine.Random.RandomRangeInt(0, unique.subTypes.Count);
                                __1.subType = unique.subTypes[rand];
                                __1.id[2] = unique.subTypes[rand]; //subtype
                                __1.RefreshIDAndValues();
                            }
                        }
                    }
                    //Fix Load items with more than 4 affixs
                    if ((__1.rarity == 5) || (__1.rarity == 6))
                    {
                        __1.rarity = 4;                        
                        __1.RefreshIDAndValues();                        
                    }
                }

                bool result = true;
                if ((CanRun()) && (Save_Manager.instance is not null))
                {
                    if (((Save_Manager.instance.data.Items.Pickup.Enable_AutoPickup_Keys) && (Item.isKey(__1.itemType))) ||
                        (__1.itemType == 107) || //107 = Woven Echo
                            ((Save_Manager.instance.data.Items.Pickup.Enable_AutoPickup_Materials) && (ItemList.isCraftingItem(__1.itemType))))
                    {
                        bool pickup = ItemContainersManager.Instance.attemptToPickupItem(__1, __0.position());
                        if (pickup)
                        {
                            if ((Save_Manager.instance.data.Items.Pickup.Enable_AutoStore_OnDrop) && (ItemList.isCraftingItem(__1.itemType)))
                            {
                                InventoryPanelUI.instance.StoreMaterialsButtonPress();
                            }
                            result = false;
                        }
                    }
                    else if ((__1.itemType < 34) && (Refs_Manager.filter_manager is not null) &&
                        ((Save_Manager.instance.data.Items.Pickup.Enable_AutoPickup_FromFilter) ||
                        (Save_Manager.instance.data.Items.Pickup.Enable_AutoSell_Hide)))
                    {
                        if (!Refs_Manager.filter_manager.Filter.IsNullOrDestroyed())
                        {
                            bool FilterShow = false;
                            bool FilterHide = false;
                            foreach (Rule rule in Refs_Manager.filter_manager.Filter.rules)
                            {
                                if ((rule.isEnabled) && (rule.Match(__1.TryCast<ItemDataUnpacked>())) &&
                                    (((rule.levelDependent) && (rule.LevelInBounds(__0.stats.level))) ||
                                    (!rule.levelDependent)))
                                {
                                    if (rule.type == Rule.RuleOutcome.SHOW) { FilterShow = true; break; }
                                    else if (rule.type == Rule.RuleOutcome.HIDE) { FilterHide = true; }
                                }
                            }
                            if ((FilterShow) && (Save_Manager.instance.data.Items.Pickup.Enable_AutoPickup_FromFilter))
                            {
                                bool pickup = ItemContainersManager.Instance.attemptToPickupItem(__1, __0.position());
                                if (pickup) { result = false; }

                            }
                            else if ((!FilterShow) && (FilterHide) && (Save_Manager.instance.data.Items.Pickup.Enable_AutoSell_Hide))
                            {
                                ItemDataUnpacked? item = __1.TryCast<ItemDataUnpacked>();
                                if (item is not null)
                                {
                                    __0.goldTracker.modifyGold(item.VendorSaleValue);
                                }
                                result = false;
                            }
                        }
                    }
                }

                return result;
            }
        }
    }
}
