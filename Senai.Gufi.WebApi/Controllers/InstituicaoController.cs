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

    public class InstituicaoController : Controller
    {
        private IGeneralRepository<Instituicao> _instituicaoRepository { get; set; }

        public InstituicaoController()
        {
            _instituicaoRepository = new RepositoryBase<Instituicao>();
        }

        // GET: api/Instituicao
        /// <summary>
        /// Lista todos instituicao 
        /// </summary>
        /// <returns>Uma lista de Instituicao</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpGet]
        [Authorize(Roles = "1, 2")]
        public IActionResult ListarTodos()
        {
            try
            {
                var listaInstituicao = _instituicaoRepository.ListarTodos();
                return Ok(listaInstituicao);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Instituicao/5
        /// <summary>
        /// Lista instituicao por Id
        /// </summary>
        /// <returns>Uma um instituicao</returns>
        
        [HttpGet("{id}")]
        [Authorize(Roles = "1, 2")]
        public IActionResult ListarPorId(int id)
        {
            try
            {
                var listaPorID = _instituicaoRepository.ListarPorId(id);
                return Ok(listaPorID);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// POST: api/Instituicao
        /// <summary>
        /// Cadastrar um instituicao 
        /// </summary>
        /// <returns>StatusCode 201</returns>
        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpPost]

        [Authorize(Roles = "1")]
        public IActionResult Cadastrar(Instituicao novoInstituicao)
        {
            try
            {
                _instituicaoRepository.Cadastrar(novoInstituicao);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT: api/Instituicao/5
        /// <summary>
        /// Atualiza um instituicao
        /// </summary>
        /// <param name="instituicaoAtualizado">objeto Instituicao com um ID existente e as informações atualizadas</param>
        /// <returns>StatusCode 200</returns>
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public IActionResult Atualizar(Instituicao instituicaoAtualizado)
        {
            var instituicaoEscolhido = _instituicaoRepository.ListarPorId(instituicaoAtualizado.IdInstituicao);

            if (instituicaoEscolhido == null)
            {
                return NotFound();
            }

            _instituicaoRepository.Atualizar(instituicaoAtualizado);
            return Ok();

        }

        // DELETE: api/Instituicao/5
        /// <summary>
        /// Deleta um instituicao
        /// </summary>
        /// <param name="id">ID do instituicao</param>
        /// <returns>StatusCode 200</returns>
        /// 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpDelete("{id}")]

        [Authorize(Roles = "1")]
        public IActionResult Deletar(int id)
        {
            var instituicaoEscolhido = _instituicaoRepository.ListarPorId(id);

            if (instituicaoEscolhido == null)
            {
                return NotFound();
            }

            try
            {
                _instituicaoRepository.Deletar(instituicaoEscolhido);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
