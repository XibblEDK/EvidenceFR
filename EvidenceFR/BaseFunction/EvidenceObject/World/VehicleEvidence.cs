using Rage;
using Rage.Native;

namespace EvidenceFR.BaseFunction.EvidenceObject.World
{
    internal class VehicleEvidence
    {

        /// <summary>
        /// Returns the last passenger on the specified seat in the specified vehicle
        /// Returns Game.LocalPlayer.Character if the vehicle, the seat or the returned ped is invalid
        /// </summary>
        /// <param name="veh">The vehicle where the ped sat in</param>
        /// <param name="seatIndex">The seat index, reaching from -1 to the amount of seats in the vehicle -1</param>
        /// <returns></returns>
        public static Ped GetLastPassengerAtSeatIndex(Vehicle veh, int seatIndex)
        {
            if (!veh) return Game.LocalPlayer.Character;
            int seats = NativeFunction.Natives.GET_VEHICLE_MAX_NUMBER_OF_PASSENGERS<int>();
            if (seatIndex > seats) return Game.LocalPlayer.Character;
            Entity ent = NativeFunction.Natives.GET_LAST_PED_IN_VEHICLE_SEAT<Entity>(veh, seatIndex);
            if (!ent) return Game.LocalPlayer.Character;
            return (Ped)ent;
        }

        public static uint GetLastMaterialHitByVehicle(Vehicle veh)
        {
            uint materialHash = NativeFunction.Natives.GET_LAST_MATERIAL_HIT_BY_ENTITY<uint>(veh);
            return materialHash;
        }

    }
}
