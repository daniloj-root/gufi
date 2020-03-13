using Senai.Gufi.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gufi.WebApi.Interfaces
{
    interface IUsuarioRepository : IGeneralRepository<Usuario>
    {
        Usuario ListarPorEmailSenha(string email, string senha);
    }
}
