using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidenceFR.BaseFunction.EvidenceObject
{
    public class EvidenceAttribute
    {
        public FIBBadge? FibBadge { get; set; }

        public EvidenceAttribute(FIBBadge fibBadge)
        {
            FibBadge = fibBadge;
        }
    }

    public class FIBBadge
    {
        public string Name { get; set; }
        public string DirectorName { get; set; }
        public int BadgeNumber { get; set; }

        public FIBBadge(string name, string directorName, int badgeNumber)
        {
            Name = name;
            DirectorName = directorName;
            BadgeNumber = badgeNumber;
        }
    }

    public enum EvidenceAttributeType
    {
        FIBBadge,
    }
}
