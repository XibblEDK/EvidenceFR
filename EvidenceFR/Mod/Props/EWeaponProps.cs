using EvidenceFR.Utils;

namespace EvidenceFR.Mod.Props
{
    public class EWeaponProps : EProp
    {
        public static EWeaponProps FireExtinguisher => new EWeaponProps("w_am_fire_exting", "Fire Extinguisher");
        public static EWeaponProps JerryCan => new EWeaponProps("w_am_jerrycan", "Jerry Can");
        public static EWeaponProps Flare => new EWeaponProps("w_am_flare", "Flare");
        public static EWeaponProps Bat => new EWeaponProps("w_me_bat", "Bat");
        public static EWeaponProps Crowbar => new EWeaponProps("w_me_crowbar", "Crowbar");
        public static EWeaponProps Hammer => new EWeaponProps("w_me_hammer", "Hammer");
        public static EWeaponProps Knife => new EWeaponProps("w_me_knife_01", "Knife");
        public static EWeaponProps Nightstick => new EWeaponProps("w_me_nightstick", "Nightstick");
        public static EWeaponProps Golfclub => new EWeaponProps("w_me_gclub", "Golfclub");
        public static EWeaponProps Pistol => new EWeaponProps("w_pi_pistol", "Pistol");
        public static EWeaponProps APPistol => new EWeaponProps("w_pi_appistol", "APPistol");
        public static EWeaponProps CombatPistol => new EWeaponProps("w_pi_combatpistol", "Combat Pistol");
        public static EWeaponProps StunGun => new EWeaponProps("w_pi_stungun", "Stun Gun");
        public static EWeaponProps Pistol50 => new EWeaponProps("w_pi_pistol50", "Pistol50");
        public static EWeaponProps SMG => new EWeaponProps("w_sb_smg", "SMG");
        public static EWeaponProps MicroSMG => new EWeaponProps("w_sb_microsmg", "Micro SMG");
        public static EWeaponProps AssaultSMG => new EWeaponProps("w_sb_assaultsmg", "Assault SMG");
        public static EWeaponProps PumpshotGun => new EWeaponProps("w_sg_pumpshotgun", "Pumpshotgun");
        public static EWeaponProps Sawnoff => new EWeaponProps("w_sg_sawnoff", "Sawnoff");
        public static EWeaponProps AssaultShotgun => new EWeaponProps("w_sg_assaultshotgun", "Assault Shotgun");
        public static EWeaponProps BullpupShotgun => new EWeaponProps("w_sg_bullpupshotgun", "Bullpup Shotgun");
        public static EWeaponProps AssaultRifle => new EWeaponProps("w_ar_assaultrifle", "Assault Rifle");
        public static EWeaponProps CarbineRifle => new EWeaponProps("w_ar_carbinerifle", "Carbine Rifle");
        public static EWeaponProps AdvancedRifle => new EWeaponProps("w_ar_advancedrifle", "Advanced Rifle");
        public static EWeaponProps SniperRifle => new EWeaponProps("w_sr_sniperrifle", "Sniper Rifle");
        public static EWeaponProps HeavySniper => new EWeaponProps("w_sr_heavysniper", "Heavy Sniper");
        public static EWeaponProps GrenadeLauncher => new EWeaponProps("w_lr_grenadelauncher", "Grenade Launcher");
        public static EWeaponProps RPG => new EWeaponProps("w_lr_rpg", "RPG");
        public static EWeaponProps RPGRocket => new EWeaponProps("w_lr_rpg_rocket", "RPG Rocket");
        public static EWeaponProps MG => new EWeaponProps("w_mg_mg", "MG");
        public static EWeaponProps CombatMG => new EWeaponProps("w_mg_combatmg", "Combat MG");
        public static EWeaponProps MiniGun => new EWeaponProps("w_mg_minigun", "Minigun");
        public static EWeaponProps C4 => new EWeaponProps("w_ex_pe", "C4");
        public static EWeaponProps SmokeGrenade => new EWeaponProps("w_ex_grenadesmoke", "Smoke Grenade");
        public static EWeaponProps GrenadeFrag => new EWeaponProps("w_ex_grenadefrag", "Grenade");
        public static EWeaponProps Molotov => new EWeaponProps("w_ex_molotov", "Molotov");
        public static EWeaponProps BrokenBottle => new EWeaponProps("w_me_bottle", "Broken Bottle");
        public static EWeaponProps SNSPistol => new EWeaponProps("w_pi_sns_pistol", "SNS Pistol");
        public static EWeaponProps HeavyPistol => new EWeaponProps("w_pi_heavypistol", "Heavy Pistol");
        public static EWeaponProps SpecialCarbine => new EWeaponProps("w_ar_specialcarbine", "Spepial Carbine");
        public static EWeaponProps BullpupRifle => new EWeaponProps("w_ar_bullpuprifle", "Bullpup Rifle");
        public static EWeaponProps CandyCane => new EWeaponProps("w_me_candy_xm3", "Candy cane");
        public static EWeaponProps PistolXM3 => new EWeaponProps("w_pi_pistol_xm3", "Pistol XM3");
        public static EWeaponProps Paper => new EWeaponProps("w_am_papers_xm3", "Newspaper");
        public static EWeaponProps RailGun => new EWeaponProps("w_ar_railgun_xm3", "Railgun");
        public static EWeaponProps RayGun => new EWeaponProps("w_pi_raygun", "Raygun");
        public static EWeaponProps VehicleMissile => new EWeaponProps("w_ex_vehiclemissile_4", "Vehicle Missile");
        public static EWeaponProps Homing => new EWeaponProps("w_lr_homing", "Homing Launcher");
        public static EWeaponProps HomingRocket => new EWeaponProps("w_lr_homing_rocket", "Homing Rocket");
        public static EWeaponProps APMine => new EWeaponProps("w_ex_apmine", "AP Mine");
        public static EWeaponProps Gusenberg => new EWeaponProps("w_sb_gusenberg", "Gusenberg");
        public static EWeaponProps Dagger => new EWeaponProps("w_me_dagger", "Dagger");
        public static EWeaponProps VinagePistol => new EWeaponProps("w_pi_vintage_pistol", "Vintage Pistol");
        public static EWeaponProps Musket => new EWeaponProps("w_ar_musket", "Musket");
        public static EWeaponProps HeavyShotgun => new EWeaponProps("w_sg_heavyshotgun", "Heavy Shotgun");
        public static EWeaponProps MarksmanRifle => new EWeaponProps("w_sr_marksmanrifle", "Marksman Rifle");
        public static EWeaponProps CeramicPistol => new EWeaponProps("w_pi_ceramic_pistol", "Ceramic Pistol");
        public static EWeaponProps FlareGun => new EWeaponProps("w_pi_flaregun", "Flare Gun");
        public static EWeaponProps KnucleDuster => new EWeaponProps("w_me_knuckle", "Knuckle Duster");
        public static EWeaponProps PDW => new EWeaponProps("w_sb_pdw", "PDW");
        public static EWeaponProps Machette => new EWeaponProps("w_me_machette_lr", "Machette");
        public static EWeaponProps CompactSMG => new EWeaponProps("w_sb_compactsmg", "Compact SMG");
        public static EWeaponProps AssaultRifleSMG => new EWeaponProps("w_ar_assaultrifle_smg", "Assault Rifle SMG");
        public static EWeaponProps DoubleBarrel => new EWeaponProps("w_sg_doublebarrel", "Double Barrel");
        public static EWeaponProps Flashlight => new EWeaponProps("w_me_flashlight", "Flashlight");
        public static EWeaponProps PipeBomb => new EWeaponProps("w_ex_pipebomb", "Pipe bomb");
        public static EWeaponProps BattleAxe => new EWeaponProps("w_me_battleaxe", "Battle Axe");
        public static EWeaponProps PoolCue => new EWeaponProps("w_me_poolcue", "Pool Cue");
        public static EWeaponProps Wrench => new EWeaponProps("w_me_wrench", "Wrench");

        public EWeaponProps(string propModelName, string displayName)
            : base(propModelName, displayName) { }
    }
}
