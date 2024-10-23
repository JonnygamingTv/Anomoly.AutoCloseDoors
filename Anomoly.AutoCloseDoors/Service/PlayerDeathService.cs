using Rocket.Core.Logging;
using Rocket.Unturned.Events;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anomoly.AutoCloseDoors.Service
{
    public class PlayerDeathService
    {

        public class DoorPlayer
        {
            //public Player Player { get; set; }
            public System.Action DeathCallback { get; set; }

        }

        private Dictionary<Player, DoorPlayer> _doorPlayers;

        public PlayerDeathService() {

            _doorPlayers = new Dictionary<Player, DoorPlayer>();
            UnturnedPlayerEvents.OnPlayerDeath += UnturnedPlayerEvents_OnPlayerDeath;
            Logger.Log("Player Death service has loaded!");
        }

        private void UnturnedPlayerEvents_OnPlayerDeath(Rocket.Unturned.Player.UnturnedPlayer player, SDG.Unturned.EDeathCause cause, SDG.Unturned.ELimb limb, Steamworks.CSteamID murderer)
        {
            if (AutoCloseDoorsPlugin.Instance.Configuration.Instance.CancelOnPlayerDead && _doorPlayers.TryGetValue(player.Player, out DoorPlayer doorPlayer))
            {
                doorPlayer.DeathCallback.Invoke();
                RemovePlayer(player.Player);
            }
        }

        public void AddPlayer(Player player, System.Action callback)
        {
            _doorPlayers.Add(player, new DoorPlayer()
            {
                DeathCallback = callback,
            });
        }

        public void RemovePlayer(Player player)
        {
            _doorPlayers.Remove(player);
        }

        public void Unload()
        {
            UnturnedPlayerEvents.OnPlayerDeath -= UnturnedPlayerEvents_OnPlayerDeath;
        }
    }
}
