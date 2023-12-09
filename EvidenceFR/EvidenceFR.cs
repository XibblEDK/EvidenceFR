using DamageTrackerLib;
using EvidenceFR.Callouts;
using EvidenceFR.Functions.Object;
using EvidenceFR.Mod;
using EvidenceFR.Utils;
using Rage;
using RAGENativeUI;
using System;

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
            Logging.Log(Logging.LogLevel.Debug, "Starting DamageTrackerService");
            DamageTrackerService.Start();
            Logging.Log(Logging.LogLevel.Debug, "DamageTrackerService started successfully");
            menuPool = new MenuPool();
            TextureRendererManager.InitializeTextures();
            GameFiber.StartNew(EvidenceFRMainMenu.SetupMenu);
            GameFiber.StartNew(StartMenuDraw);
            // Start the Evidence Managers clue collecting fiber
            EvidenceManager.CheckClues();
            // Start processes and load .INI file.
            Settings.Instance.Load();
            //this.RegisterCallouts();
            Game.DisplayNotification("Succesfully loaded EvidenceFR");
            this.SubscribeToEvents();
        }

        public void Stop()
        {
            Logging.Log(Logging.LogLevel.Debug, "Starting DamageTrackerService");
            DamageTrackerService.Stop();
            Logging.Log(Logging.LogLevel.Debug, "Stopping DamageTrackerService");

            TextureRendererManager.UnsubscribeRenderer();

            foreach (EvidenceEntity ent in EvidenceManager.evidenceEntityPool)
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
            Logging.Log(Logging.LogLevel.Debug, "Subscribing to events..");
            Logging.Log(Logging.LogLevel.Debug, "Subscribing DamageTracker Event");
            DamageTrackerService.OnPedTookDamage += DamageTracker.HandleDamage;
            Logging.Log(Logging.LogLevel.Debug, "DamageTracker Event subscribed successfully");
            // Subscribe to events as needed.


            // End of subscription
            Logging.Log(Logging.LogLevel.Debug, "Subscribing to events was successful");
        }

        private void UnsubscribeToEvents()
        {
            Logging.Log(Logging.LogLevel.Debug, "Unsubscribing to events..");
            Logging.Log(Logging.LogLevel.Debug, "Unsubscribing DamageTracker Event");
            DamageTrackerService.OnPedTookDamage -= DamageTracker.HandleDamage;
            Logging.Log(Logging.LogLevel.Debug, "DamageTracker Event unsubscribed successfully");
            // Unsubscribe to events as needed.



            // End of unsubscription
            Logging.Log(Logging.LogLevel.Debug, "Unsubscribing to events was successful");
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
            try
            {
                while (Main.IsOnDuty)
                {
                    GameFiber.Yield();

                    menuPool.ProcessMenus();

                    if (Game.IsKeyDown(System.Windows.Forms.Keys.F6))
                    {

                        if (!EvidenceManager.isAnyEntityBeingPreviewed)
                        {
                            menuPool.CloseAllMenus();
                            EvidenceFRMainMenu.Menu.Visible = true;
                        }
                        else
                        {
                            Game.DisplaySubtitle("~r~Please exit the evidence preview before opening the menu.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log(Logging.LogLevel.Error, ex.Message);
                Logging.Log(Logging.LogLevel.Error, ex.StackTrace);
            }
        }
    }
}
