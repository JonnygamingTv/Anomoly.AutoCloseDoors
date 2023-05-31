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
            public Player Player { get; set; }
            public System.Action DeathCallback { get; set; }

        }

        private List<DoorPlayer> _doorPlayers;

        public PlayerDeathService() {

            _doorPlayers = new List<DoorPlayer>();
            UnturnedPlayerEvents.OnPlayerDeath += UnturnedPlayerEvents_OnPlayerDeath;
            Logger.Log("Player Death service has loaded!");
        }

        private void UnturnedPlayerEvents_OnPlayerDeath(Rocket.Unturned.Player.UnturnedPlayer player, SDG.Unturned.EDeathCause cause, SDG.Unturned.ELimb limb, Steamworks.CSteamID murderer)
        {
            if (AutoCloseDoorsPlugin.Instance.Configuration.Instance.CancelOnPlayerDead)
            {
                var doorPlayer = _doorPlayers.FirstOrDefault(x => x.Player == player.Player);

                if(doorPlayer == null)
                {
                    RemovePlayer(doorPlayer.Player);
                }

                doorPlayer.DeathCallback.Invoke();
                RemovePlayer(doorPlayer.Player);
            }
        }

        public void AddPlayer(Player player, System.Action callback)
        {
            _doorPlayers.Add(new DoorPlayer()
            {
                Player = player,
                DeathCallback = callback,
            });
        }

        public void RemovePlayer(Player player)
        {
            _doorPlayers.RemoveAll(x => x.Player == player);
        }

        public void Unload()
        {
            UnturnedPlayerEvents.OnPlayerDeath -= UnturnedPlayerEvents_OnPlayerDeath;
        }
    }
}
