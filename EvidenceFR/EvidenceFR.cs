<<<<<<< HEAD
using EvidenceFR.Callouts;
=======
﻿using EvidenceFR.Callouts;
using EvidenceFR.Functions.Object;
>>>>>>> 4da6cec8166153eecde03dfe02d2682a61f25fc4
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
﻿using EvidenceFR.Functions.Object;
using Rage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAGENativeUI;
using EvidenceFR.Mod;
using EvidenceFR.Utils;

namespace EvidenceFR
{
    internal sealed class EvidenceFR
    {
        private static EvidenceFR instance;
        public MenuPool menuPool;
        
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
            menuPool = new MenuPool();
            GameFiber.StartNew(EvidenceFRMainMenu.SetupMenu);
            GameFiber.StartNew(StartMenuDraw);
            // Start the Evidence Managers clue collecting fiber
            EvidenceManager.CheckClues();
            // Start processes and load .INI file.
            Settings.Instance.Load();
            this.RegisterCallouts();
            Game.DisplayNotification("Succesfully loaded EvidenceFR");
            this.SubscribeToEvents();
        }

        public void Stop()
        {

            foreach(EvidenceEntity ent in EvidenceManager.evidenceEntityPool)
            {
                ent.DeleteEvidence();
            }

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

        private void StartMenuDraw()
        {
            while (Main.IsOnDuty)
            {
                GameFiber.Yield();

                //Logging.Log(Logging.LogLevel.Debug, "WEEEE");

                menuPool.ProcessMenus();

                if (Game.IsKeyDown(System.Windows.Forms.Keys.NumPad3))
                {
                    EvidenceFRMainMenu.Menu.Visible = true;
                }
            }
        }
    }
}
