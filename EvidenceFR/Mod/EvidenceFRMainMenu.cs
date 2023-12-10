using EvidenceFR.Functions.Object;
using RAGENativeUI;
using RAGENativeUI.Elements;
using System.Collections.Generic;
using System.Drawing;

namespace EvidenceFR.Mod
{
    internal static class EvidenceFRMainMenu
    {
        internal static readonly UIMenu Menu = new("EvidenceFR", "Unveiling tthe truth");
        internal static readonly UIMenu CasesMenu = new("Cases", "Unveiling the truth");
        internal static readonly UIMenu CaseMenu = new("", "Incident Data");
        internal static readonly UIMenu EvidenceMenu = new("Evidence Name", "Evidence Data");

        private static readonly UIMenuItem CasesMenuItem = new("Cases");

        private static readonly List<UIMenuItem> CasesList = new List<UIMenuItem>();
        private static readonly Dictionary<int, UIMenu> CaseMenus = new Dictionary<int, UIMenu>();
        private static readonly Dictionary<int, UIMenu> EvidenceMenus = new Dictionary<int, UIMenu>();

        internal static EvidenceCase CurrentCase;
        internal static EvidenceEntity CurrentEvidence;

        internal static void SetupMenu()
        {
            EvidenceFR.Instance.menuPool.Add(Menu);
            Menu.MouseControlsEnabled = false;
            Menu.AllowCameraMovement = true;
            Menu.TitleStyle = new TextStyle(TextFont.ChaletLondon, Color.White);

            Menu.AddItems(CasesMenuItem);
            Menu.BindMenuToItem(CasesMenu, CasesMenuItem);

            EvidenceFR.Instance.menuPool.Add(CaseMenu, EvidenceMenu, CasesMenu);

            Events.EvidenceCaseAdded += OnCaseAdded;
            Events.EvidenceEntityAddedToCase += EvidenceEntityAddedToCase;
        }

        private static void EvidenceEntityAddedToCase(EvidenceEntity evidenceEntity, EvidenceCase evidenceCase)
        {
            UIMenuItem uIMenuItem = !evidenceEntity.Found ? new UIMenuItem("Undiscovered Evidence") : new UIMenuItem(evidenceEntity.EvidenceName);
            CaseMenus[evidenceCase.caseId].AddItem(uIMenuItem);
            evidenceEntity.OnEvidenceFound += OnEvidenceFound;
        }

        private static void OnEvidenceFound(EvidenceEntity evidenceEntity)
        {
            CaseMenus[evidenceEntity.parentCase.caseId].MenuItems[evidenceEntity.evidenceID].Text = evidenceEntity.EvidenceName;
            CaseMenus[evidenceEntity.parentCase.caseId].MenuItems[evidenceEntity.evidenceID].Description = evidenceEntity.Found.ToString();

            UIMenu uIMenu = CreateEvidenceMenu(evidenceEntity);
            EvidenceFR.Instance.menuPool.Add(uIMenu);
        }

        private static UIMenu CreateEvidenceMenu(EvidenceEntity evidenceEntity)
        {
            UIMenu uiMenu = new UIMenu(evidenceEntity.EvidenceName, evidenceEntity.evidenceID.ToString());
            EvidenceFR.Instance.menuPool.Add(uiMenu);
            CaseMenus[evidenceEntity.parentCase.caseId].BindMenuToItem(uiMenu, CaseMenus[evidenceEntity.parentCase.caseId].MenuItems[evidenceEntity.evidenceID]);

            // Create evidence options.

            UIMenuItem preview = new UIMenuItem("Preview Evidence");
            preview.Activated += (s, e) =>
            {
                uiMenu.Visible = false;
                evidenceEntity.Preview();
            };
            uiMenu.AddItem(preview);

            if (evidenceEntity.Attribute != null)
            {
                UIMenuItem attributeBinder = new UIMenuItem("Attributes");
                UIMenu attributeMenu = new UIMenu("Attributes", " ");
                uiMenu.AddItem(attributeBinder);
                uiMenu.BindMenuToItem(attributeMenu, attributeBinder);
                EvidenceFR.Instance.menuPool.Add(attributeMenu);

                if (evidenceEntity.Attribute.FibBadge != null)
                {
                    UIMenuItem fibbadge = new UIMenuItem("FIB Badge");
                    fibbadge.Activated += (s, e) =>
                    {
                        EvidenceManager.EnableAttributeUI(BaseFunction.EvidenceObject.EvidenceAttributeType.FIBBadge);
                    };
                    attributeMenu.AddItem(fibbadge);
                }
            }

            return uiMenu;
        }

        private static void OnCaseAdded(EvidenceCase newEvidenceCase)
        {
            UIMenuItem caseItem = new UIMenuItem("#" + newEvidenceCase.caseId);

            UIMenu caseSubMenu = new UIMenu("#" + newEvidenceCase.caseId, "INCIDENT DATA AND TIME OF CLOCK CALL WAS STARTED");
            CaseMenus.Add(newEvidenceCase.caseId, caseSubMenu);
            EvidenceFR.Instance.menuPool.Add(caseSubMenu);

            foreach (EvidenceEntity evidence in EvidenceManager.GetCaseById(newEvidenceCase.caseId).evidenceEntities)
            {
                UIMenuItem evidenceItem = new UIMenuItem(evidence.EvidenceName, evidence.Found.ToString());
                caseSubMenu.AddItem(evidenceItem);
            }

            CasesMenu.AddItem(caseItem);

            CasesMenu.BindMenuToItem(caseSubMenu, caseItem);
        }
    }
}
