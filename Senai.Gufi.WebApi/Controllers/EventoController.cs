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

    public class EventoController : Controller
    {
        private IGeneralRepository<Evento> _eventoRepository { get; set; }

        public EventoController()
        {
            _eventoRepository = new RepositoryBase<Evento>();
        }

        // GET: api/Evento
        /// <summary>
        /// Lista todos evento 
        /// </summary>
        /// <returns>Uma lista de Evento</returns>
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpGet]
        [Authorize(Roles = "1, 2")]
        public IActionResult ListarTodos()
        {
            try
            {
                var listaEvento = _eventoRepository.ListarTodos();
                return Ok(listaEvento);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Evento/5
        /// <summary>
        /// Lista evento por Id
        /// </summary>
        /// <returns>Uma um evento</returns>
        
        [HttpGet("{id}")]
        [Authorize(Roles = "1, 2")]
        public IActionResult ListarPorId(int id)
        {
            try
            {
                var listaPorID = _eventoRepository.ListarPorId(id);
                return Ok(listaPorID);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// POST: api/Evento
        /// <summary>
        /// Cadastrar um evento 
        /// </summary>
        /// <returns>StatusCode 201</returns>
        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpPost]

        [Authorize(Roles = "1")]
        public IActionResult Cadastrar(Evento novoEvento)
        {
            try
            {
                _eventoRepository.Cadastrar(novoEvento);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT: api/Evento/5
        /// <summary>
        /// Atualiza um evento
        /// </summary>
        /// <param name="eventoAtualizado">objeto Evento com um ID existente e as informações atualizadas</param>
        /// <returns>StatusCode 200</returns>
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public IActionResult Atualizar(Evento eventoAtualizado)
        {
            var eventoEscolhido = _eventoRepository.ListarPorId(eventoAtualizado.IdEvento);

            if (eventoEscolhido == null)
            {
                return NotFound();
            }

            _eventoRepository.Atualizar(eventoAtualizado);
            return Ok();

        }

        // DELETE: api/Evento/5
        /// <summary>
        /// Deleta um evento
        /// </summary>
        /// <param name="id">ID do evento</param>
        /// <returns>StatusCode 200</returns>
        /// 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpDelete("{id}")]

        [Authorize(Roles = "1")]
        public IActionResult Deletar(int id)
        {
            var eventoEscolhido = _eventoRepository.ListarPorId(id);

            if (eventoEscolhido == null)
            {
                return NotFound();
            }

            try
            {
                _eventoRepository.Deletar(eventoEscolhido);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
