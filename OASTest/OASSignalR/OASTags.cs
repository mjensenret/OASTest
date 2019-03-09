using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using OASSignalR.Models;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace OASSignalR
{
    public class OASTags
    {
        private bool m_Closing;
        private readonly static Lazy<OASTags> _instance = new Lazy<OASTags>(() => new OASTags(GlobalHost.ConnectionManager.GetHubContext<OASHub>().Clients));
        private readonly ConcurrentDictionary<string, TagValueModel> _tags = new ConcurrentDictionary<string, TagValueModel>();
        // A Queue or Hashtable is recommended to receieve your values so they can be processed on a seperate thread.
        // You can also process data directly from the ValuesChangedAll Event, but keep in mind other values will be buffered until you release the Event.
        private Queue m_DataValuesQueue = new Queue(); // This Queue demonstrates a technique for processing a lot of data.
        private Hashtable m_DataValuesHashtable = new Hashtable(); // This hashtable will contain the values for each Tag to access whenever you like.
        private readonly object _updateTagValuesLock = new object();
        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(250);
        internal Timer TimerToProcessValues;
        internal OPCSystemsDataConnector.OPCSystemsData OpcSystemsData;
        private bool m_InProcessValues = false;

        private OASTags(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
            LoadDefaultTags();


        }
        public string GetTagValues()
        {
            return _tags.Values;
        }


        public static OASTags Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private void LoadDefaultTags()
        {
            _tags.Clear();

            string[] Tags = new string[3];
            Tags[0] = "Ramp.Value";
            Tags[1] = "Sine.Value";
            Tags[2] = "Random.Value";
            Int32[] Errors = null;
            // The Errors array will be sized to the same length as your Tags array.
            // The following values will be returned to you in the Int32 array you pass.
            // 0 = Good Quality
            // 1 = Bad Quality
            // 2 = Timeout
            string values = null;
            values = OAS.DataConnector.ReadTagValues(Tags);

        }
        
        private void OpcSystemsData_ValuesChangedAll(string[] Tags, object[] Values, bool[] Qualities, DateTime[] TimeStamps)
        {
            // High speed version
            lock (m_DataValuesQueue.SyncRoot)
            {
                m_DataValuesQueue.Enqueue(new ClassTagValues(Tags, Values, Qualities, TimeStamps));
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        private void TimerToProcessValues_Tick(object sender, System.EventArgs e)
        {
            if (m_InProcessValues)
            {
                return;
            }
            if (m_Closing)
            {
                return;
            }
            m_InProcessValues = true;

            ClassTagValues[] arrTagValues = new ClassTagValues[0];
            Int32 numberOfTagValues = 0;
            lock (m_DataValuesQueue.SyncRoot)
            {
                if (m_DataValuesQueue.Count < 1)
                {
                    m_InProcessValues = false;
                    return; // There is nothing to do
                }
                numberOfTagValues = m_DataValuesQueue.Count;
                arrTagValues = new ClassTagValues[numberOfTagValues];
                m_DataValuesQueue.CopyTo(arrTagValues, 0);
                m_DataValuesQueue.Clear();
            }

            Int32 allTagValuesIndex = 0;
            ClassTagValues allTagValues = null;

            string[] tagNames = null;
            object[] tagValues = null;
            bool[] tagQualities = null;
            DateTime[] tagTimeStamps = null;
            Int32 tagIndex = 0;
            Int32 numberOfTags = 0;

            System.Text.StringBuilder UpdateString = new System.Text.StringBuilder();
            double ValueDouble = 0;
            int ValueInteger = 0;
            bool ValueBoolean = false;
            string ValueString = null;
            ValueDouble = 0;
            ValueInteger = 0;
            ValueBoolean = false;
            ValueString = "";

            for (allTagValuesIndex = 0; allTagValuesIndex < numberOfTagValues; allTagValuesIndex++)
            {
                // Obtain arrays of tags and values from the tag values class previously stored in the OpcSystemsData_ValuesChangedAll event
                allTagValues = arrTagValues[allTagValuesIndex];
                tagNames = allTagValues.TagNames;
                tagValues = allTagValues.Values;
                tagQualities = allTagValues.Qualities;
                tagTimeStamps = allTagValues.TimeStamps;
                numberOfTags = tagNames.GetLength(0);
                lock (m_DataValuesHashtable.SyncRoot)
                {
                    for (tagIndex = 0; tagIndex < numberOfTags; tagIndex++)
                    {
                        try
                        {
                            UpdateString.Append(tagTimeStamps[tagIndex].ToString("HH:mm:ss.fff") + " ");
                        }
                        catch (Exception ex)
                        {
                            UpdateString.Append("Unknown Time ");
                        }
                        UpdateString.Append(tagNames[tagIndex] + " = ");

                        if (tagQualities[tagIndex]) // The Tag quality is good
                        {
                            // Store value to a hashtable so you can access it from your own routines
                            if (m_DataValuesHashtable.Contains(tagNames[tagIndex]))
                            {
                                m_DataValuesHashtable[tagNames[tagIndex]] = tagValues[tagIndex];
                            }
                            else
                            {
                                m_DataValuesHashtable.Add(tagNames[tagIndex], tagValues[tagIndex]);
                            }
                            try
                            {
                                if (object.ReferenceEquals(tagValues[tagIndex].GetType(), typeof(double)))
                                {
                                    ValueDouble = Convert.ToDouble(tagValues[tagIndex]);
                                    UpdateString.Append(ValueDouble.ToString());
                                }
                                else if (object.ReferenceEquals(tagValues[tagIndex].GetType(), typeof(int)))
                                {
                                    ValueInteger = Convert.ToInt32(tagValues[tagIndex]);
                                    UpdateString.Append(ValueInteger.ToString());
                                }
                                else if (object.ReferenceEquals(tagValues[tagIndex].GetType(), typeof(bool)))
                                {
                                    ValueBoolean = Convert.ToBoolean(tagValues[tagIndex]);
                                    UpdateString.Append(ValueBoolean.ToString());
                                }
                                else if (object.ReferenceEquals(tagValues[tagIndex].GetType(), typeof(string)))
                                {
                                    ValueString = tagValues[tagIndex].ToString();
                                    UpdateString.Append(ValueString);
                                }
                            }
                            catch (Exception ex)
                            {
                                UpdateString.Append("Error");
                            }
                        }
                        else // The Tag quality is bad
                        {
                            // Remove the value from the hashtable so you know the value is bad from your own routines
                            if (m_DataValuesHashtable.Contains(tagNames[tagIndex]))
                            {
                                m_DataValuesHashtable.Remove(tagNames[tagIndex]);
                            }
                            UpdateString.Append("Bad Quality");

                        }
                        UpdateString.Append("\r" + "\n");
                        try
                        {
                            if (!(m_Closing))
                            {
                                //if (TextBoxValues.Text.Length > (TextBoxValues.MaxLength - 100))
                                //{
                                //    TextBoxValues.Text = "";
                                //}
                                //TextBoxValues.AppendText(UpdateString.ToString());
                                UpdateString.Remove(0, UpdateString.Length);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            m_InProcessValues = false;
        }
        internal class ClassTagValues
        {
            internal string[] TagNames;
            internal object[] Values;
            internal bool[] Qualities;
            internal DateTime[] TimeStamps;

            public ClassTagValues(string[] NewTagNames, object[] NewValues, bool[] NewQualities, DateTime[] NewTimeStamps)
            {
                TagNames = NewTagNames;
                Values = NewValues;
                Qualities = NewQualities;
                TimeStamps = NewTimeStamps;
            }
        }

    }
}