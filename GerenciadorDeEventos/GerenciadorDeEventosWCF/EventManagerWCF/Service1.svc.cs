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
        private bool ContatoJaExiste(string numero)
        {
            using (EventManagerDataContext dc = new EventManagerDataContext())
            {
                return dc.Contatos.Any(c => c.Numero.Contains(numero));
            }
        }
        public void ContatoAdicionar(string nome, string numero, string uri)
        {
            // Só adiciona um novo contato se o número ainda não estiver cadastrado
            using (EventManagerDataContext dc = new EventManagerDataContext())
            {
                if (ContatoJaExiste(numero) == false)
                {
                    Contato contato = new Contato
                    {
                        Numero = numero,
                        Nome = nome,
                        Uri = uri
                    };

                    dc.Contatos.InsertOnSubmit(contato);
                    dc.SubmitChanges();
                }
            }


            //EventManagerDataContext dc = new EventManagerDataContext();
            //try
            //{
            //    Contato contatoAdicionado = (from c in dc.Contatos where c.Numero == numero select c).Single();
            //}
            //catch (Exception)
            //{
            //    Contato contato = new Contato
            //    {
            //        Numero = numero,
            //        Nome = nome,
            //        Uri = uri
            //    };

            //    dc.Contatos.InsertOnSubmit(contato);
            //    dc.SubmitChanges();
            //}
        }

        public void ContatoAtualizar(string nome, string numero, string uri)
        {
            EventManagerDataContext dc = new EventManagerDataContext();

            try
            {
                Contato contato = dc.Contatos.First(c => c.Numero == numero);
                contato.Nome = nome;
                contato.Uri = uri;
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
                          join p in dc.Participantes on e.Id equals p.IdEvento
                          join c in dc.Contatos on p.IdContato equals c.Id
                          where c.Numero == numero
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
                participante.IdEvento = evento_id;
                participante.IdContato = dc.Contatos.First(c => c.Numero == numeroConvidado).Id;
                dc.Participantes.InsertOnSubmit(participante);

                Convite convite = dc.Convites.First(c => c.IdEvento == evento_id && c.NumeroConvidado == numeroConvidado);
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
                Convite convite = dc.Convites.First(c => c.IdEvento == evento_id && c.NumeroConvidado == numeroConvidado);
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

            var convites = from c in dc.Convites where c.NumeroConvidado == numeroConvidado select c;

            if (convites.Count() == 0) return null;

            return convites.ToList();
        }

        //
        // Eventos
        //
        public void EventoAdicionar(string nome, string descricao, DateTime data, string numeroResponsavel, float latitude, float longitude)
        {
            using (EventManagerDataContext dc = new EventManagerDataContext())
            {
                try
                {
                    // Só cria o evento se o responável for encontrado no banco
                    Evento evento = new Evento
                    {
                        Nome = nome,
                        Descricao = descricao,
                        Data = data,
                        Latitude = latitude,
                        Longitude = longitude
                    };

                    dc.Eventos.InsertOnSubmit(evento);
                    dc.SubmitChanges();

                    // Adiciona o criador do evento como um dos participantes
                    Contato responsavel = (from c in dc.Contatos where c.Numero == numeroResponsavel select c).Single();
                    Participante participante = new Participante
                    {
                        IdEvento = evento.Id,
                        IdContato = responsavel.Id
                    };

                    dc.Participantes.InsertOnSubmit(participante);
                    dc.SubmitChanges();
                }
                catch(Exception)
                {
                    //enviar mensagem de numero inexistente
                }
            }
        }

        public void EventoAtualizar(int evento_id, string nome, string descricao, DateTime data, float latitude, float longitude)
        {
            EventManagerDataContext dc = new EventManagerDataContext();

            try
            {
                Evento evento = dc.Eventos.First(e => e.Id == evento_id);

                evento.Nome = nome;
                evento.Descricao = descricao;
                evento.Data = data;
                evento.Latitude = latitude;
                evento.Longitude = longitude;

                dc.Eventos.InsertOnSubmit(evento);
                dc.SubmitChanges();
            }
            catch
            {
                //enviar mensagem de numero inexistente
            }
        }

        public Evento EventoAbrir(int evento_id)
        {
            EventManagerDataContext dc = new EventManagerDataContext();

            Evento evento = dc.Eventos.First(e => e.Id == evento_id);
            return evento;
        }

        public void EventoConvidarParticipante(int evento_id, string numeroParticipante)
        {
            EventManagerDataContext dc = new EventManagerDataContext();

            try
            {
                Contato contato = dc.Contatos.First(c => c.Numero == numeroParticipante);
                Evento evento = dc.Eventos.First(e => e.Id == evento_id);
                Convite convite = new Convite();
                convite.IdEvento = evento_id;
                convite.NumeroConvidado = numeroParticipante;
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
                Evento evento = dc.Eventos.First(e => e.Id == evento_id);
                dc.Eventos.DeleteOnSubmit(evento);

                var convites = from c in dc.Convites where c.IdEvento == evento.Id select c;
                foreach (var c in convites)
                    dc.Convites.DeleteOnSubmit(c);

                var participantes = from p in dc.Participantes where p.IdEvento == evento.Id select p;
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
                Participante participante = dc.Participantes.First(p => p.IdContato == dc.Contatos.First(c => c.Numero == numeroParticipante).Id && p.IdEvento == evento_id);

                dc.Participantes.DeleteOnSubmit(participante);

                dc.SubmitChanges();
            }
            catch
            {

            }
        }
    }
}
