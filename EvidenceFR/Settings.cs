using Rage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvidenceFR
{
    internal sealed class Settings
    {
        // Assign variables as needed
        public Keys MenuKey = Keys.NumPad3;

        private static InitializationFile config;
        private static Settings instance = (Settings)null;

        private Settings()
        {
        }

        public static Settings Instance
        {
            get
            {
                if (Settings.instance == null)
                    Settings.instance = new Settings();
                return Settings.instance;
            }
        }

        // This should be called when the plugin is initialized. Also DO NOT use this class' variables before this function is used. Just to avoid any exceptions.
        public void Load()
        {
            config = new InitializationFile("Plugins/LSPDFR/EvidenceFR.ini");
            instance.DoDirtyWork();
        }

        // This function can write new values for the selected variable in the ini. This could be used for a settings menu in-game.
        public void Write(string sectionName, string name, string newValue)
        {
            config.Write(sectionName, name, newValue);
        }

        // This function does the dirty work. Just reading the values and setting it for the variables.
        private void DoDirtyWork()
        {
            if (config != null)
            {
                MenuKey = config.ReadEnum("[Keybinds]", "MenuKey", Keys.F7);
            }
        }
    }
}
