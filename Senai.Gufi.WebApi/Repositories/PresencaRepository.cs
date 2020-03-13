using Senai.Gufi.WebApi.Domains;
using Senai.Gufi.WebApi.Interfaces;
using Senai.Gufi.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gufi.WebApi.Repositories
{
    public class PresencaRepository : RepositoryBase<Presenca>, IPresencaRepository
    {
        public IEnumerable<Presenca> ListarMinhas(Usuario usuarioAtual)
        {
            return dbo.Presenca.Where(x => x.IdUsuario == usuarioAtual.IdUsuario);
        }

        public void Inscrever(Presenca presencaInscrever)
        {
            dbo.Presenca.Add(presencaInscrever);

            dbo.SaveChanges();
        }

        public void Convidar(Presenca presencaConvidando)
        {
            dbo.Presenca.Add(presencaConvidando);
            dbo.SaveChanges();
        }

        public void Confirmar(Presenca presenca)
        {
            presenca.Situacao = "Confirmado";
            dbo.Presenca.Update(presenca);

            dbo.SaveChanges();
        }

        public void Recusar(Presenca presenca)
        {
            presenca.Situacao = "Recusado";
            dbo.Presenca.Update(presenca);

            dbo.SaveChanges();
        }
    }
}
