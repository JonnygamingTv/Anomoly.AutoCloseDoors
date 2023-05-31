using HarmonyLib;
using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Core.Utils;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anomoly.AutoCloseDoors.Patches
{
    [HarmonyPatch(typeof(InteractableDoor))]
    internal class InteractableDoorPatch
    {

        [HarmonyPostfix]
        [HarmonyPatch("ReceiveToggleRequest")]
        static void ReceiveToggleRequest_Postfix(InteractableDoor __instance, in ServerInvocationContext context, bool desiredOpen)
        {
            if (AutoCloseDoorsPlugin.Instance.State != PluginState.Loaded)
                return;

            if (!desiredOpen)
                return;

            var player = context.GetPlayer();

            var doorPlayer = AutoCloseDoorsPlugin.Instance.PlayerConfigService.GetPlayer(player.channel.owner.playerID.steamID.m_SteamID.ToString());

            if (!doorPlayer.Enabled)
                return;


            var config = AutoCloseDoorsPlugin.Instance.Configuration.Instance;


            if (config.AllowAutoCloseUnclaimedDoors && __instance.owner.m_SteamID.ToString() != player.channel.owner.playerID.steamID.m_SteamID.ToString())
                return;

            int delay = 5;

            if(config.CloseDelay >= 5)
                delay = config.CloseDelay;

            bool shouldCancel = false;

            if (config.CancelOnPlayerDead)
            {
                AutoCloseDoorsPlugin.Instance.PlayerDeathService.AddPlayer(player, () =>
                {
                    shouldCancel = true;
                });
            }


            TaskDispatcher.QueueOnMainThread(() =>
            {
                if(config.CancelOnPlayerDead)
                    AutoCloseDoorsPlugin.Instance.PlayerDeathService.RemovePlayer(player);
                if (shouldCancel)
                    return;

                if (!__instance.isOpen)
                    return;

                if (config.DisplayDoorClosedMessage)
                    UnturnedChat.Say(player.channel.owner.playerID.steamID, AutoCloseDoorsPlugin.Instance.Translate("autodoor_closed"), UnturnedChat.GetColorFromName(config.MessageColor, UnityEngine.Color.red));

                BarricadeManager.ServerSetDoorOpen(__instance, false);
            }, delay);
        }
    }
}
