using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRudder.Tools
{
    public class MessageEventArgs
    {
        public MessageEventArgs(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; set; }
        public string Message { get; set; }


    }
    public class MessageStream
    {
        public event EventHandler<MessageEventArgs>? MessageSend;

        public void Send(object? sender, MessageEventArgs e)
        {
            MessageSend?.Invoke(sender ?? this, e);
        }
    }
}
