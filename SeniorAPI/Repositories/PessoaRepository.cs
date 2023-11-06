using Microsoft.IdentityModel.Tokens;
using SeniorAPI.Data;
using SeniorAPI.DTOModels;
using SeniorAPI.Interfaces;
using SeniorAPI.Models;

namespace SeniorAPI.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly SessionContext _sessionContext = new SessionContext();

        public async Task<PessoaModel> Add(PessoaDTOModel pessoa)
        {
            var _pessoa = new PessoaModel();

            var ultimoID = _sessionContext.Pessoas.Max(x => (int?)x.Codigo) ?? 0;
            
            if (ultimoID == 0)
                _pessoa.Codigo = 1;
            else
                _pessoa.Codigo = ultimoID + 1;

            _pessoa.Nome = pessoa.Nome;
            _pessoa.Cpf = pessoa.Cpf;
            _pessoa.DataNascimento = pessoa.DataNascimento.ToUniversalTime();
            _pessoa.Uf = pessoa.Uf;

            Validacao(_pessoa);

            await _sessionContext.Pessoas.AddAsync(_pessoa);
            await _sessionContext.SaveChangesAsync();
            return _pessoa;
        }

        public async void Delete(int codigo)
        {
            var pessoa = await GetPessoaPorCodigo(codigo);

            _sessionContext.Pessoas.Remove(pessoa);
            await _sessionContext.SaveChangesAsync();
        }

        public async Task<List<PessoaModel>> Get()
        {
            var pessoas = _sessionContext.Pessoas.OrderBy(x => (int?)x.Codigo)?.ToList();

            return pessoas;
        }

        public async Task<PessoaModel> GetPessoaPorCodigo(int codigo)
        {
            var pessoa = _sessionContext.Pessoas.FirstOrDefault(x => x.Codigo == codigo);

            if (pessoa == null)
                throw new ApplicationException("Pessoa não encontrada com o código informado.");

            return pessoa;
        }

        public async Task<List<PessoaModel>> GetPessoasPorEstado(string uf)
        {
            if (uf.Length != 2)
                throw new ApplicationException("Favor insira uma sigla valida para o estado.");

            var pessoas = _sessionContext.Pessoas.Where(x => x.Uf.ToUpper() == uf.ToUpper()).ToList();

            return pessoas;
        }

        public async Task<PessoaModel> PutPessoa(PessoaModel pessoa)
        {
            Validacao(pessoa);

            var _pessoa = await GetPessoaPorCodigo(pessoa.Codigo);

            _pessoa.Nome = pessoa.Nome;
            _pessoa.Cpf = pessoa.Cpf;
            _pessoa.DataNascimento = pessoa.DataNascimento.ToUniversalTime();
            _pessoa.Uf = pessoa.Uf;

            _sessionContext.Pessoas.Update(_pessoa);
            await _sessionContext.SaveChangesAsync();

            return _pessoa;
        }


        public void Validacao(PessoaModel pessoa)
        {
            if (pessoa.Nome.IsNullOrEmpty())
                throw new ApplicationException("Nome inválido.");
            else if (pessoa.Cpf.Length != 11)
                throw new ApplicationException("CPF inválido.");
            else if (pessoa.Uf.Length != 2)
                throw new ApplicationException("Favor inserir uma sigla valida para o estado.");
        }
    }
}
