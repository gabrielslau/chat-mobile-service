using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SisEventos.Resources;
using SisEventos.GerenciadorDeEventos;

namespace SisEventos
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Service1Client ws;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            ws = new Service1Client();
			ws.ContatoAdicionarCompleted += Ws_ContatoAdicionarCompleted; ;
		}

		private void Ws_ContatoAdicionarCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			ws.EventoAdicionarAsync("titulo 1", "descricao 1", DateTime.Now, App.Phone, 1.0, 1.0);
			ws.EventoAdicionarAsync("titulo 2", "descricao 2", DateTime.Now, App.Phone, 1.0, 1.0);
			NavigationService.Navigate(new Uri("/ParticipandoEventos.xaml", UriKind.Relative));
		}

		private void button_Click(object sender, RoutedEventArgs e)
        {
			App.Phone = App.Nome = "";
			if (loginTxt.Text != string.Empty && usernameTxt.Text != string.Empty)
			{
				App.Phone = loginTxt.Text;
				App.Nome = usernameTxt.Text;

				ws.ContatoAdicionarAsync(loginTxt.Text, usernameTxt.Text, "uri");
			}
			else
				MessageBox.Show("Digite nome e numero");
        }
    }
}