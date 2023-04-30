using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
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
    public class Stream
    {
        public event EventHandler<MessageEventArgs>? MessageSend;

        public void Send(object? sender, MessageEventArgs e)
        {
            MessageSend?.Invoke(sender ?? this, e);
        }
    }
}
