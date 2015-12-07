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
			EventManagerDataContext dc = new EventManagerDataContext();

			Contato contato = new Contato();
			contato.numero = numero;
			contato.nome = nome;
			contato.uri = uri;

			try
			{
				dc.Contatos.InsertOnSubmit(contato);
				dc.SubmitChanges();
			}
			catch
			{
				//enviar mensagem de dados invalidos
			}
        }

        public void ContatoAtualizar(string nome, string numero, string uri)
        {
			EventManagerDataContext dc = new EventManagerDataContext();

			try
			{
				Contato contato = dc.Contatos.First(c => c.numero == numero);
				contato.nome = nome;
				contato.uri = uri;
				dc.SubmitChanges();
			}
			catch
			{
				//enviar mensagem de numero inexistente
			}
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
			EventManagerDataContext dc = new EventManagerDataContext();

			try
			{
				Participante participante = new Participante();
				participante.idEvento = evento_id;
				participante.idContato = dc.Contatos.First(c => c.numero == numeroConvidado).id;
				dc.Participantes.InsertOnSubmit(participante);

				Convite convite = dc.Convites.First(c => c.idEvento == evento_id && c.numeroConvidado == numeroConvidado);
				dc.Convites.DeleteOnSubmit(convite);

				dc.SubmitChanges();
			}
			catch
			{
				//enviar mensagem de numero ou evento inexistente
			}
		}

        public void ConviteRecusar(int evento_id, string numeroConvidado)
		{
			EventManagerDataContext dc = new EventManagerDataContext();

			try
			{
				Convite convite = dc.Convites.First(c => c.idEvento == evento_id && c.numeroConvidado == numeroConvidado);
				dc.Convites.DeleteOnSubmit(convite);

				dc.SubmitChanges();
			}
			catch
			{
				//enviar mensagem de numero ou evento inexistente
			}
		}

        public List<Convite> ConviteListar(string numeroConvidado)
        {
            EventManagerDataContext dc = new EventManagerDataContext();

			var convites = from c in dc.Convites where c.numeroConvidado == numeroConvidado select c;

            if (convites.Count() == 0) return null;

            return convites.ToList();
        }

        //
        // Eventos
        //
        public void EventoAdicionar(string nome, string descricao, DateTime data, string numeroResponsavel, long latitude, long longitude)
        {
			EventManagerDataContext dc = new EventManagerDataContext();

			try
			{
				Evento evento = new Evento();
				evento.nome = nome;
				evento.descricao = descricao;
				evento.data = data;
				evento.latitude = latitude;
				evento.longitude = longitude;
				dc.SubmitChanges();
				evento = dc.Eventos.First(e => e.nome==nome && e.descricao==descricao && e.data==data && e.latitude==latitude && e.longitude==longitude);

				Contato responsavel = dc.Contatos.First(c => c.numero == numeroResponsavel);
				Participante participante = new Participante();
				participante.idEvento = evento.id;
				participante.idContato = dc.Contatos.First(c => c.numero == numeroResponsavel).id;
				dc.Participantes.InsertOnSubmit(participante);

				dc.SubmitChanges();
			}
			catch
			{
				//enviar mensagem de numero inexistente
			}
		}

        public void EventoAtualizar(int evento_id, string nome, string descricao, DateTime data, long latitude, long longitude)
        {
			EventManagerDataContext dc = new EventManagerDataContext();

			try
			{
				Evento evento = dc.Eventos.First(e => e.id == evento_id);

				evento.nome = nome;
				evento.descricao = descricao;
				evento.data = data;
				evento.latitude = latitude;
				evento.longitude = longitude;
				dc.SubmitChanges();
			}
			catch
			{
				//enviar mensagem de numero inexistente
			}
		}

        public void EventoConvidarParticipante(int evento_id, string numeroParticipante)
		{
			EventManagerDataContext dc = new EventManagerDataContext();

			try
			{
				Contato contato = dc.Contatos.First(c => c.numero == numeroParticipante);
				Evento evento = dc.Eventos.First(e => e.id == evento_id);
				Convite convite = new Convite();
				convite.idEvento = evento_id;
				convite.numeroConvidado = numeroParticipante;
				dc.Convites.InsertOnSubmit(convite);
				dc.SubmitChanges();
			}
			catch
			{
				//enviar mensagem de numero ou evento inexistente
			}
		}

        public void EventoDelete(int evento_id)
		{
			EventManagerDataContext dc = new EventManagerDataContext();

			try
			{
				Evento evento = dc.Eventos.First(e => e.id == evento_id);
				dc.Eventos.DeleteOnSubmit(evento);

				var convites = from c in dc.Convites where c.idEvento == evento.id select c;
				foreach (var c in convites)
					dc.Convites.DeleteOnSubmit(c);

				var participantes = from p in dc.Participantes where p.idEvento == evento.id select p;
				foreach (var p in participantes)
					dc.Participantes.DeleteOnSubmit(p);

				dc.SubmitChanges();
			}
			catch
			{
				//enviar mensagem de evento inexistente
			}
		}

        public void EventoRemoverParticipante(int evento_id, string numeroParticipante)
		{
			EventManagerDataContext dc = new EventManagerDataContext();

			try
			{
				Participante participante = dc.Participantes.First(p => p.idContato == dc.Contatos.First(c => c.numero == numeroParticipante).id && p.idEvento == evento_id);

				dc.Participantes.DeleteOnSubmit(participante);

				dc.SubmitChanges();
			}
			catch
			{

			}
		}
    }
}
