using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorAPI.Models;
using SeniorAPI.ViewModel;

namespace SeniorAPI.Controllers
{
    [ApiController]
    [Route("api/v1/pessoa")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ILogger<PessoaController> _logger;

        public PessoaController(IPessoaRepository pessoaRepository, ILogger<PessoaController> logger)
        {
            _pessoaRepository = pessoaRepository ?? throw new ArgumentNullException(nameof(pessoaRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public IActionResult Add(PessoaViewModel pessoaView)
        {
            var pessoa = new PessoaModel(pessoaView.Codigo, pessoaView.Nome, pessoaView.Cpf, pessoaView.DataNascimento, pessoaView.UF);

            _pessoaRepository.Add(pessoa);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            _logger.Log(LogLevel.Error, "Teve um erro");
            var pessoa = _pessoaRepository.Get();

            return Ok(pessoa);
        }

        [HttpGet]
        [Route("{codigo}")]
        public IActionResult GetPessoaPorCodigo(int codigo)
        {
            var pessoa = _pessoaRepository.GetPessoaPorCodigo(codigo);
        
            return Ok(pessoa);
        }

        [HttpGet]
        [Route("estado/{uf}")]
        public IActionResult GetPessoasPorEstado(string uf)
        {
            var pessoas = _pessoaRepository.GetPessoasPorEstado(uf);

            return Ok(pessoas);
        }

        [HttpDelete]
        [Route("codigo")]
        public IActionResult DeletePessoa(int codigo)
        {
            var pessoa = _pessoaRepository.GetPessoaPorCodigo(codigo);
            _pessoaRepository.Delete(pessoa);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(PessoaViewModel pessoaView)
        {
            var pessoa = _pessoaRepository.GetPessoaPorCodigo(pessoaView.Codigo);

            if(pessoa != null)
            {
                pessoa.Nome = pessoaView.Nome;
                pessoa.Cpf = pessoaView.Cpf;
                pessoa.DataNascimento = pessoaView.DataNascimento;
                pessoa.Uf = pessoaView.UF;
            }

            _pessoaRepository.PutPessoa(pessoa);

            return Ok(pessoa);
        }
    }
}
