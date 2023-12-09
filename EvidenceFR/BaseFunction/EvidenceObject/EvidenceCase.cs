﻿using EvidenceFR.Mod;
using EvidenceFR.Utils;
using System.Collections.Generic;



namespace EvidenceFR.Functions.Object
{
    public class EvidenceCase
    {
        public readonly List<EvidenceEntity> evidenceEntities = new List<EvidenceEntity>();
        public readonly int caseId;

        public bool CanCollectClues = true;


        public EvidenceCase()
        {

            Logging.Log(Logging.LogLevel.Debug, "Creating new Case...");
            caseId = EvidenceManager.GetCaseId();
            Logging.Log(Logging.LogLevel.Debug, "Case ID: " + caseId);
            Logging.Log(Logging.LogLevel.Debug, "Registering Case to Manager");
            EvidenceManager.AddCase(this);
            Logging.Log(Logging.LogLevel.Debug, "Case successfully created");
        }

        public void AddEvidenceEntity(EvidenceEntity entity)
        {
            Logging.Log(Logging.LogLevel.Debug, "Adding Entity " + entity.EvidenceName + " for Case #" + caseId);
            evidenceEntities.Add(entity);
            Events.FireEvidenceEntityAddedToCase(entity, this);
            entity.parentCase = this;
        }

        public void RemoveEvidenceEntity(EvidenceEntity entity)
        {
            Logging.Log(Logging.LogLevel.Debug, "Removing Entity " + entity.EvidenceName + " from Case #" + caseId);
            evidenceEntities.Remove(entity);
            Logging.Log(Logging.LogLevel.Debug, "Entity removed");
        }

    }
}
