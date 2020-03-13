using Senai.Gufi.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gufi.WebApi.Interfaces
{
    interface IPresencaRepository : IGeneralRepository<Presenca>
    {
        IEnumerable<Presenca> ListarMinhas(Usuario usuarioAtual);
        void Convidar(Presenca presencaConvidando);
        void Confirmar(Presenca idPresenca);
        void Recusar(Presenca idPresenca);
    }
}
