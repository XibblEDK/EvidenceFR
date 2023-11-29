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
        private Entity entity;
        private EvidenceEntity evidence;
        private int evidenceID;
        
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

        /// <summary>
        /// Use this for creating an evidence piece from an existing entity in the game world
        /// WARNING: Returns null if the passed entity is invalid
        /// </summary>
        /// <param name="ent">An existing Rage Entity</param>
        /// <returns></returns>
        public EvidenceEntity(Entity ent)
        {
            if (!ent)
            {
                // Entity invalid
                Logging.Log(Logging.LogLevel.Error, "The passed entity was invalid. Evidence was not created!");
                return;
            }

            evidence = new EvidenceEntity(ent);

        }

    }
}
