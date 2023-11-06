using SeniorAPI.DTOModels;
using SeniorAPI.Models;

namespace SeniorAPI.Interfaces
{
    public interface IPessoaRepository
    {
        Task<PessoaModel> Add(PessoaDTOModel pessoa);

        Task<List<PessoaModel>> Get();

        Task<PessoaModel> GetPessoaPorCodigo(int codigo);

        Task<List<PessoaModel>> GetPessoasPorEstado(string uf);

        void Delete(int codigo);

        Task<PessoaModel> PutPessoa(PessoaModel pessoa);
    }
}
