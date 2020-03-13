using Senai.Gufi.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gufi.WebApi.Interfaces
{
    interface IGeneralRepository<T>
    {
        IEnumerable<T> ListarTodos();
        T ListarPorId(int id);
        void Cadastrar(T novoItem);
        void Atualizar(T itemAtualizado);
        void Deletar(T itemEscolhido);
    }
}