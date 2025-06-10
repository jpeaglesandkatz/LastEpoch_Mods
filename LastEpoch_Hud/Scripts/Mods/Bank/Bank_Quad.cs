using HarmonyLib;
using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace LastEpoch_Hud.Scripts.Mods.Bank
{
    //[RegisterTypeInIl2Cpp]
    public class Bank_Quad// : MonoBehaviour
    {
        //public Bank_Quad(System.IntPtr ptr) : base(ptr) { }
        //public static Bank_Quad? instance { get; private set; }

        /*public static Il2CppSystem.Collections.Generic.List<ItemContainer>? item_container_list = null;
        public static Vector2Int stash_size = new Vector2Int(24, 34);

        void Awake()
        {
            instance = this;
        }
        void Update()
        {

        }
        
        [HarmonyPatch(typeof(StashHolderItemContainer), "GetSize")]
        public class StashHolderItemContainer_GetSize
        {
            [HarmonyPrefix]
            static void Prefix(ref StashHolderItemContainer __instance, ref UnityEngine.Vector2Int __result)
            {
                if (item_container_list is null) { item_container_list = new Il2CppSystem.Collections.Generic.List<ItemContainer>(); }
                
                //__result = stash_size;

                StashItemContainer stash_item_container = __instance.ActiveContainer;
                if (stash_item_container is not null)
                {
                    if (stash_item_container.id == ContainerID.STASH)
                    {
                        ItemContainer item_container = stash_item_container.containers[0];
                        item_container.size = stash_size;
                        if (item_container_list is not null) { item_container_list.Add(item_container); }
                    }
                }

                //return false;
            }
            [HarmonyPostfix]
            static void Postfix(ref StashHolderItemContainer __instance, ref UnityEngine.Vector2Int __result)
            {
                Main.logger_instance?.Msg("result = " + __result.ToString());
            }
        }

        [HarmonyPatch(typeof(StashHolderItemContainer), "TryAddItem", new System.Type[] { typeof(ItemData), typeof(int), typeof(Vector2Int), typeof(Context) })]
        public class StashHolderItemContainer_TryAddItem
        {
            [HarmonyPrefix]
            static void Prefix(StashHolderItemContainer __instance, ref bool __result, ItemData __0, int __1, Vector2Int __2, Context __3)
            {
                Main.logger_instance?.Msg("Item pos x = " + __2.x + ", y = " + __2.y);
                Main.logger_instance?.Msg("Slot pos x = " + stash_size.x + ", y = " + stash_size.y);
                if ((__2.x <= stash_size.x) && (__2.y <= stash_size.y))
                {
                    Main.logger_instance?.Msg("Ok");
                    __result = true;
                    return false;
                }
                else
                {
                    Main.logger_instance?.Msg("No");
                    return true;
                }
            }
        }

        [HarmonyPatch(typeof(ItemContainer), "CheckSlotsOccupied")]
        public class ItemContainer_CheckSlotsOccupied
        {
            [HarmonyPrefix]
            static bool Prefix(ref ItemContainer __instance, ref bool __result, UnityEngine.Vector2Int __0, UnityEngine.Vector2Int __1)
            {
                Main.logger_instance?.Msg("ItemContainer.CheckSlotsOccupied() " + __result);
                bool r = true;
                if (item_container_list is not null)
                {
                    if (item_container_list.Contains(__instance))
                    {
                        Main.logger_instance?.Msg("Item pos x = " + __0.x + ", y = " + __0.y);
                        Main.logger_instance?.Msg("Item size x = " + __1.x + ", y = " + __1.y);
                        Main.logger_instance?.Msg("Conntainer size x = " + stash_size.x + ", y = " + stash_size.y);

                        if (((__0.x + __1.x) <= stash_size.x) && ((__0.y + __1.y) <= stash_size.y))
                        {
                            Main.logger_instance?.Msg("Ok");
                            __result = true;
                            r = false;
                        }
                    }
                }

                return r;
            }
        }*/
    }
}
