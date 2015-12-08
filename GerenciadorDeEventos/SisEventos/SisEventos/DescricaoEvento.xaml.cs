using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SisEventos.GerenciadorDeEventos;

namespace SisEventos
{
	public partial class DescricaoEvento : PhoneApplicationPage
	{
		private Service1Client ws;

		public DescricaoEvento()
		{
			InitializeComponent();
			ws = new Service1Client();
			ws.EventoAbrirCompleted += Ws_EventoAbrirCompleted;
		}

		private void Ws_EventoAbrirCompleted(object sender, EventoAbrirCompletedEventArgs e)
		{
			tituloEvento.Text = e.Result.nome;
			textBlock1.Text = e.Result.descricao;
			textBlock3.Text = "local do evento";
			listBox1.ItemsSource = e.Result.participantes;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			ws.EventoAbrirAsync(Convert.ToInt32(NavigationContext.QueryString["id"]));
		}

		private void EventoConvidar(object sender, RoutedEventArgs e)
		{
			string id = ((TextBlock)((StackPanel)sender).FindName("ID")).Text;
			string uri = string.Format("/DescricaoEvento.xaml?id={0}", id);
			NavigationService.Navigate(new Uri(uri, UriKind.Relative));
		}
	}
}