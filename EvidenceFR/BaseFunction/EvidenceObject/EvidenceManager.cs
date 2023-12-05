using System;
using Rage;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvidenceFR.Mod.Props;
using Rage.Native;
using EvidenceFR.Utils;
using EvidenceFR.Mod;
using System.Security.Policy;

namespace EvidenceFR.Functions.Object
{
    internal class EvidenceManager
    {
        private static List<EvidenceCase> evidenceCases = new List<EvidenceCase>();
        public static List<EvidenceEntity> evidenceEntityPool = new List<EvidenceEntity>();
        private static int TickDelay = 1;
        public static bool isAnyEntityBeingPreviewed = false;

        public static EvidenceEntity GetEvidenceEntityById(int id)
        {
            if (evidenceEntityPool.Count == 0)
                return null;
            return evidenceEntityPool.ElementAt(id);
        }

        public static void RegisterEntity(EvidenceEntity entity)
        {
            Logging.Log(Logging.LogLevel.Debug, $"Adding evidence entity to Pool (C:{entity.parentCase.caseId}.{entity.EvidenceName})");
            evidenceEntityPool.Add(entity);
        } 

        public static void AddCase(EvidenceCase evidenceCase)
        {
            Logging.Log(Logging.LogLevel.Debug, $"Adding case to Management ({evidenceCase.caseId})");
            evidenceCases.Add(evidenceCase);
            Events.FireEvidenceCaseAdded(evidenceCase);
            Logging.Log(Logging.LogLevel.Debug, $"Case added to Management and Menu. Total Cases:{evidenceCases.Count} Created Case ID: ({evidenceCase.caseId})");
        }

        public static int GetCaseId()
        {
            Logging.Log(Logging.LogLevel.Debug, $"Getting case ID");
            return evidenceCases.Count;
        }

        public static EvidenceCase? GetCaseById(int id)
        {
            Logging.Log(Logging.LogLevel.Debug, $"Getting case by ID");
            foreach (EvidenceCase evidenceCase in evidenceCases) if (evidenceCase.caseId == id) return evidenceCase;
            return null;
        }

        private static bool IsCheckingForClues = false;

        public static void CheckClues()
        {
            if(IsCheckingForClues)
            {
                Logging.Log(Logging.LogLevel.Warning, $"WARNING: You tried to run CheckClues two times. This is impossible! Method: " + new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name);
                return;
            }
            IsCheckingForClues = true;

            Logging.Log(Logging.LogLevel.Debug, $"Checking clues Fiber starting..");
            GameFiber.StartNew(delegate
            {
                while(true)
                {
                    if(Game.IsKeyDown(System.Windows.Forms.Keys.F7))
                    {

                        EvidenceCase testCase = new EvidenceCase();
                        Model model = new Model("xm_prop_x17_corpse_01");
                        model.LoadAndWait();
                        for (int i = 0; i < 10; i++)
                        {
                            
                            Rage.Object obj = new Rage.Object("xm_prop_x17_corpse_01", Game.LocalPlayer.Character.Position.Around2D(5,14));
                            NativeFunction.Natives.PLACE_OBJECT_ON_GROUND_OR_OBJECT_PROPERLY(obj);
                            EvidenceEntity evidenceEntity = new EvidenceEntity(obj, testCase, "Corpse-"+i, new Utils.EvidenceMarker());
                            evidenceEntity.DiscoverRange = 1.5f;
                        }
                        GameFiber.Wait(100);
                    }

                    GameFiber.Yield();
                    
                    foreach (EvidenceCase evidenceCase in evidenceCases)
                    {
                        Logging.Log(Logging.LogLevel.Debug, "Iterating Cases. Entity Count: " + evidenceCase.evidenceEntities.Count + " CaseID: " + evidenceCase.caseId);
                        //if (!evidenceCase.CanCollectClues) continue;
                        foreach (EvidenceEntity currentEvidenceEntity in evidenceCase.evidenceEntities)
                        {
                            Logging.Log(Logging.LogLevel.Debug, "Iterating Entities");
                            if (currentEvidenceEntity.Entity)
                            {
                                Logging.Log(Logging.LogLevel.Debug, "Entity valid");
                                // TODO: Evidence Collection By NPC Cops
                                if (Game.LocalPlayer.Character.DistanceTo(currentEvidenceEntity.Entity.Position) < currentEvidenceEntity.DiscoverRange)
                                {
                                    Logging.Log(Logging.LogLevel.Debug, "Evidence Found");
                                    // Discover
                                    Game.DisplayNotification($"You discovered Clue #[{evidenceCase.caseId}]- {currentEvidenceEntity.EvidenceName}");
                                    currentEvidenceEntity.Found = true;
                                }
                            }
                        }
                    }
                }
            },"CheckClues-Fiber (EvidenceManager.cs)");
            
        }
    }
}
