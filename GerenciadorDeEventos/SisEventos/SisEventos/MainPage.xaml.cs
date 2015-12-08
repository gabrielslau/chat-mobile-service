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
using SisEventos.EventManager;
using Windows.Phone.PersonalInformation;

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
            ws.ContatoAdicionarCompleted += Ws_ContatoAdicionarCompleted;

            // Adição manual de contatos à lista de telefone do usuário
            //addPerson("Pessoa 1", "+55 84 88997766");
            //addPerson("Pessoa 2", "+55 84 88997755");
            //addPerson("Pessoa 3", "+55 84 88997744");
        }

        private void Ws_ContatoAdicionarCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ParticipandoEventos.xaml", UriKind.Relative));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            App.Phone = App.Nome = "";
            if (loginTxt.Text != string.Empty && usernameTxt.Text != string.Empty)
            {
                App.Phone = loginTxt.Text;
                App.Nome = usernameTxt.Text;

                ws.ContatoAdicionarAsync(usernameTxt.Text, loginTxt.Text, "uri");
            }
            else
                MessageBox.Show("Digite nome e numero");
        }

        private async void addPerson(string nome, string telefone)
        {
            var store = await ContactStore.CreateOrOpenAsync();

            var contact = new StoredContact(store)
            {
                DisplayName = nome
            };

            var props = await contact.GetPropertiesAsync();
            props.Add(KnownContactProperties.MobileTelephone, telefone);

            await contact.SaveAsync();
        }
    }
}