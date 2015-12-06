using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeEventosWCF.ClassesBanco
{
	class Convite
	{
		private static int idCount;
		private static List<Convite> Convites;

		static Convite()
		{
			Convites = new List<Convite>();
		}

		public static bool Existe(string numeroConvidado, int idEvento)
		{
			return Convites.Find(c => c.numeroConvidado == numeroConvidado && c.idEvento == idEvento) != null;
		}

		public static List<Convite> SelectConvitesDeContato(string numeroConvidado)
		{
			return (List<Convite>) Convites.Select(c => c.numeroConvidado == numeroConvidado);
        }

		public static Convite Find(string numeroConvidado, int idEvento)
		{
			return Convites.Find(c => c.numeroConvidado == numeroConvidado && c.idEvento == idEvento);
        }

		public int id { get; set; }
		public string numeroConvidado { get; set; }
		public int idEvento { get; set; }

		public Convite(string numeroConvidado, int idEvento)
		{
			if (Existe(numeroConvidado, idEvento))
				throw new Exception();
			if (Contato.Existe(numeroConvidado) && Evento.Existe(idEvento))
				throw new Exception();
			id = idCount++;
			this.numeroConvidado = numeroConvidado;
			this.idEvento = idEvento;
			Convites.Add(this);
		}

		public void aceitarConvite(bool aceitar = true)
		{
			if (aceitar && Evento.Existe(idEvento))
			{
				Evento.Find(idEvento).addParticipante(numeroConvidado);
				Contato.Find(numeroConvidado).addEvento(idEvento);
			}
			Convites.Remove(this);
		}
	}
}
