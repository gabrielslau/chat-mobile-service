using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EventManagerWCF
{
    public class Service1 : IService1
    {
        //
        // Contatos
        //
        public void ContatoAdicionar(string nome, string numero, string uri)
        {
            // TODO
        }

        public void ContatoAtualizar(string nome, string numero, string uri)
        {
            // TODO
        }

        public List<Evento> ContatoEventos(string numero)
        {
            EventManagerDataContext dc = new EventManagerDataContext();

            var eventos = from e in dc.Eventos
                          join p in dc.Participantes on e.id equals p.idEvento
                          join c in dc.Contatos on p.idContato equals c.id
                          where c.numero == numero
                          select e;

            if (eventos.Count() == 0) return null;

            return eventos.ToList();
        }

        public List<Contato> ContatoListar()
        {
            EventManagerDataContext dc = new EventManagerDataContext();

            var contatos = from c in dc.Contatos
                           select c;

            if (contatos.Count() == 0) return null;

            return contatos.ToList();
        }

        //
        // Convites
        //
        public void ConviteAceitar(int evento_id, string numeroConvidado)
        {
            // TODO
        }

        public void ConviteRecusar(int evento_id, string numeroConvidado)
        {
            // TODO
        }

        public List<Evento> ConviteListar(string numeroConvidado)
        {
            EventManagerDataContext dc = new EventManagerDataContext();

            var eventos = from e in dc.Eventos
                          join c in dc.Convites on e.id equals c.idEvento 
                          where c.numeroConvidado == numeroConvidado
                          select e;

            if (eventos.Count() == 0) return null;

            return eventos.ToList();
        }

        //
        // Eventos
        //
        public void EventoAdicionar(string nome, string descricao, DateTime data, string numeroResponsavel, double latitude, double longitude)
        {
            // TODO
        }

        public void EventoAtualizar(int evento_id, string nome, string descricao, DateTime data, double latitude, double longitude)
        {
            // TODO
        }

        public void EventoConvidarParticipante(int evento_id, string numeroParticipante)
        {
            // TODO
        }

        public void EventoDelete(int evento_id)
        {
            // TODO
        }

        public void EventoRemoverParticipante(int evento_id, string numeroParticipante)
        {
            // TODO
        }
    }
}
