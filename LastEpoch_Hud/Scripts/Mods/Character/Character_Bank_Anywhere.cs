using MelonLoader;
using UnityEngine;

namespace LastEpoch_Hud.Scripts.Mods.Character
{
	[RegisterTypeInIl2Cpp]
	public class Character_Bank_Anywhere : MonoBehaviour
	{
		public static Character_Bank_Anywhere? instance { get; private set; }
		public Character_Bank_Anywhere(System.IntPtr ptr) : base(ptr) { }

		void Awake()
		{
			instance = this;
		}
		void Update()
		{
			if ((Scenes.IsGameScene()) && (Refs_Manager.game_uibase is not null) && (Save_Manager.instance is not null))
			{
				if (Input.GetKeyDown(Save_Manager.instance.data.KeyBinds.BankStashs))
				{
                    Refs_Manager.game_uibase.stashPanel.instance.active = !Refs_Manager.game_uibase.stashPanel.instance.active;
                }					
			}
		}
	}
}
