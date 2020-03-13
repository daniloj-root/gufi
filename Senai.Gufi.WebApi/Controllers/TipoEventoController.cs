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

    public class TipoEventoController : Controller
    {
        private IGeneralRepository<TipoEvento> _tipoEventoRepository { get; set; }

        public TipoEventoController()
        {
            _tipoEventoRepository = new RepositoryBase<TipoEvento>();
        }

        // GET: api/TipoEvento
        /// <summary>
        /// Lista todos os tipo de evento 
        /// </summary>
        /// <returns>Uma lista de TipoEvento</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpGet]
        [Authorize(Roles = "1, 2")]
        public IActionResult ListarTodos()
        {
            try
            {
                var listaTipoEvento = _tipoEventoRepository.ListarTodos();
                return Ok(listaTipoEvento);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/TipoEvento/5
        /// <summary>
        /// Lista um tipo de evento pelo ID
        /// </summary>
        /// <returns>Retorna um tipo de evento</returns>
        
        [HttpGet("{id}")]
        [Authorize(Roles = "1, 2")]
        public IActionResult ListarPorId(int id)
        {
            try
            {
                var tipoEscolhido = _tipoEventoRepository.ListarPorId(id);
                return Ok(tipoEscolhido);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// POST: api/TipoEvento
        /// <summary>
        /// Cadastra um tipo de evento 
        /// </summary>
        /// <returns>StatusCode 201</returns>
        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpPost]

        [Authorize(Roles = "1")]
        public IActionResult Cadastrar(TipoEvento novoTipoEvento)
        {
            try
            {
                _tipoEventoRepository.Cadastrar(novoTipoEvento);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT: api/TipoEvento/5
        /// <summary>
        /// Atualiza um tipo de evento
        /// </summary>
        /// <param name="tipoEventoAtualizado">objeto TipoEvento com um ID existente e as informações atualizadas</param>
        /// <returns>StatusCode 200</returns>
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public IActionResult Atualizar(TipoEvento tipoEventoAtualizado)
        {
            var tipoEventoEscolhido = _tipoEventoRepository.ListarPorId(tipoEventoAtualizado.IdTipoEvento);

            if (tipoEventoEscolhido == null)
            {
                return NotFound();
            }

            _tipoEventoRepository.Atualizar(tipoEventoAtualizado);
            return Ok();

        }

        // DELETE: api/TipoEvento/5
        /// <summary>
        /// Deleta um tipo de evento
        /// </summary>
        /// <param name="id">ID do TipoEvento</param>
        /// <returns>StatusCode 200</returns>
        /// 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpDelete("{id}")]

        [Authorize(Roles = "1")]
        public IActionResult Deletar(int id)
        {
            var tipoEventoEscolhido = _tipoEventoRepository.ListarPorId(id);

            if (tipoEventoEscolhido == null)
            {
                return NotFound();
            }

            try
            {
                _tipoEventoRepository.Deletar(tipoEventoEscolhido);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
