using DamageTrackerLib.DamageInfo;
using EvidenceFR.BaseFunction.EvidenceObject;
using EvidenceFR.Mod;
using EvidenceFR.Utils;
using Rage;
using Rage.Native;
using System.Collections.Generic;
using System.Linq;

namespace EvidenceFR.Functions.Object
{
    internal class EvidenceManager
    {
        private static List<EvidenceCase> evidenceCases = new List<EvidenceCase>();
        public static List<EvidenceEntity> evidenceEntityPool = new List<EvidenceEntity>();
        private static int TickDelay = 1;
        public static bool isAnyEntityBeingPreviewed = false;

        private static bool isHitUIEnabled = false;
        private static bool isAttributeUIEnabled = false;

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

        public static void EnableAttributeUI(EvidenceAttributeType evidenceAttributeType)
        {
            isAttributeUIEnabled = !isAttributeUIEnabled;
            if (!isAttributeUIEnabled)
            {
                switch (evidenceAttributeType)
                {
                    case EvidenceAttributeType.FIBBadge:
                        TextureRendererManager.RenderedTextures.Add(TextureRendererManager.fibbadge);
                        break;
                }
            }
            else
            {
                switch (evidenceAttributeType)
                {
                    case EvidenceAttributeType.FIBBadge:
                        TextureRendererManager.RenderedTextures.Remove(TextureRendererManager.fibbadge);
                        break;
                }
            }
            
        }

        private static void EnableHitUI()
        {
            isHitUIEnabled = true;
            //TextureRendererManager.RenderedTextures.Add(TextureRendererManager.headen);
            TextureRendererManager.RenderedTextures.Add(TextureRendererManager.headdis);

            //TextureRendererManager.RenderedTextures.Add(TextureRendererManager.bodyen);
            TextureRendererManager.RenderedTextures.Add(TextureRendererManager.bodydis);

            //TextureRendererManager.RenderedTextures.Add(TextureRendererManager.leftarmen);
            TextureRendererManager.RenderedTextures.Add(TextureRendererManager.leftarmdis);

            //TextureRendererManager.RenderedTextures.Add(TextureRendererManager.rightarmen);
            TextureRendererManager.RenderedTextures.Add(TextureRendererManager.rightarmdis);

            //TextureRendererManager.RenderedTextures.Add(TextureRendererManager.leftlegen);
            TextureRendererManager.RenderedTextures.Add(TextureRendererManager.leftlegdis);

            //TextureRendererManager.RenderedTextures.Add(TextureRendererManager.rightlegen);
            TextureRendererManager.RenderedTextures.Add(TextureRendererManager.rightlegdis);
        }

        private static void DisableHitUI()
        {
            isHitUIEnabled = false;
            TextureRendererManager.RenderedTextures.Remove(TextureRendererManager.headen);
            TextureRendererManager.RenderedTextures.Remove(TextureRendererManager.headdis);

            TextureRendererManager.RenderedTextures.Remove(TextureRendererManager.bodyen);
            TextureRendererManager.RenderedTextures.Remove(TextureRendererManager.bodydis);

            TextureRendererManager.RenderedTextures.Remove(TextureRendererManager.leftarmen);
            TextureRendererManager.RenderedTextures.Remove(TextureRendererManager.leftarmdis);

            TextureRendererManager.RenderedTextures.Remove(TextureRendererManager.rightarmen);
            TextureRendererManager.RenderedTextures.Remove(TextureRendererManager.rightarmdis);

            TextureRendererManager.RenderedTextures.Remove(TextureRendererManager.leftlegen);
            TextureRendererManager.RenderedTextures.Remove(TextureRendererManager.leftlegdis);

            TextureRendererManager.RenderedTextures.Remove(TextureRendererManager.rightlegen);
            TextureRendererManager.RenderedTextures.Remove(TextureRendererManager.rightlegdis);
        }

        private static void ToggleHitBoneUI(Limb limb)
        {
            switch(limb)
            {
                case Limb.Head:
                    TextureRendererManager.RenderedTextures.Remove(TextureRendererManager.headdis);
                    TextureRendererManager.RenderedTextures.Add(TextureRendererManager.headen);
                    break;
                case Limb.Chest:
                    TextureRendererManager.RenderedTextures.Remove(TextureRendererManager.bodydis);
                    TextureRendererManager.RenderedTextures.Add(TextureRendererManager.bodyen);
                    break;
                case Limb.LeftArm:
                    TextureRendererManager.RenderedTextures.Remove(TextureRendererManager.leftarmdis);
                    TextureRendererManager.RenderedTextures.Add(TextureRendererManager.leftarmen);
                    break;
                case Limb.RightArm:
                    TextureRendererManager.RenderedTextures.Remove(TextureRendererManager.rightarmdis);
                    TextureRendererManager.RenderedTextures.Add(TextureRendererManager.rightarmen);
                    break;
                case Limb.LeftLeg:
                    TextureRendererManager.RenderedTextures.Remove(TextureRendererManager.leftlegdis);
                    TextureRendererManager.RenderedTextures.Add(TextureRendererManager.leftlegen);
                    break;
                case Limb.RightLeg:
                    TextureRendererManager.RenderedTextures.Remove(TextureRendererManager.rightlegdis);
                    TextureRendererManager.RenderedTextures.Add(TextureRendererManager.rightlegen);
                    break;
            }
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
            if (IsCheckingForClues)
            {
                Logging.Log(Logging.LogLevel.Warning, $"WARNING: You tried to run CheckClues two times. This is impossible! Method: " + new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name);
                return;
            }
            IsCheckingForClues = true;

            Logging.Log(Logging.LogLevel.Debug, $"Checking clues Fiber starting..");
            GameFiber.StartNew(delegate
            {
                while (true)
                {
                    if (Game.IsKeyDown(System.Windows.Forms.Keys.F7))
                    {

                        EvidenceCase testCase = new EvidenceCase();
                        Model model = new Model("xm_prop_x17_corpse_01");
                        model.LoadAndWait();
                        for (int i = 0; i < 10; i++)
                        {
                            if (i == 0)
                            {
                                Rage.Object obj = new Rage.Object("xm_prop_x17_corpse_01", Game.LocalPlayer.Character.Position.Around2D(5, 14));
                                NativeFunction.Natives.PLACE_OBJECT_ON_GROUND_OR_OBJECT_PROPERLY(obj);
                                EvidenceEntity evidenceEntity = new EvidenceEntity(obj, testCase, "Corpse-" + i, new Utils.EvidenceMarker(), new EvidenceAttribute(new FIBBadge("Adam", "Nielsen", 1)));
                                evidenceEntity.DiscoverRange = 1.5f;
                            }
                            else
                            {
                                Rage.Object obj = new Rage.Object("xm_prop_x17_corpse_01", Game.LocalPlayer.Character.Position.Around2D(5, 14));
                                NativeFunction.Natives.PLACE_OBJECT_ON_GROUND_OR_OBJECT_PROPERLY(obj);
                                EvidenceEntity evidenceEntity = new EvidenceEntity(obj, testCase, "Corpse-" + i, new Utils.EvidenceMarker());
                                evidenceEntity.DiscoverRange = 1.5f;
                            }
                        }
                        GameFiber.Wait(100);
                    }

                    GameFiber.Wait(5);

                    Ped closestPed = World.GetAllPeds().Where(p => p & p.IsDead & p.DistanceTo(Game.LocalPlayer.Character) < 3 & EvidencePedManager.IsDamageEventOfPedestrianCollected(p)).FirstOrDefault();
                    if (closestPed)
                    {
                        if (Game.IsKeyDown(System.Windows.Forms.Keys.F11))
                        {
                            if(!isHitUIEnabled)
                            {
                                EnableHitUI();
                                Game.DisplayNotification("Times hit: (And enabling ui) " + EvidencePedManager.GetDamageEventInfos(closestPed).Count());
                                
                                EvidencePedManager.GetDamageEventInfos(closestPed).ForEach(eI =>
                                {
                                    ToggleHitBoneUI(eI.pedDamageInfo.BoneInfo.Limb);
                                });
                            } else
                            {
                                DisableHitUI();
                            }
                            
                        }
                        Game.DisplaySubtitle("Distance To nearby ped: " + closestPed.DistanceTo(Game.LocalPlayer.Character));
                    }

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
                                if (Game.LocalPlayer.Character.DistanceTo(currentEvidenceEntity.Entity.Position) < currentEvidenceEntity.DiscoverRange && !currentEvidenceEntity.Found)
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
            }, "CheckClues-Fiber (EvidenceManager.cs)");

        }
    }
}
