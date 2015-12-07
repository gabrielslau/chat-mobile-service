using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace SisEventos
{
    public partial class ParticipandoEventos : PhoneApplicationPage
    {
        private GerenciadorDeEventos.Service1Client ws;

        public ParticipandoEventos()
        {
            InitializeComponent();
            ws = new GerenciadorDeEventos.Service1Client();
            ws.ContatoEventosCompleted += ContatoEventosCompleted;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ws.ContatoEventosAsync(App.Phone);
        }

        private void ContatoEventosCompleted(object sender, GerenciadorDeEventos.ContatoEventosCompletedEventArgs e) {
            listBox1.ItemsSource = e.Result;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/CriarEvento.xaml", UriKind.Relative));
        }
    }
}