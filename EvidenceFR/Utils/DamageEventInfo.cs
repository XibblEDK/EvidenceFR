using DamageTrackerLib.DamageInfo;
using Rage;
using Rage.Native;
using System.Drawing;

namespace EvidenceFR.Utils
{
    internal class DamageEventInfo
    {
        public readonly Ped victimPed;
        public readonly Ped attackerPed;
        public readonly Vector3 victimPosTOD;
        public readonly Vector3 attackerPosTOD;

        public readonly PedDamageInfo pedDamageInfo;


        public DamageEventInfo(Ped victim, Ped attacker, PedDamageInfo damageInfo)
        {
            if (!victim || !attacker) return;
            if (victim & attacker) Logging.Log(Logging.LogLevel.Debug, "victim and attacker ped is valid");

            victimPosTOD = victim.Position;
            attackerPosTOD = attacker.Position;

            victimPed = victim;
            if(victimPed) Logging.Log(Logging.LogLevel.Debug, "victim STILL valid");
            attackerPed = attacker;
            pedDamageInfo = damageInfo;

            if (damageInfo.WeaponInfo.Group == DamageGroup.Bullet)
            {
                Game.DisplayHelp("Raycasting");
                Entity currentWeapon = NativeFunction.Natives.GET_CURRENT_PED_WEAPON_ENTITY_INDEX<Entity>(attacker, false);
                Vector3 pos = currentWeapon.Position;
                if (currentWeapon == null) Game.DisplayNotification(":(");
                TraceFlags traceFlags = TraceFlags.IntersectEverything;

                Logging.Log(Logging.LogLevel.Debug, "Raycasting");
                Game.DisplayNotification(currentWeapon + "~n~" + traceFlags + "~n~" + attacker);
                HitResult hit = Math.Raycast(currentWeapon.Position, currentWeapon.RightVector, 1000, traceFlags, Game.LocalPlayer.Character, currentWeapon, attacker);
                Logging.Log(Logging.LogLevel.Debug, "Raycasting done");
                if (hit.HitEntity != null)
                {
                    if (hit.HitEntity != victimPed) Logging.Log(Logging.LogLevel.Warning, "Could not determine exact hit location, is your game laggy?");

                }
                else
                {
                    Logging.Log(Logging.LogLevel.Warning, "No entity hit");
                }
                Vector3 offset = Vector3.Zero;
                if (victimPed)
                {
                    Logging.Log(Logging.LogLevel.Debug, "victimPed ped is still valid");
                    Logging.Log(Logging.LogLevel.Debug, "Getting world position of bone");
                    Vector3 bonePos = NativeFunction.Natives.GET_WORLD_POSITION_OF_ENTITY_BONE<Vector3>(victimPed, 0);
                    Logging.Log(Logging.LogLevel.Debug, "Calculating offset");
                    offset = hit.HitPosition - victim.Position;
                    Logging.Log(Logging.LogLevel.Debug, "Bone Position: " + bonePos.ToString());
                    Logging.Log(Logging.LogLevel.Debug, "Hit Position: " + hit.HitPosition.ToString());
                    Logging.Log(Logging.LogLevel.Debug, "Offset: " + offset);
                    Logging.Log(Logging.LogLevel.Debug, "Everything done.");
                }
                else
                {
                    Logging.Log(Logging.LogLevel.Warning, "victimPed ped is invalid");
                }

                Logging.Log(Logging.LogLevel.Debug, "Drawing debug line");
                GameFiber.StartNew(delegate
                {
                    bool run = true;
                    GameFiber.StartNew(delegate
                    {
                        while (run)
                        {
                            GameFiber.Yield();
                            //Debug.DrawLine(pos, hit.HitPosition, Color.Red);
                            if (victimPed) Debug.DrawSphere(victimPed.Position - offset, 0.2f, Color.Red);
                            Game.DisplaySubtitle(victimPed.IsValid().ToString());

                            for(float i = 0; i<pos.DistanceTo(hit.HitPosition)*2; i++)
                            {
                                Vector3 dir = hit.HitPosition - pos;
                                Debug.DrawArrow(pos+dir*(i/4), dir, Rotator.Zero, 0.25f, Color.Red);
                            }

                        }
                    });

                    GameFiber.SleepUntil(() => Game.IsKeyDown(System.Windows.Forms.Keys.F10), 60000);
                    run = false;
                });
            }


        }
    }
}
