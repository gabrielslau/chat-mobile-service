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
		private static List<Convite> convites;

		static Convite()
		{
			convites = new List<Convite>();
		}

		public static bool Existe(string numeroConvidado, int idEvento)
		{
			return convites.Find(c => c.numeroConvidado == numeroConvidado && c.idEvento == idEvento) != null;
		}

		public int id { get; set; }
		public string numeroConvidado { get; set; }
		public int idEvento { get; set; }

		public Convite(string numeroConvidado, int idEvento)
		{
			if (Existe(numeroConvidado, idEvento))
				throw new Exception();
			if (Contato.Possui(numeroConvidado) && Evento.Existe(idEvento))
				throw new Exception();
			id = idCount++;
			this.numeroConvidado = numeroConvidado;
			this.idEvento = idEvento;
			convites.Add(this);
		}

		public void aceitarConvite(bool aceitar = true)
		{
			if (aceitar && Evento.Existe(idEvento))
			{
				Evento.Find(idEvento).addParticipante(numeroConvidado);
				Contato.Find(numeroConvidado).addEvento(idEvento);
			}
			convites.Remove(this);
		}
	}
}
