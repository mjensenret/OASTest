using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OAS
{
    public static class DataConnector
    {
        public static OPCSystemsDataConnector.OPCSystemsData OASData = new OPCSystemsDataConnector.OPCSystemsData();

        public static void AddNewTag(string tagName)
        {
            OASData.AddTag(tagName);
        }

        public static string ReadTagValues(string[] tagName)
        {
            
            Int32[] Errors = null;
            // The Errors array will be sized to the same length as your Tags array.
            // The following values will be returned to you in the Int32 array you pass.
            // 0 = Good Quality
            // 1 = Bad Quality
            // 2 = Timeout

            object[] Values = null;
            Values = OASData.SyncReadTags(tagName, ref Errors, 10000);
            // The Values array will be sized to the number of Tags you ask for and contain the values if there is Good Quality.
            Int32 TagIndex = 0;
            System.Text.StringBuilder tempStringBuilder = new System.Text.StringBuilder();
            double tempDouble = 0;

            for (TagIndex = 0; TagIndex < tagName.GetLength(0); TagIndex++)
            {
                tempStringBuilder.Append(tagName[TagIndex]);
                tempStringBuilder.Append(" = ");
                switch (Errors[TagIndex])
                {
                    case 0:
                        tempDouble = Convert.ToDouble(Values[TagIndex]);
                        tempStringBuilder.Append(tempDouble.ToString());
                        break;
                    case 1:
                        tempStringBuilder.Append("Bad Quality");
                        break;
                    case 2:
                        tempStringBuilder.Append("Timeout");
                        break;
                }
                tempStringBuilder.AppendLine();
            }

            return tempStringBuilder.ToString();
        }
    }
}
