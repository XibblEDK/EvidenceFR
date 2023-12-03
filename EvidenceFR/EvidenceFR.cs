using EvidenceFR.Callouts;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidenceFR
{
    internal sealed class EvidenceFR
    {
        private static EvidenceFR instance;
        
        public static EvidenceFR Instance
        {
            get
            {
                if (EvidenceFR.instance == null) 
                    EvidenceFR.instance = new EvidenceFR();
                return EvidenceFR.instance;
            }
        }

        public void Start()
        {
            // Start processes and load .INI file.
            Settings.Instance.Load();
            this.RegisterCallouts();
            Game.DisplayNotification("Succesfully loaded EvidenceFR");
            this.SubscribeToEvents();
        }

        public void Stop()
        {
            // Stop processes.
            this.UnsubscribeToEvents();
        }

        private EvidenceFR()
        {
            // Create variables. Like this.menuHandler = new MenuHandler(); Or whatever.
        }

        private void SubscribeToEvents()
        {
            // Subscribe to events as needed.
        }

        private void UnsubscribeToEvents()
        {
            // Unsubscribe to events as needed.
        }

        // Rest of class is used for private important functions and functions for subscribed events.

        private void RegisterCallouts()
        {
            LSPD_First_Response.Mod.API.Functions.RegisterCallout(typeof(CSIDrugDealUnderControl));
            LSPD_First_Response.Mod.API.Functions.RegisterCallout(typeof(CSIFIBAgentMurdered));
            LSPD_First_Response.Mod.API.Functions.RegisterCallout(typeof(CSIMurder));
            LSPD_First_Response.Mod.API.Functions.RegisterCallout(typeof(CSIOldCaseResurrection));
        }
    }
}
