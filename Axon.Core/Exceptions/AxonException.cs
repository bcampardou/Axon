using System;
using System.Collections.Generic;
using System.Text;

namespace Axon.Core.Exceptions
{
    public class AxonException : Exception
    {
        protected string _caller;
        public IEnumerable<string> DetailsMessages { get; set; }
        public override string StackTrace => !string.IsNullOrEmpty(_caller) ? _caller : "BUSINESS_RULES";
        public AxonException() : base() { }
        public AxonException(string message) : base(message) { }
        public AxonException(string message, IEnumerable<string> details) : base(message) { DetailsMessages = details; }
        public AxonException(string message, string caller) : base(message) { _caller = caller; }
        public AxonException(string message, string caller, IEnumerable<string> details) : base(message) { _caller = caller; DetailsMessages = details; }
    }
}
