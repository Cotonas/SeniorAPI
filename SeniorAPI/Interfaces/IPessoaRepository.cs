using SeniorAPI.Models;

namespace SeniorAPI.Interfaces
{
    public interface IPessoaRepository
    {
        Task<PessoaModel> Add(PessoaModel pessoa);

        Task<List<PessoaModel>> Get();

        Task<PessoaModel> GetPessoaPorCodigo(int codigo);

        Task<List<PessoaModel>> GetPessoasPorEstado(string uf);

        void Delete(PessoaModel pessoa);

        Task<PessoaModel> PutPessoa(PessoaModel pessoa);
    }
}
