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
    public partial class DescricaoEvento : PhoneApplicationPage
    {
        private GerenciadorDeEventos.Service1Client ws;

        public DescricaoEvento()
        {
            InitializeComponent();
            ws = new GerenciadorDeEventos.Service1Client();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            // TODO: implementar no serviço função de pegar dados do evento
            tituloEvento.Text = "título do evento";
            textBlock1.Text = "descrição do evento";
            textBlock3.Text = "local do evento";
        }
    }
}