using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EvidenceFR.Utils
{
    public abstract class EProp
    {
        public string DisplayName { get; private set; }

        public string PropModelName { get; private set; }

        protected EProp(string propModelName, string displayName) => (PropModelName, DisplayName) = (propModelName, displayName);
    }
}
