namespace SeniorAPI.Models
{
    public interface IPessoaRepository
    {
        void Add(PessoaModel pessoa);

        List<PessoaModel> Get();

        PessoaModel GetPessoaPorCodigo(int codigo);

        List<PessoaModel> GetPessoasPorEstado(string uf);

        void Delete(PessoaModel pessoa);

        PessoaModel PutPessoa(PessoaModel pessoa);
    }
}
