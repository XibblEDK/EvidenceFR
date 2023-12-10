using EvidenceFR.Utils;
using Rage;
using Rage.Native;
using System.Collections.Generic;
using System.Linq;
using static EvidenceFR.Utils.VehicleDamageInfo;

namespace EvidenceFR.BaseFunction.EvidenceObject
{
    internal class EvidenceVehicleManager
    {

        private static Dictionary<Vehicle, List<VehicleDamageInfo>> damagedVehicles = new Dictionary<Vehicle, List<VehicleDamageInfo>>();

        private static bool initialized = false;

        public static void Initialize()
        {
            if (initialized)
            {
                Logging.Log(Logging.LogLevel.Warning, "Evidence Vehicle Manager is already initialized.");
            }
            else
            {
                CheckVehicleCollisions();
                initialized = true;
            }
        }

        private static bool checkForCollisions = true;

        private static void CheckVehicleCollisions()
        {
            Logging.Log(Logging.LogLevel.Warning, "Starting CheckVehicleCollision Fiber");
            checkForCollisions = true;
            GameFiber.StartNew(delegate
            {
                while (checkForCollisions)
                {
                    GameFiber.Yield();
                    if (Game.IsKeyDown(System.Windows.Forms.Keys.P))
                    {
                        GiveCarDamageReportOfClosestVehicle();
                    }


                    var vehicles = Rage.World.GetAllVehicles().Where(v => v & v.DistanceTo(Game.LocalPlayer.Character) < 150);
                    foreach (Vehicle curVeh in vehicles)
                    {
                        Vector3 lastHitVec = Vector3.Zero;
                        Material lastHitMaterial = Material.None;

                        lastHitMaterial = (Material)NativeFunction.Natives.GET_LAST_MATERIAL_HIT_BY_ENTITY<uint>(curVeh);
                        lastHitVec = NativeFunction.Natives.GET_COLLISION_NORMAL_OF_LAST_HIT_FOR_ENTITY<Vector3>(curVeh);
                        if (lastHitVec != Vector3.Zero & lastHitMaterial != Material.None)
                        {
                            AddDamageEvent(new VehicleDamageInfo(curVeh, lastHitMaterial, lastHitVec));
                        }
                    }
                }
            }, "EvidenceVehicleManager-Fiber");
        }

        public static List<VehicleDamageInfo> GetDamageEventInfos(Vehicle veh)
        {
            if (!IsDamageEventOfVehicleCollected(veh)) return null;
            return damagedVehicles[veh];
        }

        public static bool IsDamageEventOfVehicleCollected(Vehicle veh)
        {
            if (damagedVehicles.ContainsKey(veh)) return true;
            return false;
        }

        /// <summary>
        /// Gets the closest vehicle in the range of 5 and prints all damages and rückstände
        /// </summary>
        public static void GiveCarDamageReportOfClosestVehicle()
        {
            var closestVehicle = Rage.World.GetAllVehicles().Where(v => v & IsDamageEventOfVehicleCollected(v) & v.DistanceTo(Game.LocalPlayer.Character) < 5).FirstOrDefault();
            if (closestVehicle != null)
            {
                string frontDamages = "Front side Damages:~n~";
                string backDamages = "Back side Damages:~n~";
                string leftDamages = "Left side Damages:~n~";
                string rightDamages = "Right side Damages:~n~";
                string topDamages = "Top side Damages:~n~";
                string belowDamages = "Bottom side Damages:~n~";

                List<VehicleDamageInfo> vehicleDamageInfos = GetDamageEventInfos(closestVehicle);
                if (vehicleDamageInfos != null)
                {
                    foreach (var curVehicleDamageInfo in vehicleDamageInfos.ToList())
                    {
                        Logging.Log(Logging.LogLevel.Debug, "Recorded vehicle collision found, damaged at: " + curVehicleDamageInfo.relativeLocation + ". Collided with: " + curVehicleDamageInfo.material);
                        if (VehicleDamageInfo.GetAccidentNameForMaterial(curVehicleDamageInfo.material).Contains("None")) continue;
                        switch (curVehicleDamageInfo.relativeLocation)
                        {
                            case RelativeLocation.Front:
                                if (!frontDamages.Contains(VehicleDamageInfo.GetAccidentNameForMaterial(curVehicleDamageInfo.material)))
                                    frontDamages += "~r~" + VehicleDamageInfo.GetAccidentNameForMaterial(curVehicleDamageInfo.material) + "~n~~w~";
                                break;
                            case RelativeLocation.Back:
                                if (!backDamages.Contains(VehicleDamageInfo.GetAccidentNameForMaterial(curVehicleDamageInfo.material)))
                                    backDamages += "~r~" + VehicleDamageInfo.GetAccidentNameForMaterial(curVehicleDamageInfo.material) + "~n~~w~";
                                break;
                            case RelativeLocation.Left:
                                if (!leftDamages.Contains(VehicleDamageInfo.GetAccidentNameForMaterial(curVehicleDamageInfo.material)))
                                    leftDamages += "~r~" + VehicleDamageInfo.GetAccidentNameForMaterial(curVehicleDamageInfo.material) + "~n~~w~";
                                break;
                            case RelativeLocation.Right:
                                if (!rightDamages.Contains(VehicleDamageInfo.GetAccidentNameForMaterial(curVehicleDamageInfo.material)))
                                    rightDamages += "~r~" + VehicleDamageInfo.GetAccidentNameForMaterial(curVehicleDamageInfo.material) + "~n~~w~";
                                break;
                            case RelativeLocation.Top:
                                if (!topDamages.Contains(VehicleDamageInfo.GetAccidentNameForMaterial(curVehicleDamageInfo.material)))
                                    topDamages += "~r~" + VehicleDamageInfo.GetAccidentNameForMaterial(curVehicleDamageInfo.material) + "~n~~w~";
                                break;
                            case RelativeLocation.Below:
                                if (!belowDamages.Contains(VehicleDamageInfo.GetAccidentNameForMaterial(curVehicleDamageInfo.material)))
                                    belowDamages += "~r~" + VehicleDamageInfo.GetAccidentNameForMaterial(curVehicleDamageInfo.material) + "~n~~w~";
                                break;
                        }
                    }
                    if (!frontDamages.Contains("~r~")) frontDamages = "";
                    if (!backDamages.Contains("~r~")) backDamages = "";
                    if (!leftDamages.Contains("~r~")) leftDamages = "";
                    if (!topDamages.Contains("~r~")) topDamages = "";
                    if (!belowDamages.Contains("~r~")) belowDamages = "";
                    Game.DisplayNotification(frontDamages + backDamages + leftDamages + rightDamages + topDamages + belowDamages);
                }
            }
        }

        public static void AddDamageEvent(VehicleDamageInfo damageEvent)
        {
            if (!damageEvent.vehicle) return;

            if (damagedVehicles.ContainsKey(damageEvent.vehicle))
            {
                //Logging.Log(Logging.LogLevel.Debug, "Vehicle was already damaged before");
                //Logging.Log(Logging.LogLevel.Debug, "Getting list of past damage events");
                List<VehicleDamageInfo> info = damagedVehicles[damageEvent.vehicle];
                //logging.log(logging.loglevel.debug, "got the list");
                //Logging.Log(Logging.LogLevel.Debug, "Adding event to the list");
                info.Add(damageEvent);
                //Logging.Log(Logging.LogLevel.Debug, "Event successfully added to list");
            }
            else
            {
                //Logging.Log(Logging.LogLevel.Debug, "Vehicle was never damaged before");
                List<VehicleDamageInfo> damageInfo = new List<VehicleDamageInfo>();
                //Logging.Log(Logging.LogLevel.Debug, "Adding damage event to new list");
                damageInfo.Add(damageEvent);
                //Logging.Log(Logging.LogLevel.Debug, "Added");
                //Logging.Log(Logging.LogLevel.Debug, "Adding entry to the directory");
                damagedVehicles.Add(damageEvent.vehicle, damageInfo);
                //Logging.Log(Logging.LogLevel.Debug, "Entry successfully added to the directory");
            }

        }
    }
}
