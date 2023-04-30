#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRudder.Tools
{
    public class MessageEventArgs
    {
        public MessageEventArgs(string from, string key, object message)
        {
            Key = key;
            Message = message;
            From = from;
        }

        public string Key { get; }
        public object Message { get; }
        public string From { get; }


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
