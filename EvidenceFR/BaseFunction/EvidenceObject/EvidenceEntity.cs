using EvidenceFR.BaseFunction.EvidenceObject;
using EvidenceFR.Utils;
using Rage;
using Rage.Native;
using RAGENativeUI;
using System.Windows.Forms;

namespace EvidenceFR.Functions.Object
{
    public class EvidenceEntity
    {
        public readonly Entity Entity;
        private EvidenceEntity evidence;
        public int evidenceID { get; private set; }

        public EvidenceCase parentCase;
        public EvidenceAttribute? Attribute;
        public float DiscoverRange = 2f;
        public bool CanBeDiscoveredByNPCCops = false;

        public event EvidenceFoundEventHandler OnEvidenceFound;
        public delegate void EvidenceFoundEventHandler(EvidenceEntity evidenceEntity);

        private bool canBeDiscovered = true;

        private void FireEvidenceFound()
        {
            EvidenceFoundEventHandler evidenceCaseAdded = OnEvidenceFound;
            if (evidenceCaseAdded == null)
                return;
            evidenceCaseAdded(this);
        }

        /// <summary>
        /// Set this to false if you don't want to player to discover this evidence piece.
        /// </summary>
        public bool CanBeDiscovered
        {
            get { return CanBeDiscovered; }
            set { canBeDiscovered = value; }
        }


        private bool found = false;

        /// <summary>
        /// This boolean determines if the evidence is found by the player or not
        /// </summary>
        public bool Found
        {
            get { return found; }
            set { found = value; FireEvidenceFound(); }
        }

        private bool deleteEntityWhenFound = false;

        /// <summary>
        /// Set this to true if you want to have the entity deleted after the player found the evidence
        /// </summary>
        public bool DeleteEntityWhenFound
        {
            get { return deleteEntityWhenFound; }
            set { deleteEntityWhenFound = value; }
        }

        private int addToCaseWhenFound = 0;

        /// <summary>
        /// Adds the evidence to the case board when found, if the evidence should not be added to a particular case, leave it on 0
        /// </summary>
        public int AddToCaseWhenFound
        {
            get { return addToCaseWhenFound; }
            set { addToCaseWhenFound = value; }
        }

        private string evidenceName = "Evidence";

        /// <summary>
        /// Change the name of the evidence. The player will see this.
        /// </summary>
        public string EvidenceName
        {
            get { return evidenceName; }
            set { evidenceName = value; }
        }


        /// <summary>
        /// Use this for creating an evidence piece from an existing entity in the game world
        /// WARNING: Returns null if the passed entity is invalid
        /// </summary>
        /// <param name="ent">An existing Rage Entity</param>
        /// <returns></returns>
        public EvidenceEntity(Entity ent, EvidenceCase parentCase, string evidenceName, EvidenceMarker evidenceMarker, EvidenceAttribute evidenceAttribute)
        {
            if (!ent)
            {
                // Entity invalid
                Logging.Log(Logging.LogLevel.Error, "The passed entity was invalid. Evidence was not created!");
                return;
            }
            Entity = ent;
            evidence = this;
            this.evidenceID = parentCase.evidenceEntities.Count == 0 ? 0 : parentCase.evidenceEntities.Count;
            this.parentCase = parentCase;
            this.Attribute = evidenceAttribute;
            this.EvidenceName = evidenceName;
            this.EvidenceMarker = evidenceMarker;
            parentCase.AddEvidenceEntity(this);
            EvidenceManager.RegisterEntity(this);
            Logging.Log(Logging.LogLevel.Debug, "Calling Fiber Function (" + EvidenceName + ")");
            StartEvidenceFiber();

        }

        public EvidenceEntity(Entity ent, EvidenceCase parentCase, string evidenceName, EvidenceMarker evidenceMarker)
        {
            if (!ent)
            {
                // Entity invalid
                Logging.Log(Logging.LogLevel.Error, "The passed entity was invalid. Evidence was not created!");
                return;
            }
            Entity = ent;
            evidence = this;
            this.evidenceID = parentCase.evidenceEntities.Count == 0 ? 0 : parentCase.evidenceEntities.Count;
            this.parentCase = parentCase;
            this.EvidenceName = evidenceName;
            this.EvidenceMarker = evidenceMarker;
            parentCase.AddEvidenceEntity(this);
            EvidenceManager.RegisterEntity(this);
            Logging.Log(Logging.LogLevel.Debug, "Calling Fiber Function (" + EvidenceName + ")");
            StartEvidenceFiber();

        }

        public void DeleteEvidence()
        {
            Logging.Log(Logging.LogLevel.Debug, $"Deleting Evidence ({EvidenceName})");
            StopEvidenceFiber();

            if (Entity)
            {
                Logging.Log(Logging.LogLevel.Debug, $"Removing Evidence ({EvidenceName}) from Case");
                parentCase.RemoveEvidenceEntity(this);
                Logging.Log(Logging.LogLevel.Debug, $"Deleting Entity ({EvidenceName})");
                Entity.Delete();
            }
        }

        public EvidenceMarker EvidenceMarker;
        public Vector3 MarkerOffset = new Vector3(0, 0, 1);

        public bool DrawMarkerAfterFound = true;

        private bool isEvidenceFiberRunning = false;
        private void StartEvidenceFiber()
        {
            Logging.Log(Logging.LogLevel.Debug, $"Starting Evidence Fiber {EvidenceName}");
            isEvidenceFiberRunning = true;
            GameFiber.StartNew(delegate
            {
                while (isEvidenceFiberRunning)
                {
                    GameFiber.Yield();
                    if (!Entity)
                    {
                        DeleteEvidence();
                        break;
                    }

                    if (DrawMarkerAfterFound & found)
                    {
                        EvidenceMarker.Draw(Entity.Position + MarkerOffset);
                    }
                }
            }, $"EvidenceFiber (C:{parentCase.caseId}.{evidenceName})");
        }

        private void StopEvidenceFiber()
        {
            isEvidenceFiberRunning = false;
        }

        public void Preview()
        {
            if (!Entity) return;
            if (EvidenceManager.isAnyEntityBeingPreviewed)
            {
                Game.DisplaySubtitle("~r~Another evidence is already being previewed at the moment.");
                return;
            }

            Game.DisplayHelp($"Press {Keys.Space.GetInstructionalId()} to exit the object preview.");


            GameFiber.StartNew(delegate
            {
                bool inEvidencePreview = true;
                EvidenceManager.isAnyEntityBeingPreviewed = true;
                Entity.Model.LoadAndWait();
                Rage.Object deer = new Rage.Object(Entity.Model, Game.LocalPlayer.Character.Position + new Vector3(3f, 0, 0));
                Camera camera = new Camera(true);
                Game.DisplaySubtitle(evidenceName);
                camera.Position = Game.LocalPlayer.Character.Position + new Vector3(1, 0, 0.5f);
                camera.Face(deer);
                GameFiber.StartNew(delegate
                {
                    GameFiber.SleepUntil(() => Game.IsKeyDown(Keys.Space), 100000);
                    inEvidencePreview = false;
                    if (deer) deer.Delete();
                    if (camera) camera.Delete();
                    EvidenceManager.isAnyEntityBeingPreviewed = false;
                });

                while (inEvidencePreview)
                {
                    GameFiber.Yield();
                    if (!deer || !camera) break;
                    NativeFunction.Natives.SET_​MOUSE_​CURSOR_​THIS_​FRAME();

                    NativeFunction.Natives.DISABLE_CONTROL_ACTION(0, 24, true); // Attack
                    NativeFunction.Natives.DISABLE_CONTROL_ACTION(0, 69, true); // Attack
                    NativeFunction.Natives.DISABLE_CONTROL_ACTION(0, 70, true); // Attack
                    NativeFunction.Natives.DISABLE_CONTROL_ACTION(0, 92, true); // Attack
                    NativeFunction.Natives.DISABLE_CONTROL_ACTION(0, 114, true); // Attack
                    NativeFunction.Natives.DISABLE_CONTROL_ACTION(0, 121, true); // Attack
                    NativeFunction.Natives.DISABLE_CONTROL_ACTION(0, 140, true); // Attack
                    NativeFunction.Natives.DISABLE_CONTROL_ACTION(0, 141, true); // Attack
                    NativeFunction.Natives.DISABLE_CONTROL_ACTION(0, 142, true); // Attack
                    NativeFunction.Natives.DISABLE_CONTROL_ACTION(0, 257, true); // Attack
                    NativeFunction.Natives.DISABLE_CONTROL_ACTION(0, 263, true); // Attack
                    NativeFunction.Natives.DISABLE_CONTROL_ACTION(0, 264, true); // Attack
                    NativeFunction.Natives.DISABLE_CONTROL_ACTION(0, 331, true); // Attack

                    NativeFunction.Natives.DISABLE_CONTROL_ACTION(0, 14, true); // Weapon Wheel 
                    NativeFunction.Natives.DISABLE_CONTROL_ACTION(0, 15, true); // Weapon Wheel
                    NativeFunction.Natives.DISABLE_CONTROL_ACTION(0, 16, true); // Weapon Wheel
                    NativeFunction.Natives.DISABLE_CONTROL_ACTION(0, 17, true); // Weapon Wheel
                    NativeFunction.Natives.DISABLE_CONTROL_ACTION(0, 261, true); // Weapon Wheel
                    NativeFunction.Natives.DISABLE_CONTROL_ACTION(0, 262, true); // Weapon Wheel

                    Vector3 traceStart;
                    Vector3 traceDir;
                    bool work = World.ConvertScreenPositionToTrace(camera, new Vector2(Cursor.Position.X, Cursor.Position.Y), out traceStart, out traceDir);
                    traceDir = traceDir * 50000 * -1;
                    if (deer) deer.Face(traceDir);
                    if (Game.IsControlJustPressed(0, GameControl.VehicleCinematicDownOnly)) camera.FOV += 3;
                    if (Game.IsControlJustPressed(0, GameControl.VehicleCinematicUpOnly)) camera.FOV -= 3;
                }
            });
        }

    }
}
