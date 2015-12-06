using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeEventosWCF.ClassesBanco
{
	class Contato
	{
		private static int idCount;
		private static List<Contato> contatos;

		static Contato()
		{
			contatos = new List<Contato>();
		}

		public static Contato Find(string numero)
		{
			return contatos.Find(c => c.numero == numero);
		}

		public static void EditNome(string numero, string nome)
		{
			contatos.Find(c => c.numero == numero).nome = nome;
		}

		public static void EditUri(string numero, string uri)
		{
			contatos.Find(c => c.numero == numero).uri = uri;
		}

		public static bool Existe(string numero)
		{
			return contatos.Find(c => c.numero == numero) != null;
        }

		/*Variaveis de Contato*/

		public int id { get; private set; }
		public string nome { get; private set; }
		public string numero { get; private set; }
		public string uri { get; private set; }
		private List<Evento> eventos;

		public Contato(string nome, string numero, string uri)
		{
			if (Existe(numero))
				throw new Exception();
			id = idCount++;
			this.nome = nome;
			this.numero = numero;
			this.uri = uri;
			eventos = new List<Evento>();
			contatos.Add(this);
		}

		public void addEvento(int idEvento)
		{
			throw new NotImplementedException();
		}

		public void removerEvento(int idEvento)
		{
			throw new NotImplementedException();
		}

		public void aceitarConvite(int idConvite)
		{
			throw new NotImplementedException();
		}
	}
}
