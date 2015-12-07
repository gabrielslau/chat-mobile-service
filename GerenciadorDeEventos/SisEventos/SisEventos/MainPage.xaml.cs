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

namespace SisEventos
{
    public partial class MainPage : PhoneApplicationPage
    {
        private GerenciadorDeEventos.Service1Client ws;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            ws = new GerenciadorDeEventos.Service1Client();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            App.Phone = loginTxt.Text;
            App.Nome = usernameTxt.Text;

            // Teste para injetar automaticamente alguns eventos para o usuario
            ws.EventoAdicionarAsync("titulo 1", "descricao 1", DateTime.Now, App.Phone, 1.0, 1.0);
            ws.EventoAdicionarAsync("titulo 2", "descricao 2", DateTime.Now, App.Phone, 1.0, 1.0);

            NavigationService.Navigate(new Uri("/ParticipandoEventos.xaml", UriKind.Relative));
        }
    }
}