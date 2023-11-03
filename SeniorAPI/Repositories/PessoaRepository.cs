using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SeniorAPI.Data;
using SeniorAPI.Interfaces;
using SeniorAPI.Models;

namespace SeniorAPI.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly SessionContext _sessionContext = new SessionContext();

        public async Task<PessoaModel> Add(PessoaModel pessoa)
        {
            await Validacao(pessoa);

            var ultimoID = _sessionContext.Pessoas.Max(x => (int?)x.Codigo) ?? 0;
            if (ultimoID == 0)
            {
                pessoa.Codigo = 1;
            }
            else
            {
                pessoa.Codigo = ultimoID + 1;
            }

            await _sessionContext.Pessoas.AddAsync(pessoa);
            await _sessionContext.SaveChangesAsync();
            return pessoa;
        }

        public async void Delete(PessoaModel pessoa)
        {
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
            {
                throw new Exception("Pessoa não encontrada com o código informado.");
            }

            return pessoa;
        }

        public async Task<List<PessoaModel>> GetPessoasPorEstado(string uf)
        {

            if (uf.Length != 2)
                throw new ApplicationException("Favor inserir apenas a sigla do estado.");

            var pessoas = _sessionContext.Pessoas.Where(x => x.Uf.ToUpper() == uf.ToUpper()).ToList();

            return pessoas;
        }

        public async Task<PessoaModel> PutPessoa(PessoaModel pessoa)
        {
            await Validacao(pessoa);

            _sessionContext.Pessoas.Update(pessoa);
            await _sessionContext.SaveChangesAsync();

            return pessoa;
        }


        public async Task Validacao(PessoaModel pessoa)
        {
            if (pessoa.Nome.IsNullOrEmpty())
                throw new ApplicationException("Nome inválido.");
            else if (pessoa.Cpf.Length != 11)
                throw new ApplicationException("CPF inválido.");
            else if (pessoa.Uf.Length != 2)
                throw new ApplicationException("Favor inserir apenas a sigla do estado.");
        }
    }
}
