using EvidenceFR.Utils;
using Rage;
using System.Collections.Generic;

namespace EvidenceFR.BaseFunction.EvidenceObject
{
    internal class EvidencePedManager
    {
        private static Dictionary<Ped, List<DamageEventInfo>> damagedPeds = new Dictionary<Ped, List<DamageEventInfo>>();

        private static bool initialized = false;

        public static void Initialize()
        {
            if (initialized)
            {
                Logging.Log(Logging.LogLevel.Warning, "Evidence Ped Manager is already initialized.");
            }
            else
            {
                initialized = true;
                GarbageCollector();
            }
        }

        public static List<DamageEventInfo> GetDamageEventInfos(Ped ped)
        {
            if (!IsDamageEventOfPedestrianCollected(ped)) return null;
            return damagedPeds[ped];
        }

        public static bool IsDamageEventOfPedestrianCollected(Ped ped)
        {
            if (damagedPeds.ContainsKey(ped)) return true;
            return false;
        }
        public static void AddDamageEvent(DamageEventInfo damageEvent)
        {
            if (!damageEvent.victimPed) return;

            if (damagedPeds.ContainsKey(damageEvent.victimPed))
            {
                Logging.Log(Logging.LogLevel.Debug, "Ped was already damaged before");
                Logging.Log(Logging.LogLevel.Debug, "Getting list of past damage events");
                List<DamageEventInfo> info = damagedPeds[damageEvent.victimPed];
                Logging.Log(Logging.LogLevel.Debug, "Got the list");
                Logging.Log(Logging.LogLevel.Debug, "Adding event to the list");
                info.Add(damageEvent);
                Logging.Log(Logging.LogLevel.Debug, "Event successfully added to list");
            }
            else
            {
                Logging.Log(Logging.LogLevel.Debug, "Ped was never damaged before");
                List<DamageEventInfo> damageInfo = new List<DamageEventInfo>();
                Logging.Log(Logging.LogLevel.Debug, "Adding damage event to new list");
                damageInfo.Add(damageEvent);
                Logging.Log(Logging.LogLevel.Debug, "Added");
                Logging.Log(Logging.LogLevel.Debug, "Adding entry to the directory");
                damagedPeds.Add(damageEvent.victimPed, damageInfo);
                Logging.Log(Logging.LogLevel.Debug, "Entry successfully added to the directory");
            }

        }


        private static void GarbageCollector()
        {
            GameFiber.StartNew(delegate
            {
                while (Main.IsOnDuty)
                {
                    GameFiber.Wait(15000);
                    Logging.Log(Logging.LogLevel.Debug, "Collecting invalid Peds");
                    int deletedPeds = 0;
                    foreach (Ped ped in damagedPeds.Keys)
                    {
                        if (!ped)
                        {
                            damagedPeds.Remove(ped);
                            deletedPeds++;
                        }
                    }
                    Logging.Log(Logging.LogLevel.Debug, deletedPeds + " damage informations of invalid peds were removed");
                }
            }, "Damage Event Garbage Collection");
        }

    }
}
