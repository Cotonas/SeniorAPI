using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorAPI.Interfaces;
using SeniorAPI.Models;

namespace SeniorAPI.Controllers
{
    [ApiController]
    [Route("api/v1/pessoa")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaController(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository ?? throw new ArgumentNullException(nameof(pessoaRepository));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Add(PessoaModel pessoa)
        {
             await _pessoaRepository.Add(pessoa);
             return Ok(pessoa);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var pessoa = await _pessoaRepository.Get();

            return Ok(pessoa);
        }

        [HttpGet]
        [Authorize]
        [Route("{codigo}")]
        public async Task<ActionResult> GetPessoaPorCodigo(int codigo)
        {
            var pessoa = await _pessoaRepository.GetPessoaPorCodigo(codigo);

            return Ok(pessoa);
        }

        [HttpGet]
        [Authorize]
        [Route("estado/{uf}")]
        public async Task<ActionResult> GetPessoasPorEstado(string uf)
        {
            var pessoas = await _pessoaRepository.GetPessoasPorEstado(uf);

            return Ok(pessoas);
        }

        [HttpDelete]
        [Authorize]
        [Route("codigo")]
        public async Task<ActionResult> DeletePessoa(int codigo)
        {
            var pessoa = await _pessoaRepository.GetPessoaPorCodigo(codigo);
            _pessoaRepository.Delete(pessoa);

            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Put(PessoaModel pessoa)
        {
            var _pessoa = await _pessoaRepository.GetPessoaPorCodigo(pessoa.Codigo);
            
            _pessoa.Nome = pessoa.Nome;
            _pessoa.Cpf = pessoa.Cpf;
            _pessoa.DataNascimento = pessoa.DataNascimento;
            _pessoa.Uf = pessoa.Uf;

            await _pessoaRepository.PutPessoa(_pessoa);

            return Ok(pessoa);
        }
    }
}
