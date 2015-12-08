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
		[OperationContract]
		List<Contato> ContatoListar();
		// Evento
		[OperationContract]
        void EventoAdicionar(string nome, string descricao, DateTime data, string numero_responsavel, double latitude, double longitude);
		[OperationContract]
		void EventoAtualizar(int evento_id, string nome, string descricao, DateTime data, double latitude, double longitude);
		[OperationContract]
		Evento EventoAbrir(int evento_id);
		[OperationContract]
		void EventoDelete(int evento_id);
        [OperationContract]
        void EventoConvidarParticipante(int evento_id, string numero_participante);
        [OperationContract]
        void EventoRemoverParticipante(int evento_id, string numero_participante);

        // Contato
        [OperationContract]
        void ContatoAdicionar(string nome, string numero, string uri);
		[OperationContract]
		void ContatoAtualizar(string nome, string numero, string uri);
		[OperationContract]
		List<Evento> ContatoEventos(string numero);

		// Convite
		[OperationContract]
        List<Convite> ConviteListar(string numeroContato);
        [OperationContract]
        void ConviteAceitar(int evento_id, string numeroContato);
		[OperationContract]
		void ConviteRecusar(int evento_id, string numeroContato);
	}
}
