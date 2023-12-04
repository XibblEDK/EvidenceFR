using LSPD_First_Response.Mod.API;
using Rage;
using System;
using System.IO;
using System.Reflection;

namespace EvidenceFR
{
    public class Main : Plugin
    {
        private bool isRunning;

        public override void Initialize() => LSPD_First_Response.Mod.API.Functions.OnOnDutyStateChanged += new LSPD_First_Response.Mod.API.Functions.OnDutyStateChangedEventHandler(this.Functions_OnOnDutyStateChanged);

        public override void Finally()
        {
            if (!this.isRunning)
                return;
            EvidenceFR.Instance.Stop();
            this.isRunning = false;
            
        }

        private void Functions_OnOnDutyStateChanged(bool onDuty)
        {
            if (!onDuty)
                return;
            EvidenceFR.Instance.Start();
            this.isRunning = true;
        }
    }
}