using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeEventosWCF.ClassesBanco
{
	class Evento
	{
		private static int idCount;
		private static List<Evento> eventos;

		static Evento()
		{
			eventos = new List<Evento>();
		}

		public static Evento Find(int id)
		{
			return eventos.Find(e => e.id == id);
		}

		public static bool Existe(int id)
		{
			return eventos.Find(e => e.id == id) != null;
		}

		public static void excluirEvento(int idEvento)
		{
			if(Existe(idEvento))
				eventos.Find(e => e.id == idEvento).excluirEvento();
		}


		public int id { get; private set; }
		public string nome { get; private set; }
		public string descricao { get; private set; }
		public DateTime data { get; private set; }
		private List<Contato> participantes;

		public Evento()
		{
			id = idCount++;
			participantes = new List<Contato>();
		}
		
		public Evento(string nome, string descricao, DateTime data, string numeroResponsavel)
		{
			Contato responsavel = Contato.Find(numeroResponsavel);
			if (responsavel == null)
				throw new Exception();
			id = idCount++;
			this.nome = nome;
			this.descricao = descricao;
			this.data = data;
			participantes = new List<Contato>();
			participantes.Add(responsavel);
			eventos.Add(this);
			responsavel.addEvento(id);
		}

		public void alterarDados(string nome, string descricao, DateTime data)
		{
			this.nome = nome;
			this.descricao = descricao;
			this.data = data;
		}

		public void addParticipante(string numero)
		{
			throw new NotImplementedException();
		}

		public void convidarParticipante(string numero)
		{
			throw new NotImplementedException();
		}

		public void removerParticipante(string numero)
		{
			throw new NotImplementedException();
		}

		public void excluirEvento()
		{
			throw new NotImplementedException();
		}
	}
}
