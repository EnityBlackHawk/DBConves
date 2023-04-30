using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace DBRudder.ViewModel
{
    public class MessageTesteViewModel : NewObservableObject
    {
		private string _message;

		public string Message
		{
			get { return _message; }
			set { _message = value; OnPropertyChanged(); }
		}

		public ButtonCommand Command { get; set; }

        private MainWindowViewModel MainWindowViewModel { get; set; }

        public MessageTesteViewModel()
        {
            Command = new ButtonCommand(StartTeste);
        }

        public void StartTeste()
        {
            MainWindowViewModel = App.Get(MainWindowViewModel);
            MainWindowViewModel.CurrentView = new View.Teste();
            App.GetStream().MessageSend += RecevedMessage;
        }

        private void RecevedMessage(object sender, Tools.MessageEventArgs e)
        {
            if (e.Key == nameof(View.Teste))
            {
                Message = e.Message;
            }
        }
    }
}
