using Anomoly.AutoCloseDoors.Models;
using Anomoly.AutoCloseDoors.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anomoly.AutoCloseDoors.Service
{
    public class PlayerConfigService
    {
        public JsonStorage<List<AutoDoorPlayer>> PlayerData { get; private set; }

        public PlayerConfigService()
        {
            PlayerData = new JsonStorage<List<AutoDoorPlayer>>(Path.Combine(AutoCloseDoorsPlugin.Instance.Directory, "AutoDoors_Players.json"), new List<AutoDoorPlayer>());
        }

        public AutoDoorPlayer GetPlayer(string playerId)
        {
            var player = PlayerData.Data.FirstOrDefault(p => p.SteamId == playerId);

            if(player == null)
            {
                player = new AutoDoorPlayer()
                {
                    SteamId = playerId,
                    Enabled = AutoCloseDoorsPlugin.Instance.Configuration.Instance.DefaultEnabled,
                };
                PlayerData.Data.Add(player);
                PlayerData.Save();
            }

            return player;
        }

        public void ToggleEnabled(string playerId)
        {
            var player = GetPlayer(playerId);

            player.Enabled = !player.Enabled;
            PlayerData.Save();
        }
    }
}
