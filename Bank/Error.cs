using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class Error
    {
        public event EventHandler<ErrorEventArgs>? ErrorAppeared;
        public void OnErrorAppeared(ErrorEventArgs e)
        {
            EventHandler<ErrorEventArgs> handler = ErrorAppeared!;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
    internal class ErrorEventArgs : EventArgs
    {
        public string? Message { get; set; }
        public DateTime ErrorTime { get; set; }
    }
}
