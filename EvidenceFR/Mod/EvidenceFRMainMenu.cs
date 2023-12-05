using EvidenceFR.Functions.Object;
using RAGENativeUI;
using RAGENativeUI.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EvidenceFR.Mod
{
    internal static class EvidenceFRMainMenu
    {
        internal static readonly UIMenu Menu = new("EvidenceFR", "Unveiling truth through meticulous examination and compelling proof.");
        internal static readonly UIMenu CasesMenu = new("Cases", "Unveiling truth through meticulous examination and compelling proof.");
        internal static readonly UIMenu CaseMenu = new("", "Incident Data");
        internal static readonly UIMenu EvidenceMenu = new("Evidence Name", "Evidence Data");

        private static readonly UIMenuItem CasesMenuItem = new("Cases");

        private static readonly List<UIMenuItem> CasesList = new List<UIMenuItem>();
        private static readonly Dictionary<int, UIMenu> CaseMenus = new Dictionary<int, UIMenu>();

        internal static EvidenceCase CurrentCase;
        internal static EvidenceEntity CurrentEvidence;

        internal static void SetupMenu()
        {
            EvidenceFR.Instance.menuPool.Add(Menu);
            Menu.MouseControlsEnabled = false;
            Menu.AllowCameraMovement = true;

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
        }

        private static void OnCaseAdded(EvidenceCase newEvidenceCase)
        {
            UIMenuItem caseItem = new UIMenuItem("#" + newEvidenceCase.caseId);

            UIMenu caseSubMenu = new UIMenu(newEvidenceCase.caseId.ToString(), "INCIDENT DATA AND TIME OF CLOCK CALL WAS STARTED");
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
