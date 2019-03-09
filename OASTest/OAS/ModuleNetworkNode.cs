using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS
{
    public static class ModuleNetworkNode
    {
        internal static string NetworkNode; // Set by the user
        internal static string NetworkPath; // Network path to include for remoting
        internal static bool NetworkNodeChanged;
        public static OPCSystems.OPCSystemsComponent OASSystemComponent = new OPCSystems.OPCSystemsComponent();

        public static void SetNetworkNode(string NodeName)
        {
            NetworkNode = NodeName;
            if (string.Compare(NetworkNode, "localhost", true) == 0 || string.IsNullOrEmpty(NetworkNode))
            {
                NetworkPath = "";
            }
            else
            {
                NetworkPath = "\\\\" + NetworkNode + "\\";
                NetworkNodeChanged = true;
            }
        }
    }
}
