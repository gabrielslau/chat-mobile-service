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

        public void ContatoAtualizar(string nome, string numero)
        {
            // TODO
            throw new NotImplementedException();
        }

        public List<Contato> ContatoListar()
        {
            // TODO
            throw new NotImplementedException();
        }

        public void ContatoRemover(string numero)
        {
            // TODO
            throw new NotImplementedException();
        }

        //
        // Convites
        //
        public void ConviteAceitar(int evento_id, string numeroConvidado)
        {
            Convite convite = new Convite(numeroConvidado, evento_id);
            convite.aceitarConvite();
        }

        public List<Convite> ConviteListar(string numeroConvidado)
        {
            return Convite.SelectConvitesDeContato(numeroConvidado);
        }

        //
        // Eventos
        //
        public void EventoAdicionar(string nome, string descricao, DateTime data, string numeroResponsavel)
        {
            Evento evento = new Evento(nome, descricao, data, numeroResponsavel);
        }

        public void EventoAdicionarParticipante(int evento_id, string numeroParticipante)
        {
            if (Evento.Existe(evento_id))
            {
                Evento evento = Evento.Find(evento_id);
                evento.addParticipante(numeroParticipante);
            }
        }

        public void EventoAtualizar(int evento_id, string nome, string descricao, DateTime data)
        {
            if (Evento.Existe(evento_id))
            {
                Evento evento = Evento.Find(evento_id);
                evento.alterarDados(nome, descricao, data);
            }
        }

        public void EventoConvidarParticipante(int evento_id, string numeroParticipante)
        {
            if (Evento.Existe(evento_id))
            {
                Evento evento = Evento.Find(evento_id);
                evento.convidarParticipante(numeroParticipante);
            }
        }

        public void EventoDelete(int evento_id)
        {
            Evento.excluirEvento(evento_id);
        }

        public List<Evento> EventoListar()
        {
            // TODO
            throw new NotImplementedException();
        }

        public void EventoRemoverParticipante(int evento_id, string numeroParticipante)
        {
            if (Evento.Existe(evento_id))
            {
                Evento evento = Evento.Find(evento_id);
                evento.removerParticipante(numeroParticipante);
            }
        }
    }
}
