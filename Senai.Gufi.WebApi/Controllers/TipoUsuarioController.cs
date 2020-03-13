using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Gufi.WebApi.Domains;
using Senai.Gufi.WebApi.Interfaces;
using Senai.Gufi.WebApi.Repositories;

namespace Senai.Gufi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]

    [ApiController]

    public class TipoUsuarioController : Controller
    {
        private IGeneralRepository<TipoUsuario> _tipoUsuarioRepository { get; set; }

        public TipoUsuarioController()
        {
            _tipoUsuarioRepository = new RepositoryBase<TipoUsuario>();
        }

        // GET: api/TipoUsuario
        /// <summary>
        /// Lista todos tipo de usuário 
        /// </summary>
        /// <returns>Uma lista de TipoUsuario</returns>
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpGet]
        [Authorize(Roles = "1, 2")]
        public IActionResult ListarTodos()
        {
            try
            {
                var listaTipoUsuario = _tipoUsuarioRepository.ListarTodos();
                return Ok(listaTipoUsuario);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/TipoUsuario/5
        /// <summary>
        /// Lista tipo de usuário por ID
        /// </summary>
        /// <returns>Um tipo de usuário</returns>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpGet("{id}")]
        [Authorize(Roles = "1, 2")]
        public IActionResult ListarPorId(int id)
        {
            try
            {
                var listaPorID = _tipoUsuarioRepository.ListarPorId(id);
                return Ok(listaPorID);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// POST: api/TipoUsuario
        /// <summary>
        /// Cadastrar um tipo de usuário 
        /// </summary>
        /// <returns>StatusCode 201</returns>
        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpPost]

        [Authorize(Roles = "1")]
        public IActionResult Cadastrar(TipoUsuario novoTipoUsuario)
        {
            try
            {
                _tipoUsuarioRepository.Cadastrar(novoTipoUsuario);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT: api/TipoUsuario/5
        /// <summary>
        /// Atualiza um tipo de usuário
        /// </summary>
        /// <param name="tipoUsuarioAtualizado">objeto TipoUsuario com um ID existente e as informações atualizadas</param>
        /// <returns>StatusCode 200</returns>
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public IActionResult Atualizar(TipoUsuario tipoUsuarioAtualizado)
        {
            var tipoUsuarioEscolhido = _tipoUsuarioRepository.ListarPorId(tipoUsuarioAtualizado.IdTipoUsuario);

            if (tipoUsuarioEscolhido == null)
            {
                return NotFound();
            }

            _tipoUsuarioRepository.Atualizar(tipoUsuarioAtualizado);
            return Ok();

        }

        // DELETE: api/TipoUsuario/5
        /// <summary>
        /// Deleta um tipoUsuario
        /// </summary>
        /// <param name="id">ID do tipoUsuario</param>
        /// <returns>StatusCode 200</returns>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpDelete("{id}")]

        [Authorize(Roles = "1")]
        public IActionResult Deletar(int id)
        {
            var tipoUsuarioEscolhido = _tipoUsuarioRepository.ListarPorId(id);

            if (tipoUsuarioEscolhido == null)
            {
                return NotFound();
            }

            try
            {
                _tipoUsuarioRepository.Deletar(tipoUsuarioEscolhido);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
