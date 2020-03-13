using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
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

    public class PresencaController : ControllerBase
    {
        private IPresencaRepository _presencaRepository { get; set; }
        private IGeneralRepository<Usuario> _usuarioRepository { get; set; }

        public PresencaController()
        {
            _presencaRepository = new PresencaRepository();
            _usuarioRepository = new RepositoryBase<Usuario>();
        }

        // GET: api/Presenca
        /// <summary>
        /// Lista todas as presenças 
        /// </summary>
        /// <returns>Uma lista de Presenca</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpGet]
        [Authorize(Roles = "1, 2")]
        public IActionResult ListarTodos()
        {
            try
            {
                var listaPresenca = _presencaRepository.ListarTodos();
                return Ok(listaPresenca);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Presenca/5
        /// <summary>
        /// Lista presença por ID
        /// </summary>
        /// <returns>Uma presenca</returns>
        /// <summary>
        /// Lista as presenças do usuário logado
        /// </summary>
        /// <returns>Lista de presenças e convites</returns>

        [HttpGet("meusconvites")]
        [Authorize(Roles = "2")]
        public IActionResult ListarMinhas()
        {
            var usuarioAtual = RetornarUsuarioLogado();

            try
            {
                var listaConvites = _presencaRepository.ListarMinhas(usuarioAtual);
                return Ok(listaConvites);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// POST: api/Presenca/
        /// <summary>
        /// Cadastra um convite para qualquer usuário
        /// </summary>
        /// <param name="novoPresenca"></param>
        /// <returns></returns>
        /// 

        [HttpPost]
        [Authorize(Roles = "1")]
        public IActionResult Cadastrar(Presenca novoPresenca)
        {

            var usuarioEscolhido = _usuarioRepository.ListarPorId(Convert.ToInt32(novoPresenca.IdUsuario));

            if (usuarioEscolhido == null)
                return NotFound();

            try
            {
                _presencaRepository.Cadastrar(novoPresenca);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT: api/Presenca/5
        /// <summary>
        /// Atualiza uma presença
        /// </sumamary>
        /// <param name="presencaAtualizado">objeto Presenca com uma ID existente e as informações atualizadas</param>
        /// <returns>StatusCode 200</returns>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public IActionResult Atualizar(Presenca presencaAtualizado)
        {
            var presencaEscolhido = _presencaRepository.ListarPorId(presencaAtualizado.IdPresenca);

            if (presencaEscolhido == null)
                return NotFound();

            _presencaRepository.Atualizar(presencaAtualizado);
            return Ok();

        }

        // DELETE: api/Presenca/5
        /// <sumamary>
        /// Deleta uma presença
        /// </summary>
        /// <param name="id">ID da presença</param>
        /// <returns>StatusCode 200</returns>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpDelete("{id}")]

        [Authorize(Roles = "1")]
        public IActionResult Deletar(int id)
        {
            var presencaEscolhido = _presencaRepository.ListarPorId(id);

            if (presencaEscolhido == null)
                return NotFound();

            try
            {
                _presencaRepository.Deletar(presencaEscolhido);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// POST: api/Presenca/confirmar/id
        /// <summary>
        /// Aceitar um convite
        /// </summary>
        /// <param name="id">ID do convite</param>
        /// <returns></returns>

        [HttpPost("confirmar/{id}")]
        [Authorize(Roles = "1")]
        public IActionResult Confirmar(int id)
        {
            var usuarioAtual = RetornarUsuarioLogado();

            var presencaEscolhida = _presencaRepository.ListarPorId(id);

            if (presencaEscolhida == null)
                return NotFound();

            if (presencaEscolhida.IdUsuario != usuarioAtual.IdUsuario)
                return BadRequest();

            try
            {
                _presencaRepository.Atualizar(presencaEscolhida);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// POST: api/Presenca/recusar/5
        /// <summary>
        /// Recusar um convite
        /// </summary>
        /// <param name="id">ID do convite</param>
        /// <returns>StatusCode 200</returns>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpPost("convites/recusar/{id}")]
        [Authorize(Roles = "1")]
        public IActionResult Recusar(int id)
        {
            var usuarioAtual = RetornarUsuarioLogado();

            var presencaEscolhida = _presencaRepository.ListarPorId(id);

            if (presencaEscolhida == null)
                return NotFound();

            if (presencaEscolhida.IdUsuario != usuarioAtual.IdUsuario)
                return BadRequest();

            try
            {
                _presencaRepository.Recusar(presencaEscolhida);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Busca um usuário pelo ID
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>Um usuário</returns>

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpGet("{id}")]
        [Authorize(Roles = "1")]
        public IActionResult ListarPorId(int id)
        {
            try
            {
                var listaPorID = _presencaRepository.ListarPorId(id);
                return Ok(listaPorID);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        
        /// <summary>
        /// Retorna o usuário logado
        /// </summary>
        /// <returns>Usuário logado</returns>
        public Usuario RetornarUsuarioLogado()
        {
            if (!(HttpContext.User.Identity is ClaimsIdentity identity) || identity.IsAuthenticated)
            {
                return null;
            }

            IEnumerable<Claim> claims = identity.Claims;

            var id = claims
                .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti);

            return _usuarioRepository.ListarPorId(Convert.ToInt32(id));
        }
    }
}

