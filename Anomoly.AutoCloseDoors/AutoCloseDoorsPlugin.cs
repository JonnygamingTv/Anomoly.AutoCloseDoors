using Anomoly.AutoCloseDoors.Models;
using Anomoly.AutoCloseDoors.Service;
using Anomoly.AutoCloseDoors.Storage;
using HarmonyLib;
using Rocket.API.Collections;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Events;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anomoly.AutoCloseDoors
{
    public class AutoCloseDoorsPlugin: RocketPlugin<AutoCloseDoorsPluginConfiguration>
    {
        private Harmony _harmony;

        public static AutoCloseDoorsPlugin Instance { get; private set; }

        public const string HARMONY_ID = "gg.avery.autoclosedoorsplugin";

        public PlayerDeathService PlayerDeathService { get; private set; }
        public PlayerConfigService PlayerConfigService { get; private set; }

        protected override void Load()
        {
            base.Load();
            Instance = this;

            Logger.Log("Loading AutoCloseDoorsPlugin...");

            _harmony = new Harmony(HARMONY_ID);

            Logger.Log("Patching BarricadeManager method...");
            _harmony.PatchAll();


            PlayerConfigService = new PlayerConfigService();
            if (Configuration.Instance.CancelOnPlayerDead)
                PlayerDeathService = new PlayerDeathService();
            U.Events.OnPlayerConnected += Events_OnPlayerConnected;
        }

        private void Events_OnPlayerConnected(Rocket.Unturned.Player.UnturnedPlayer player)
        {
            PlayerConfigService.GetPlayer(player.Id);
        }

        protected override void Unload()
        {
            base.Unload();
            Instance = null;
            Logger.Log("Unloading AutoCloseDoorsPlugin...");

            PlayerConfigService.PlayerData.Save();
            PlayerConfigService = null;
            if(PlayerDeathService != null)
                PlayerDeathService.Unload();
            U.Events.OnPlayerConnected -= Events_OnPlayerConnected;
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            {"command_autodoor", "[AutoDoor] Toggled auto door: {0}" },
            {"autodoor_closed","[AutoDoor] Door has been closed." }
        };

    }
}
