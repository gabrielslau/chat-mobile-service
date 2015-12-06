using GerenciadorDeEventosWCF.ClassesBanco;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GerenciadorDeEventosWCF
{
    [ServiceContract]
    public interface IService1
    {
        // Evento
        [OperationContract]
        List<Evento> EventoListar();
        [OperationContract]
        void EventoAdicionar(string nome, string descricao, DateTime data, string numero_responsavel);
        [OperationContract]
        void EventoAtualizar(int evento_id, string nome, string descricao, DateTime data);
        [OperationContract]
        void EventoDelete(int evento_id);
        [OperationContract]
        void EventoAdicionarParticipante(int evento_id, string numero_participante);
        [OperationContract]
        void EventoConvidarParticipante(int evento_id, string numero_participante);
        [OperationContract]
        void EventoRemoverParticipante(int evento_id, string numero_participante);

        // Contato
        [OperationContract]
        List<Contato> ContatoListar();
        [OperationContract]
        void ContatoAdicionar(string nome, string numero, string uri);
        [OperationContract]
        void ContatoAtualizar(string nome, string numero);
        [OperationContract]
        void ContatoRemover(string numero);

        // Convite
        [OperationContract]
        List<Convite> ConviteListar(string numeroConvidado);
        [OperationContract]
        void ConviteAceitar(int evento_id, string numeroConvidado);
    }
}
