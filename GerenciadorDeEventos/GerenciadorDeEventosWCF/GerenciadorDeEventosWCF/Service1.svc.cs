using GerenciadorDeEventosWCF.ClassesBanco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GerenciadorDeEventosWCF
{
    public class Service1 : IService1
    {
        //
        // Contatos
        //
        public void ContatoAdicionar(string nome, string numero, string uri)
        {
            Contato contato = new Contato(nome, numero, uri);
        }

        public void ContatoAtualizar(string nome, string numero, string uri)
        {
			Contato contato = Contato.Find(numero);
			if (contato != null)
			{
				contato.atualizarDados(nome, uri);
			}
			else
			{
				//Enviar mensagem de erro
			}
		}

		public List<Evento> ContatoEventos(string numero)
		{
			Contato contato = Contato.Find(numero);
			if (contato != null)
			{
				return contato.eventos;
			}
			else
			{
				return new List<Evento>();
			}
		}

		public List<Contato> ContatoListar()
		{
			return Contato.All();
		}

		//
		// Convites
		//
		public void ConviteAceitar(int evento_id, string numeroConvidado)
		{
			Convite convite = Convite.Find(numeroConvidado, evento_id);
			if (convite != null)
				convite.aceitarConvite();
			else
			{
				//Enviar mensagem de erro
			}
		}

		public void ConviteRecusar(int evento_id, string numeroConvidado)
		{
			Convite convite = Convite.Find(numeroConvidado, evento_id);
			if (convite != null)
				convite.aceitarConvite(false);
			else
			{
				//Enviar mensagem de erro
			}
		}

		public List<Convite> ConviteListar(string numeroConvidado)
        {
            return Convite.SelectConvitesDeContato(numeroConvidado);
        }

        //
        // Eventos
        //
        public void EventoAdicionar(string nome, string descricao, DateTime data, string numeroResponsavel, double latitude, double longitude)
        {
            Evento evento = new Evento(nome, descricao, data, numeroResponsavel, latitude, longitude);
        }

        public void EventoAtualizar(int evento_id, string nome, string descricao, DateTime data, double latitude, double longitude)
		{
			Evento evento = Evento.Find(evento_id);
			if (evento != null)
                evento.alterarDados(nome, descricao, data, latitude, longitude);
			else
			{
				//Enviar mensagem de erro
			}
		}

        public void EventoConvidarParticipante(int evento_id, string numeroParticipante)
		{
			Evento evento = Evento.Find(evento_id);
			if (evento != null)
				evento.convidarParticipante(numeroParticipante);
			else
			{
				//Enviar mensagem de erro
			}
		}

        public void EventoDelete(int evento_id)
        {
            Evento.excluirEvento(evento_id);
        }

        public void EventoRemoverParticipante(int evento_id, string numeroParticipante)
		{
			Evento evento = Evento.Find(evento_id);
			if (evento != null)
                evento.removerParticipante(numeroParticipante);
			else
			{
				//Enviar mensagem de erro
			}
		}
    }
}
