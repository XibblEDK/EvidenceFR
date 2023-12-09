using EvidenceFR.Functions.Object;

namespace EvidenceFR.Mod
{
    internal static class Events
    {
        internal static event Events.EvidenceCaseAddedEventHandler EvidenceCaseAdded;

        internal static event Events.EvidenceEntityAddedToCaseEventHandler EvidenceEntityAddedToCase;

        internal static void FireEvidenceEntityAddedToCase(EvidenceEntity evidenceEntity, EvidenceCase evidenceCase)
        {
            Events.EvidenceEntityAddedToCaseEventHandler evidenceEntityAddedToCase = Events.EvidenceEntityAddedToCase;
            if (evidenceEntityAddedToCase == null)
                return;
            evidenceEntityAddedToCase(evidenceEntity, evidenceCase);
        }

        internal static void FireEvidenceCaseAdded(EvidenceCase evidenceCase)
        {
            Events.EvidenceCaseAddedEventHandler evidenceCaseAdded = Events.EvidenceCaseAdded;
            if (evidenceCaseAdded == null)
                return;
            evidenceCaseAdded(evidenceCase);
        }

        internal delegate void EvidenceEntityAddedToCaseEventHandler(EvidenceEntity evidenceEntity, EvidenceCase evidenceCase);

        internal delegate void EvidenceCaseAddedEventHandler(EvidenceCase evidenceCase);
    }
}
