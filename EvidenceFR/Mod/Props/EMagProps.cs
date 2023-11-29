using EvidenceFR.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidenceFR.Mod.Props
{
    public class EMagProps : EProp
    {
        public static EMagProps PistolMag => new("w_pi_pistol_mag1", "Pistol Mag");
        public static EMagProps ExtendedPistolMag => new("w_pi_pistol_mag2", "Extended Pistol Mag");
        public static EMagProps APPistolMag => new("w_pi_appistol_mag1", "APPistol Mag");
        public static EMagProps ExtendedAPPistolMag => new("w_pi_appistol_mag2", "Extended APPistol Mag");
        public static EMagProps CombatPistolMag => new("w_pi_combatpistol_mag1", "Combat Pistol Mag");
        public static EMagProps ExtendedCombatPistolMag => new("w_pi_combatpistol_mag2", "Extended Combat Pistol Mag");
        public static EMagProps Pistol50Mag => new("w_pi_pistol50_mag1", "Pistol50 Mag");
        public static EMagProps ExtendedPistol50Mag => new("w_pi_pistol50_mag2", "Extended Pistol50 Mag");
        public static EMagProps SMGMag => new("w_sb_smg_mag1", "SMG Mag");
        public static EMagProps ExtendedSMGMag => new("w_sb_smg_mag2", "Extended SMG Mag");
        public static EMagProps AssaultSMGMag => new("w_sb_assaultsmg_mag1", "Assault SMG Mag");
        public static EMagProps ExtendedAssaultSMGMag => new("w_sb_assaultsmg_mag2", "Extended Assault SMG Mag");
        public static EMagProps CombatMGMag => new("w_mg_combatmg_mag1", "Combat MG Mag");
        public static EMagProps ExtendedCombatMGMag => new("w_mg_combatmg_mag2", "Extended Combat MG Mag");
        public static EMagProps SNSPistolMag => new("w_pi_sns_pistol_mag1", "SNS Pistol Mag");
        public static EMagProps ExtendedSNSPistolMag => new("w_pi_sns_pistol_mag2", "Extended SNS Pistol Mag");
        public static EMagProps HeavyPistolMag = new("w_pi_heavypistol_mag1", "Heavy Pistol Mag");
        public static EMagProps ExtendedHeavyPistolMag = new("w_pi_heavypistol_mag2", "Extended Heavy Pistol Mag");
        public static EMagProps SpecialCarbineMag => new("w_ar_specialcarbine_mag1", "Special Carbine Mag");
        public static EMagProps ExtendedSpecialCarbineMag => new("w_ar_specialcarbine_mag2", "Extended Special Carbine Mag");
        public static EMagProps BullpupRifleMag => new("w_ar_bullpuprifle_mag1", "Bullpup Rifle Mag");
        public static EMagProps ExtendedBullpupRifleMag => new("w_ar_bullpuprifle_mag2", "Extended Bullpup Rifle Mag");
        public static EMagProps VintagePistolMag => new("w_pi_vintage_pistol_mag1", "Vintage Pistol Mag");
        public static EMagProps ExtendedVintagePistolMag => new("w_pi_vintage_pistol_mag2", "Extended Vintage Pistol Mag");
        public static EMagProps PDWMag => new("w_sb_pdw_mag1", "PDW Mag");
        public static EMagProps ExtendedPDWMag => new("w_sb_pdw_mag2", "Extended PDW Mag");
        public static EMagProps MicroSMGMag => new("w_sb_microsmg_mag1", "Micro SMG Mag");
        public static EMagProps ExtendedMicroSMGMag => new("w_sb_microsmg_mag2", "Extended Micro SMG Mag");
        public static EMagProps MiniSMGMag => new("w_sb_minismg_mag1", "Mini SMG Mag");
        public static EMagProps ExtendedMiniSMGMag => new("w_sb_minismg_mag2", "Extended Mini SMG Mag");
        public static EMagProps PrecisionRifleMag => new("w_sr_w_sr_precisionrifle_reh_mag1", "Precision Rifle Mag");
        public static EMagProps PistolSMGMag => new("w_pi_pistolsmg_m31_mag1", "Pistol SMG Mag");
        public static EMagProps ExtendedPistolSMGMag => new("w_pi_pistolsmg_m31_mag2", "Extended Pistol SMG Mag");

        public EMagProps(string propModelName, string name)
            : base(propModelName, name) { }
    }
}
