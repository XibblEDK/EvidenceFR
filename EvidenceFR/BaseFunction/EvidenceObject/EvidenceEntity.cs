using EvidenceFR.Utils;
using Rage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidenceFR.Functions.Object
{
    public class EvidenceEntity
    {
        public readonly Entity Entity;
        private EvidenceEntity evidence;
        private int evidenceID;

        public EvidenceCase parentCase;
        public float DiscoverRange = 2f;
        public bool CanBeDiscoveredByNPCCops = false;
        
        
        private bool canBeDiscovered = true;

        /// <summary>
        /// Set this to false if you don't want to player to discover this evidence piece.
        /// </summary>
        public bool CanBeDiscovered
        {
            get { return CanBeDiscovered; }
            set { canBeDiscovered = value; }
        }


        private bool found = false;

        /// <summary>
        /// This boolean determines if the evidence is found by the player or not
        /// </summary>
        public bool Found
        {
            get { return found; }
            set { found = value; }
        }

        private bool deleteEntityWhenFound = false;

        /// <summary>
        /// Set this to true if you want to have the entity deleted after the player found the evidence
        /// </summary>
        public bool DeleteEntityWhenFound
        {
            get { return deleteEntityWhenFound; }
            set { deleteEntityWhenFound = value; }
        }

        private int addToCaseWhenFound = 0;

        /// <summary>
        /// Adds the evidence to the case board when found, if the evidence should not be added to a particular case, leave it on 0
        /// </summary>
        public int AddToCaseWhenFound
        {
            get { return addToCaseWhenFound; }
            set { addToCaseWhenFound = value; }
        }

        private string evidenceName = "Evidence";

        /// <summary>
        /// Change the name of the evidence. The player will see this.
        /// </summary>
        public string EvidenceName
        {
            get { return evidenceName; }
            set { evidenceName = value; }
        }


        /// <summary>
        /// Use this for creating an evidence piece from an existing entity in the game world
        /// WARNING: Returns null if the passed entity is invalid
        /// </summary>
        /// <param name="ent">An existing Rage Entity</param>
        /// <returns></returns>
        public EvidenceEntity(Entity ent, EvidenceCase parentCase, string evidenceName, EvidenceMarker evidenceMarker)
        {
            if (!ent)
            {
                // Entity invalid
                Logging.Log(Logging.LogLevel.Error, "The passed entity was invalid. Evidence was not created!");
                return;
            }
            Entity = ent;
            evidence = this;
            this.parentCase = parentCase;
            this.EvidenceName = evidenceName;
            this.EvidenceMarker = evidenceMarker;
            parentCase.AddEvidenceEntity(this);
            EvidenceManager.RegisterEntity(this);
            Logging.Log(Logging.LogLevel.Debug, "Calling Fiber Function (" + EvidenceName + ")");
            StartEvidenceFiber();
            
        }

        public void DeleteEvidence()
        {
            Logging.Log(Logging.LogLevel.Debug, $"Deleting Evidence ({EvidenceName})");
            StopEvidenceFiber(); 

            if(Entity)
            {
                Logging.Log(Logging.LogLevel.Debug, $"Removing Evidence ({EvidenceName}) from Case");
                parentCase.RemoveEvidenceEntity(this);
                Logging.Log(Logging.LogLevel.Debug, $"Deleting Entity ({EvidenceName})");
                Entity.Delete();
            }
        }

        public EvidenceMarker EvidenceMarker;
        public Vector3 MarkerOffset = new Vector3(0, 0, 1);

        public bool DrawMarkerAfterFound = true;

        private bool isEvidenceFiberRunning = false;
        private void StartEvidenceFiber()
        {
            Logging.Log(Logging.LogLevel.Debug, $"Starting Evidence Fiber {EvidenceName}");
            isEvidenceFiberRunning = true;
            GameFiber.StartNew(delegate
            {
                while(isEvidenceFiberRunning)
                {
                    GameFiber.Yield();
                    if (!Entity)
                    {
                        DeleteEvidence();
                        break;
                    }

                    if(DrawMarkerAfterFound & found)
                    {
                        EvidenceMarker.Draw(Entity.Position+MarkerOffset);
                    }
                }
            }, $"EvidenceFiber (C:{parentCase.caseId}.{evidenceName})");
        }

        private void StopEvidenceFiber()
        {
            isEvidenceFiberRunning = false;
        }

    }
}
