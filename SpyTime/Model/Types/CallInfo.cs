using System;
using System.Collections.Generic;
using System.Text;

namespace SpyTime.Model.Types
{
    public interface ICallInfo
    {
        string CallerName { get; set; }
        int CallDuratation { get; set; }
        string DialedNumber { get; set; }
        CallType CallType { get; set; }
        DateTime CallDateTime { get; set; }
    }
    public class CallInfo : ICallInfo
    {
        public string CallerName { get; set; }
        public int CallDuratation { get; set; }
        public string DialedNumber { get; set; }
        public CallType CallType { get; set; }
        public DateTime CallDateTime { get; set; }
    }

    public enum CallType
    {
        Incoming,
        Outgoing,
        Missed
    }
}
