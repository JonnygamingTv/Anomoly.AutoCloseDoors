using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anomoly.AutoCloseDoors
{
    public class AutoCloseDoorsPluginConfiguration : IRocketPluginConfiguration
    {
        // Allow players to auto close doors they do not open (wooden doors)
        public bool AllowAutoCloseUnclaimedDoors { get; set; }

        //If a player dies, should the door still auto close
        public bool CancelOnPlayerDead { get; set; }

        //The amount of seconds before the door closes.
        public int CloseDelay { get; set; }

        //If no player exists, this will be their default enabled
        public bool DefaultEnabled { get; set; }

        //Displays message after door has been closed.
        public bool DisplayDoorClosedMessage { get; set; }

        public string MessageColor { get; set; }
        public void LoadDefaults()
        {
            AllowAutoCloseUnclaimedDoors = false;
            CancelOnPlayerDead = false;
            CloseDelay = 5;
            DefaultEnabled = false;
            DisplayDoorClosedMessage = true;
            MessageColor = "red";
        }
    }
}
