using Rage;

namespace EvidenceFR.Utils
{
    internal class VehicleDamageInfo
    {

        public readonly Vehicle vehicle;
        public readonly Material material;
        public readonly RelativeLocation relativeLocation;
        public readonly Vector3 collisionNormal;


        public VehicleDamageInfo(Vehicle veh, Material mat, Vector3 colNormal)
        {
            if (!veh) return;
            vehicle = veh;
            material = mat;
            collisionNormal = colNormal;

            float x = Math.Modulus(colNormal.X);
            float y = Math.Modulus(colNormal.Y);
            float z = Math.Modulus(colNormal.Z);

            // x<0 = Front
            // x>0 = Back

            // y<0 = Left
            // y>0 = Right

            // z<0 = Below
            // z>0 = Top

            // Front/Back
            if (x > y & x > z)
            {
                if (colNormal.X > 0)
                {
                    relativeLocation = RelativeLocation.Back;
                }
                else
                {
                    relativeLocation = RelativeLocation.Front;
                }
                // Left/Right
            }
            else if (y > x & y > z)
            {
                if (colNormal.Y > 0)
                {
                    relativeLocation = RelativeLocation.Right;
                }
                else
                {
                    relativeLocation = RelativeLocation.Left;
                }
                // Top/Below
            }
            else
            {
                if (colNormal.Z > 0)
                {
                    relativeLocation = RelativeLocation.Top;
                }
                else
                {
                    relativeLocation = RelativeLocation.Below;
                }
            }

        }

        public enum RelativeLocation
        {
            Front,
            Back,
            Left,
            Right,
            Top,
            Below,
        }

        public static string GetAccidentNameForMaterial(Material mat)
        {

            switch (mat)
            {
                case Material.Default:
                    return "Residues of different materials";
                    break;
                case Material.Concrete:
                    return "Residues of concrete";
                    break;
                case Material.ConcretePothole:
                    return "Residues of concrete";
                    break;
                case Material.ConcreteDusty:
                    return "Residues of concrete";
                    break;
                case Material.Tarmac:
                    return "Residues of tarmac";
                    break;
                case Material.TarmacPainted:
                    return "Residues of painted tarmac";
                    break;
                case Material.TarmacPothole:
                    return "Residues of tarmac";
                    break;
                case Material.RumbleStrip:
                    return "Residues of a rumble strip";
                    break;
                case Material.BreezeBlock:
                    return "Residues of terracotta";
                    break;
                case Material.Rock:
                    return "Residues of rock";
                    break;
                case Material.RockMossy:
                    return "Residues of mossy rocks";
                    break;
                case Material.Stone:
                    return "Residues of stone";
                    break;
                case Material.Cobblestone:
                    return "Residues of stone";
                    break;
                case Material.Brick:
                    return "Residues of bricks";
                    break;
                case Material.Marble:
                    return "Residues of marble";
                    break;
                case Material.PavingSlab:
                    return "Residues of stone";
                    break;
                case Material.SandstoneSolid:
                    return "Residues of sandstone";
                    break;
                case Material.SandstoneBrittle:
                    return "Residues of brittle sandstone";
                    break;
                case Material.SandLoose:
                    return "Residues of sand";
                    break;
                case Material.SandCompact:
                    return "Residues of sand";
                    break;
                case Material.SandWet:
                    return "Residues of wet sand";
                    break;
                case Material.SandTrack:
                    return "Residues of wet sand";
                    break;
                case Material.SandUnderwater:
                    return "Residues of wet sand";
                    break;
                case Material.SandDryDeep:
                    return "Residues of sand";
                    break;
                case Material.SandWetDeep:
                    return "Residues of wet sand";
                    break;
                case Material.Ice:
                    return "Residues of water and ice";
                    break;
                case Material.IceTarmac:
                    return "Residues of water and ice";
                    break;
                case Material.SnowLoose:
                    return "Residues of water and snow";
                    break;
                case Material.SnowCompact:
                    return "Residues of water and snow";
                    break;
                case Material.SnowDeep:
                    return "Residues of water and snow";
                    break;
                case Material.SnowTarmac:
                    return "Residues of water and snow";
                    break;
                case Material.GravelSmall:
                    return "Residues of gravel";
                    break;
                case Material.GravelLarge:
                    return "Residues of gravel";
                    break;
                case Material.GravelDeep:
                    return "Residues of gravel";
                    break;
                case Material.GravelTrainTrack:
                    return "Residues of gravel";
                    break;
                case Material.DirtTrack:
                    return "Residues of dirt";
                    break;
                case Material.MudHard:
                    return "Residues of mud";
                    break;
                case Material.MudPothole:
                    return "Residues of mud";
                    break;
                case Material.MudSoft:
                    return "Residues of mud";
                    break;
                case Material.MudUnderwater:
                    return "Residues of mud";
                    break;
                case Material.MudDeep:
                    return "Residues of mud";
                    break;
                case Material.Marsh:
                    return "Residues of marsh";
                    break;
                case Material.MarshDeep:
                    return "Residues of marsh";
                    break;
                case Material.Soil:
                    return "Residues of soil";
                    break;
                case Material.ClayHard:
                    return "Residues of clay";
                    break;
                case Material.ClaySoft:
                    return "Residues of clay";
                    break;
                case Material.GrassLong:
                    return "Residues of grass";
                    break;
                case Material.Grass:
                    return "Residues of grass";
                    break;
                case Material.GrassShort:
                    return "Residues of grass";
                    break;
                case Material.Hay:
                    return "Residues of hay";
                    break;
                case Material.Bushes:
                    return "Residues of bushes";
                    break;
                case Material.Twigs:
                    return "Residues of sticks";
                    break;
                case Material.Leaves:
                    return "Residues of leave";
                    break;
                case Material.Woodchips:
                    return "Residues of wood chips";
                    break;
                case Material.TreeBark:
                    return "Residues of tree bark";
                    break;
                case Material.MetalSolidSmall:
                    return "Residues of metal";
                    break;
                case Material.MetalSolidMedium:
                    return "Residues of metal";
                    break;
                case Material.MetalSolidLarge:
                    return "Residues of metal";
                    break;
                case Material.MetalHollowSmall:
                    return "Residues of metal";
                    break;
                case Material.MetalHollowMedium:
                    return "Residues of metal";
                    break;
                case Material.MetalHollowLarge:
                    return "Residues of metal";
                    break;
                case Material.MetalChainlinkSmall:
                    return "Residues of metal";
                    break;
                case Material.MetalChainlinkLarge:
                    return "Residues of metal";
                    break;
                case Material.MetalCorrugatedIron:
                    return "Residues of metal";
                    break;
                case Material.MetalGrille:
                    return "Residues of metal";
                    break;
                case Material.MetalRailing:
                    return "Residues of metal";
                    break;
                case Material.MetalDuct:
                    return "Residues of metal";
                    break;
                case Material.MetalGarageDoor:
                    return "Residues of metal";
                    break;
                case Material.MetalManhole:
                    return "Residues of metal";
                    break;
                case Material.WoodSolidSmall:
                    return "Residues of wood";
                    break;
                case Material.WoodSolidMedium:
                    return "Residues of wood";
                    break;
                case Material.WoodSolidLarge:
                    return "Residues of wood";
                    break;
                case Material.WoodSolidPolished:
                    return "Residues of polished wood";
                    break;
                case Material.WoodFloorDusty:
                    return "Residues of wood";
                    break;
                case Material.WoodHollowSmall:
                    return "Residues of wood";
                    break;
                case Material.WoodHollowMedium:
                    return "Residues of wood";
                    break;
                case Material.WoodHollowLarge:
                    return "Residues of wood";
                    break;
                case Material.WoodChipboard:
                    return "Residues of wood";
                    break;
                case Material.WoodOldCreaky:
                    return "Residues of wood";
                    break;
                case Material.WoodHighDensity:
                    return "Residues of wood";
                    break;
                case Material.WoodLattice:
                    return "Residues of wood";
                    break;
                case Material.Ceramic:
                    return "Residues of ceramic";
                    break;
                case Material.RoofTile:
                    return "Residues of roof tiles";
                    break;
                case Material.RoofFelt:
                    return "Residues of roof felt";
                    break;
                case Material.Fibreglass:
                    return "Residues of fiber glass";
                    break;
                case Material.Tarpaulin:
                    return "Residues of tarpaulin";
                    break;
                case Material.Plastic:
                    return "Residues of plastic";
                    break;
                case Material.PlasticHollow:
                    return "Residues of plastic";
                    break;
                case Material.PlasticHighDensity:
                    return "Residues of plastic";
                    break;
                case Material.PlasticClear:
                    return "Residues of plastic";
                    break;
                case Material.PlasticHollowClear:
                    return "Residues of plastic";
                    break;
                case Material.PlasticHighDensityClear:
                    return "Residues of plastic";
                    break;
                case Material.FibreglassHollow:
                    return "Residues of fiber glass";
                    break;
                case Material.Rubber:
                    return "Residues of fiber rubber";
                    break;
                case Material.RubberHollow:
                    return "Residues of fiber rubber";
                    break;
                case Material.Linoleum:
                    return "Residues of fiber linoleum";
                    break;
                case Material.Laminate:
                    return "Residues of fiber laminate";
                    break;
                case Material.CarpetSolid:
                    return "Residues of fiber";
                    break;
                case Material.CarpetSolidDusty:
                    return "Residues of fiber";
                    break;
                case Material.CarpetFloorboard:
                    return "Residues of fiber";
                    break;
                case Material.Cloth:
                    return "Residues of fiber";
                    break;
                case Material.PlasterSolid:
                    return "Residues of plaster";
                    break;
                case Material.PlasterBrittle:
                    return "Residues of plaster";
                    break;
                case Material.CardboardSheet:
                    return "Residues of paper";
                    break;
                case Material.CardboardBox:
                    return "Residues of cardboard";
                    break;
                case Material.Paper:
                    return "Residues of paper";
                    break;
                case Material.Foam:
                    return "Residues of foam";
                    break;
                case Material.FeatherPillow:
                    return "Residues of feathers";
                    break;
                case Material.Polystyrene:
                    return "Residues of polystyrene";
                    break;
                case Material.Leather:
                    return "Residues of leather";
                    break;
                case Material.Tvscreen:
                    return "Residues of glass and leds";
                    break;
                case Material.SlattedBlinds:
                    return "Residues of slatted blinds";
                    break;
                case Material.GlassShootThrough:
                    return "Residues of glass";
                    break;
                case Material.GlassBulletproof:
                    return "Residues of glass";
                    break;
                case Material.GlassOpaque:
                    return "Residues of glass";
                    break;
                case Material.Perspex:
                    return "Residues of perspex";
                    break;
                case Material.CarMetal:
                    return "Residues of car metal and lacquer";
                    break;
                case Material.CarPlastic:
                    return "Residues of car plastic";
                    break;
                case Material.CarSofttop:
                    return "Residues of fiber";
                    break;
                case Material.CarSofttopClear:
                    return "Residues of fiber and plastic";
                    break;
                case Material.CarGlassWeak:
                    return "Residues of glass";
                    break;
                case Material.CarGlassMedium:
                    return "Residues of glass";
                    break;
                case Material.CarGlassStrong:
                    return "Residues of glass";
                    break;
                case Material.CarGlassBulletproof:
                    return "Residues of glass";
                    break;
                case Material.CarGlassOpaque:
                    return "Residues of glass";
                    break;
                case Material.Water:
                    return "Residues of water";
                    break;
                case Material.Blood:
                    return "Blood strains";
                    break;
                case Material.Oil:
                    return "Oil strains";
                    break;
                case Material.Petrol:
                    return "Petrol strains";
                    break;
                case Material.FreshMeat:
                    return "Residues of meat";
                    break;
                case Material.DriedMeat:
                    return "Residues of meat";
                    break;
                case Material.EmissiveGlass:
                    return "Residues of glass";
                    break;
                case Material.EmissivePlastic:
                    return "Residues of plastic";
                    break;
                case Material.VfxMetalElectrified:
                    return "Residues of metal";
                    break;
                case Material.VfxMetalWaterTower:
                    return "Residues of metal";
                    break;
                case Material.VfxMetalSteam:
                    return "Residues of metal";
                    break;
                case Material.VfxMetalFlame:
                    return "Residues of metal";
                    break;
                case Material.PhysGolfBall:
                    return "Residues of plastic";
                    break;
                case Material.PhysTennisBall:
                    return "Residues of rubber and fiber";
                    break;
                case Material.PhysCaster:
                    return "Residues of plastic and metal";
                    break;
                case Material.PhysCasterRusty:
                    return "Residues of plastic and metal";
                    break;
                case Material.PhysElectricFence:
                    return "Residues of metal";
                    break;
                case Material.PhysElectricMetal:
                    return "Residues of metal";
                    break;
                case Material.PhysBarbedWire:
                    return "Residues of barbed wire";
                    break;
                case Material.PhysPooltableSurface:
                    return "Residues of fiber";
                    break;
                case Material.PhysPooltableCushion:
                    return "Residues of wood";
                    break;
                case Material.PhysPooltableBall:
                    return "Residues of plastic";
                    break;
                case Material.Buttocks:
                    return "Residues of meat and blood";
                    break;
                case Material.ThighLeft:
                    return "Residues of meat and blood";
                    break;
                case Material.ShinLeft:
                    return "Residues of meat and blood";
                    break;
                case Material.FootLeft:
                    return "Residues of meat and blood";
                    break;
                case Material.ThighRight:
                    return "Residues of meat and blood";
                    break;
                case Material.ShinRight:
                    return "Residues of meat and blood";
                    break;
                case Material.FootRight:
                    return "Residues of meat and blood";
                    break;
                case Material.Spine0:
                    return "Residues of meat and blood";
                    break;
                case Material.Spine1:
                    return "Residues of meat and blood";
                    break;
                case Material.Spine2:
                    return "Residues of meat and blood";
                    break;
                case Material.Spine3:
                    return "Residues of meat and blood";
                    break;
                case Material.ClavicleLeft:
                    return "Residues of meat and blood";
                    break;
                case Material.UpperArmLeft:
                    return "Residues of meat and blood";
                    break;
                case Material.LowerArmLeft:
                    return "Residues of meat and blood";
                    break;
                case Material.HandLeft:
                    return "Residues of meat and blood";
                    break;
                case Material.ClavicleRight:
                    return "Residues of meat and blood";
                    break;
                case Material.UpperArmRight:
                    return "Residues of meat and blood";
                    break;
                case Material.LowerArmRight:
                    return "Residues of meat and blood";
                    break;
                case Material.HandRight:
                    return "Residues of meat and blood";
                    break;
                case Material.Neck:
                    return "Residues of meat and blood";
                    break;
                case Material.Head:
                    return "Residues of hair and blood";
                    break;
                case Material.AnimalDefault:
                    return "Residues of blood";
                    break;
                case Material.CarEngine:
                    return "Residues of an engine";
                    break;
                case Material.Puddle:
                    return "Residues of water";
                    break;
                case Material.ConcretePavement:
                    return "Residues of concrete";
                    break;
                case Material.BrickPavement:
                    return "Residues of brick";
                    break;
                case Material.VfxWoodBeerBarrel:
                    return "Residues of beer";
                    break;
                case Material.WoodHighFriction:
                    return "Residues of wood";
                    break;
                case Material.RockNoinst:
                    return "Residues of rock";
                    break;
                case Material.BushesNoinst:
                    return "Residues of bushes";
                    break;
                case Material.MetalSolidRoadSurface:
                    return "Residues of metal";
                    break;
                case Material.StuntRampSurface:
                    return "Residues of wood";
                    break;
                case Material.None:
                    return "Residues of insects";
                    break;
                default:
                    return "None";
                    break;
            }

        }




        public enum Material : int
        {
            Default = -1775485061,
            Concrete = 1187676648,
            ConcretePothole = 359120722,
            ConcreteDusty = -1084640111,
            Tarmac = 282940568,
            TarmacPainted = -1301352528,
            TarmacPothole = 1886546517,
            RumbleStrip = -250168275,
            BreezeBlock = -954112554,
            Rock = -840216541,
            RockMossy = -124769592,
            Stone = 765206029,
            Cobblestone = 576169331,
            Brick = 1639053622,
            Marble = 1945073303,
            PavingSlab = 1907048430,
            SandstoneSolid = 592446772,
            SandstoneBrittle = 1913209870,
            SandLoose = -1595148316,
            SandCompact = 510490462,
            SandWet = 909950165,
            SandTrack = -1907520769,
            SandUnderwater = -1136057692,
            SandDryDeep = 509508168,
            SandWetDeep = 1288448767,
            Ice = -786060715,
            IceTarmac = -1931024423,
            SnowLoose = -1937569590,
            SnowCompact = -878560889,
            SnowDeep = 1619704960,
            SnowTarmac = 1550304810,
            GravelSmall = 951832588,
            GravelLarge = 2128369009,
            GravelDeep = -356706482,
            GravelTrainTrack = 1925605558,
            DirtTrack = -1885547121,
            MudHard = -1942898710,
            MudPothole = 312396330,
            MudSoft = 1635937914,
            MudUnderwater = -273490167,
            MudDeep = 1109728704,
            Marsh = 223086562,
            MarshDeep = 1584636462,
            Soil = -700658213,
            ClayHard = 1144315879,
            ClaySoft = 560985072,
            GrassLong = -461750719,
            Grass = 1333033863,
            GrassShort = -1286696947,
            Hay = -1833527165,
            Bushes = 581794674,
            Twigs = -913351839,
            Leaves = -2041329971,
            Woodchips = -309121453,
            TreeBark = -1915425863,
            MetalSolidSmall = -1447280105,
            MetalSolidMedium = -365631240,
            MetalSolidLarge = 752131025,
            MetalHollowSmall = 15972667,
            MetalHollowMedium = 1849540536,
            MetalHollowLarge = -583213831,
            MetalChainlinkSmall = 762193613,
            MetalChainlinkLarge = 125958708,
            MetalCorrugatedIron = 834144982,
            MetalGrille = -426118011,
            MetalRailing = 2100727187,
            MetalDuct = 1761524221,
            MetalGarageDoor = -231260695,
            MetalManhole = -754997699,
            WoodSolidSmall = -399872228,
            WoodSolidMedium = 555004797,
            WoodSolidLarge = 815762359,
            WoodSolidPolished = 126470059,
            WoodFloorDusty = -749452322,
            WoodHollowSmall = 1993976879,
            WoodHollowMedium = -365476163,
            WoodHollowLarge = -925419289,
            WoodChipboard = 1176309403,
            WoodOldCreaky = 722686013,
            WoodHighDensity = -1742843392,
            WoodLattice = 2011204130,
            Ceramic = -1186320715,
            RoofTile = 1755188853,
            RoofFelt = -1417164731,
            Fibreglass = 1354180827,
            Tarpaulin = -642658848,
            Plastic = -2073312001,
            PlasticHollow = 627123000,
            PlasticHighDensity = -1625995479,
            PlasticClear = -1859721013,
            PlasticHollowClear = 772722531,
            PlasticHighDensityClear = -1338473170,
            FibreglassHollow = -766055098,
            Rubber = -145735917,
            RubberHollow = -783934672,
            Linoleum = 289630530,
            Laminate = 1845676458,
            CarpetSolid = 669292054,
            CarpetSolidDusty = 158576196,
            CarpetFloorboard = -1396484943,
            Cloth = 122789469,
            PlasterSolid = -574122433,
            PlasterBrittle = -251888898,
            CardboardSheet = 236511221,
            CardboardBox = -1409054440,
            Paper = 474149820,
            Foam = 808719444,
            FeatherPillow = 1341866303,
            Polystyrene = -1756927331,
            Leather = -570470900,
            Tvscreen = 1429989756,
            SlattedBlinds = 673696729,
            GlassShootThrough = 937503243,
            GlassBulletproof = 244521486,
            GlassOpaque = 1500272081,
            Perspex = -1619794068,
            CarMetal = -93061983,
            CarPlastic = 2137197282,
            CarSofttop = -979647862,
            CarSofttopClear = 2130571536,
            CarGlassWeak = 1247281098,
            CarGlassMedium = 602884284,
            CarGlassStrong = 1070994698,
            CarGlassBulletproof = -1721915930,
            CarGlassOpaque = 513061559,
            Water = 435688960,
            Blood = 5236042,
            Oil = -634481305,
            Petrol = -1634184340,
            FreshMeat = 868733839,
            DriedMeat = -1445160429,
            EmissiveGlass = 1501078253,
            EmissivePlastic = 1059629996,
            VfxMetalElectrified = -309134265,
            VfxMetalWaterTower = 611561919,
            VfxMetalSteam = -691277294,
            VfxMetalFlame = 332778253,
            PhysNoFriction = 1666473731,
            PhysGolfBall = -1693813558,
            PhysTennisBall = -256704763,
            PhysCaster = -235302683,
            PhysCasterRusty = 2016463089,
            PhysCarVoid = 1345867677,
            PhysPedCapsule = -291631035,
            PhysElectricFence = -1170043733,
            PhysElectricMetal = -2013761145,
            PhysBarbedWire = -1543323456,
            PhysPooltableSurface = 605776921,
            PhysPooltableCushion = 972939963,
            PhysPooltableBall = -748341562,
            Buttocks = 483400232,
            ThighLeft = -460535871,
            ShinLeft = 652772852,
            FootLeft = 1926285543,
            ThighRight = -236981255,
            ShinRight = -446036155,
            FootRight = -1369136684,
            Spine0 = -1922286884,
            Spine1 = -1140112869,
            Spine2 = 1457572381,
            Spine3 = 32752644,
            ClavicleLeft = -1469616465,
            UpperArmLeft = -510342358,
            LowerArmLeft = 1045062756,
            HandLeft = 113101985,
            ClavicleRight = -1557288998,
            UpperArmRight = 1501153539,
            LowerArmRight = 1777921590,
            HandRight = 2000961972,
            Neck = 1718294164,
            Head = -735392753,
            AnimalDefault = 286224918,
            CarEngine = -1916939624,
            Puddle = 999829011,
            ConcretePavement = 2015599386,
            BrickPavement = -1147361576,
            PhysDynamicCoverBound = -2047468855,
            VfxWoodBeerBarrel = 998201806,
            WoodHighFriction = -2140087047,
            RockNoinst = 127813971,
            BushesNoinst = 1441114862,
            MetalSolidRoadSurface = -729112334,
            StuntRampSurface = -2088174996,
            Temp01 = 746881105,
            Temp02 = -1977970111,
            Temp03 = 1911121241,
            Temp04 = 1923995104,
            Temp05 = -1393662448,
            Temp06 = 1061250033,
            Temp07 = -1765523682,
            Temp08 = 1343679702,
            Temp09 = 1026054937,
            Temp10 = 63305994,
            Temp11 = 47470226,
            Temp12 = 702596674,
            Temp13 = -1637485913,
            Temp14 = -645955574,
            Temp15 = -1583997931,
            Temp16 = -1512735273,
            Temp17 = 1011960114,
            Temp18 = 1354993138,
            Temp19 = -801804446,
            Temp20 = -2052880405,
            Temp21 = -1037756060,
            Temp22 = -620388353,
            Temp23 = 465002639,
            Temp24 = 1963820161,
            Temp25 = 1952288305,
            Temp26 = -1116253098,
            Temp27 = 889255498,
            Temp28 = -1179674098,
            Temp29 = 1078418101,
            Temp30 = 13626292,
            None = 0
        }

    }
}
