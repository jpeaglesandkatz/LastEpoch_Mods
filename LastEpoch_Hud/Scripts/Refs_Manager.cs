using MelonLoader;
using UnityEngine;
using Il2CppItemFiltering;
using Il2Cpp;

namespace LastEpoch_Hud.Scripts
{
    [RegisterTypeInIl2Cpp]
    public class Refs_Manager : MonoBehaviour
    {
        public Refs_Manager(System.IntPtr ptr) : base(ptr) { }
        public static Refs_Manager? instance { get; private set; }

        public static bool online = true;

        public static CharacterSelect? character_select;
        public static UIBase? game_uibase;        
        public static EpochInputManager? epoch_input_manager; //Use to block input        
        public static SceneList? scene_list;
        public static InventoryPanelUI? InventoryPanelUI = null;
        public static EternityCachePanelUI? EternityCachePanelUI = null;
        public static GameObject? BlessingsPanel = null;
        public static Actor? player_actor = null;
        public static ActorVisuals? player_visuals = null;
        public static Il2CppLE.Data.CharacterData? player_data = null;
        public static CharacterDataTracker? player_data_tracker = null;
        public static PlayerHealth? player_health = null;
        public static HealthPotion? health_potion = null;
        public static Stats? player_stats;
        public static GoldTracker? player_gold_tracker = null;
        public static LocalTreeData? player_treedata = null;
        public static CharacterClassList? character_class_list = null;        
        public static ExperienceTracker? exp_tracker = null;
        public static GroundItemManager? ground_item_manager = null;
        public static ItemContainersManager? item_containers_manager = null;
        public static ItemList? item_list = null;
        public static UniqueList? unique_list = null;
        public static SetBonusesList? set_bonuses_list = null;
        public static QuestList? quest_list = null;
        public static PlayerQuestListHolder? player_quest_list = null;
        public static ItemFilterManager? filter_manager = null;
        public static CameraManager? camera_manager = null;
        public static CraftingSlotManager? craft_slot_manager = null;
        public static UIPanel? craft_materials_holder = null;
        public static CraftingPanelUI? crafting_panel_ui = null;
        public static ProtectionClass? player_protection_class = null;
        public static GlobalDataTracker? player_golbal_data_tracker = null;
        public static MonolithZoneManager? monolith_zone_manager = null;
        public static MovingPlayer? player_moving = null;
        public static AbilityManager? ability_manager = null;

        void Awake()
        {
            instance = this;
        }
        void Update()
        {
            //Input
            epoch_input_manager = EpochInputManager.instance;

            //
            character_select = CharacterSelect.instance;
            if (character_select is not null) { character_select.OnOnlineTabChange = Action_SetOnline; }            

            //Ui
            game_uibase = UIBase.instance;
            InventoryPanelUI = game_uibase?.inventoryPanel?.instance?.GetComponent<InventoryPanelUI>();
            BlessingsPanel = InventoryPanelUI?.blessingPanel;
            EternityCachePanelUI = game_uibase?.eternityCachePanel?.instance?.GetComponent<EternityCachePanelUI>();
            crafting_panel_ui = game_uibase?.craftingPanel?.instance?.GetComponent<CraftingPanelUI>();
            craft_materials_holder = game_uibase?.craftingMaterialsPanel;

            //Managers
            craft_slot_manager = CraftingSlotManager.instance;
            ability_manager = AbilityManager.instance;
            ground_item_manager = GroundItemManager.instance;
            item_containers_manager = ItemContainersManager.Instance;

            //Lists
            character_class_list = CharacterClassList.instance;
            item_list = ItemList.instance;
            quest_list = QuestList.instance;
            scene_list = SceneList.instance;
            if (UniqueList.instance is null) { UniqueList.getUnique(0); }
            unique_list = UniqueList.instance;
            if (SetBonusesList.instance is null) { SetBonusesList.getEntry(0); }
            set_bonuses_list = SetBonusesList.instance;

            //Player
            player_actor = PlayerFinder.getPlayerActor();
            player_visuals = PlayerFinder.getPlayerVisuals();
            player_data = PlayerFinder.getPlayerData();
            player_data_tracker = PlayerFinder.getPlayerDataTracker();
            player_quest_list = player_actor?.gameObject.GetComponent<PlayerQuestListHolder>();
            player_health = PlayerFinder.getLocalPlayerHealth();
            player_moving = player_actor?.gameObject.GetComponent<MovingPlayer>();
            player_protection_class = player_actor?.gameObject.GetComponent<ProtectionClass>();
            health_potion = player_actor?.gameObject.GetComponent<HealthPotion>();

            player_stats = PlayerFinder.getLocalPlayerStats();
            exp_tracker = PlayerFinder.getExperienceTracker();
            player_treedata = PlayerFinder.getLocalTreeData();
            player_gold_tracker = PlayerFinder.getLocalGoldTracker();
            player_golbal_data_tracker = PlayerFinder.getGlobalDataTracker();
            filter_manager = ItemFilterManager.Instance;
            camera_manager = CameraManager.instance;
        }

        private static readonly System.Action<bool> Action_SetOnline = new System.Action<bool>(SetOnline);
        private static void SetOnline(bool result)
        {
            //Patched by https://github.com/RolandSolymosi // BUG: For some reason the game always return true in action delegates
            result = character_select?.isOnlineTabShowing ?? true;
            if (online != result)
            {
                Main.logger_instance?.Msg("Refs Manager : Online = " + result);
                online = result;
                if (Mods_Manager.instance is not null) { Mods_Manager.instance.SetActive(result); }
            }
        }
    }
}
