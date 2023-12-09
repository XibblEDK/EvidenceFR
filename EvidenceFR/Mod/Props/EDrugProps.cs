using EvidenceFR.Utils;

namespace EvidenceFR.Mod.Props
{
    public class EDrugProps : EProp
    {
        public static EDrugProps DrugTube => new("ng_proc_drug01a002", "Drug Tube");
        public static EDrugProps DrugBottle => new("prop_drug_bottle", "Drug Bottle");
        public static EDrugProps NeedleAndDrugTubes => new("v_61_bed1_mesh_drugstuff", "Needle And Drug Tubes");
        public static EDrugProps WeedBottle => new("prop_weed_bottle", "Weed Bottle");

        public EDrugProps(string propModelName, string displayName)
            : base(propModelName, displayName) { }
    }
}
