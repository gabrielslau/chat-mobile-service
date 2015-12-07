using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeEventosWCF.ClassesBanco
{
    [DataContract]
	public class Evento
	{
		private static int idCount;
		private static List<Evento> Eventos;

		static Evento()
		{
			Eventos = new List<Evento>();
		}

		public static Evento Find(int id)
		{
			return Eventos.Find(e => e.id == id);
		}

		public static bool Existe(int id)
		{
			return Eventos.Find(e => e.id == id) != null;
		}

		public static void excluirEvento(int idEvento)
		{
			if(Existe(idEvento))
				Eventos.Find(e => e.id == idEvento).excluirEvento();
		}

        [DataMember]
		public int id { get; private set; }
        [DataMember]
        public string nome { get; private set; }
        [DataMember]
        public string descricao { get; private set; }
        [DataMember]
        public DateTime data { get; private set; }
        [DataMember]
        public double latitude { get; private set; }
        [DataMember]
        public double longitude { get; private set; }
        [DataMember]
        public List<Contato> participantes { get; private set; }

		public Evento()
		{
			id = idCount++;
			participantes = new List<Contato>();
		}
		
		public Evento(string nome, string descricao, DateTime data, string numeroResponsavel, double latitude, double longitude)
		{
			Contato responsavel = Contato.Find(numeroResponsavel);
			if (responsavel == null)
				throw new Exception();
			id = idCount++;
			this.nome = nome;
			this.descricao = descricao;
			this.data = data;
			this.latitude = latitude;
			this.longitude = longitude;
			participantes = new List<Contato>();
			participantes.Add(responsavel);
			Eventos.Add(this);
			responsavel.addEvento(id);
		}

		public void alterarDados(string nome, string descricao, DateTime data, double latitude, double longitude)
		{
			this.nome = nome;
			this.descricao = descricao;
			this.data = data;
			this.latitude = latitude;
			this.longitude = longitude;
		}

		public void convidarParticipante(string numero)
		{
			Convite convite = new Convite(numero, this.id);
		}

		public void addParticipante(string numero)
		{
			Contato participante = Contato.Find(numero);
			if (participante == null)
				throw new Exception();
			participantes.Add(participante);
		}

		public void removerParticipante(string numero)
		{
			Contato participante = participantes.Find(p => p.numero == numero);
			if (participante != null)
			{
				participantes.Remove(participante);
				participante.removerEvento(id);
			}
		}

		public void excluirEvento()
		{
			foreach (Contato c in participantes)
				c.removerEvento(id);
			Eventos.Remove(this);
		}
	}
}
