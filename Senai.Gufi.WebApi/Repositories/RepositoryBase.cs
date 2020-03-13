using Senai.Gufi.WebApi.Domains;
using Senai.Gufi.WebApi.Interfaces;
using Senai.Gufi.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gufi.WebApi.Repositories
{
    public class RepositoryBase<TEntity> : IGeneralRepository<TEntity> where TEntity : class
    {
        public GufiContext dbo;

        public RepositoryBase()
        {
            dbo = new GufiContext();
        }

        public void Atualizar(TEntity itemAtualizado)
        {
            dbo.Set<TEntity>().Update(itemAtualizado);
            dbo.SaveChanges();
        }

        public void Cadastrar(TEntity novoItem)
        {
            dbo.Set<TEntity>().Add(novoItem);
            dbo.SaveChanges();
        }

        public void Deletar(TEntity itemEscolhido)
        {
            dbo.Set<TEntity>().Remove(itemEscolhido);
            dbo.SaveChanges();
        }

        public TEntity ListarPorId(int id)
        {
            var idEntidade = typeof(TEntity).GetProperty($"Id{typeof(TEntity)}");

            if (idEntidade != null)
                return dbo.Set<TEntity>().FirstOrDefault(x => idEntidade.GetValue(x).Equals(id));

            return null;
        }

        public IEnumerable<TEntity> ListarTodos()
        {
            return dbo.Set<TEntity>();
        }
    }
}

