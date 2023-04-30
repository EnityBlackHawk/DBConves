using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class SenderTeste
    {
        public Stream Stream { get; }
        public SenderTeste(Stream stream)
        {
            Stream = stream;
        }

        public Task Start()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(5* 1000);
                Stream.Send(this, new MessageEventArgs(nameof(SenderTeste), "Mensagem"));
            });
        }

    }
}
