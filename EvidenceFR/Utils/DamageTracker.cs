using DamageTrackerLib.DamageInfo;
using EvidenceFR.BaseFunction.EvidenceObject;
using Rage;

namespace EvidenceFR.Utils
{
    internal class DamageTracker
    {

        public static void HandleDamage(Ped victim, Ped attacker, PedDamageInfo damageInfo)
        {
            Logging.Log(Logging.LogLevel.Debug, "Ped was damaged!");
            DamageEventInfo damageEvent = new DamageEventInfo(victim, attacker, damageInfo);
            EvidencePedManager.AddDamageEvent(damageEvent);

            Game.DisplayNotification($"~w~Ped: {victim.Model.Name} (~r~{damageInfo.Damage} ~b~{damageInfo.ArmourDamage} ~w~Dmg ({(victim.IsAlive ? "~g~Alive" : "~r~Dead")}~w~) " +
             $"\n~w~Health: ~g~{victim.Health}/{victim.MaxHealth} Armor: ~b~{victim.Armor})" +
             $"\n~w~Attacker: ~r~{attacker?.Model.Name ?? "None"}" +
             $"\n~w~Weapon: ~y~{damageInfo.WeaponInfo.Hash.ToString()} {damageInfo.WeaponInfo.Type.ToString()} {damageInfo.WeaponInfo.Group.ToString()}" +
             $"\n~w~Bone: ~r~{damageInfo.BoneInfo.BoneId.ToString()} {damageInfo.BoneInfo.Limb.ToString()} {damageInfo.BoneInfo.BodyRegion.ToString()}");
        }

    }
}
