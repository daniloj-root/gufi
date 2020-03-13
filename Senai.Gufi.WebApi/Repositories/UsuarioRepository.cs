using Senai.Gufi.WebApi.Domains;
using Senai.Gufi.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gufi.WebApi.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public Usuario ListarPorEmailSenha(string email, string senha)
        {
            return dbo.Usuario.FirstOrDefault(x => x.Email == email && x.Senha == senha);
        }
    }
}
