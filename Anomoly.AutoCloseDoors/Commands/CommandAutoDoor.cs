using Anomoly.AutoCloseDoors.Models;
using Rocket.API;
using Rocket.Unturned.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anomoly.AutoCloseDoors.Commands
{
    public class CommandAutoDoor : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "autodoor";

        public string Help => "Toggle the auto door feature.";

        public string Syntax => "";

        public List<string> Aliases => new List<string>() { "ad" };

        public List<string> Permissions => new List<string>() { "autodoor" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var playerConfigServ = AutoCloseDoorsPlugin.Instance.PlayerConfigService;

            var player = playerConfigServ.GetPlayer(caller.Id);
            playerConfigServ.ToggleEnabled(caller.Id);

            var toggledMsg = player.Enabled ? "Enabled" : "Disabled";

            UnturnedChat.Say(caller, AutoCloseDoorsPlugin.Instance.Translate("command_autodoor", toggledMsg), UnturnedChat.GetColorFromName(AutoCloseDoorsPlugin.Instance.Configuration.Instance.MessageColor, UnityEngine.Color.red));

        }
    }
}
