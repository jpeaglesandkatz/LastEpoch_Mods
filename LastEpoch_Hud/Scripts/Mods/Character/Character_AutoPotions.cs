using MelonLoader;
using UnityEngine;

namespace LastEpoch_Hud.Scripts.Mods.Character
{
    [RegisterTypeInIl2Cpp]
    public class Character_AutoPotions : MonoBehaviour
    {
        public static Character_AutoPotions? instance { get; private set; }
        public Character_AutoPotions(System.IntPtr ptr) : base(ptr) { }

        void Awake()
        {
            instance = this;
        }
        void Update()
        {
            if ((Scenes.IsGameScene()) && (Refs_Manager.player_health is not null) && (Refs_Manager.health_potion is not null) && (Save_Manager.instance is not null))
            {
                if (Save_Manager.instance.data.Character.Cheats.Enable_AutoPot)
                {
                    int player_health_percent = (int)(Refs_Manager.player_health.currentHealth / Refs_Manager.player_health.maxHealth * 100);
                    int auto_pot_percent = (int)(Save_Manager.instance.data.Character.Cheats.autoPot / 255 * 100);
                    if (player_health_percent < auto_pot_percent) { Refs_Manager.health_potion.UsePotion(); }
                }                
            }
        }
    }
}
