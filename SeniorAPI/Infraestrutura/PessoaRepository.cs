using SeniorAPI.Models;

namespace SeniorAPI.Infraestrutura
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly SessionContext _sessionContext =  new SessionContext();


        public void Add(PessoaModel pessoa)
        {
            _sessionContext.Pessoas.Add(pessoa);
            _sessionContext.SaveChanges();
        }

        public void Delete(PessoaModel pessoa)
        {
            _sessionContext.Pessoas.Remove(pessoa);
            _sessionContext.SaveChanges();
        }

        public List<PessoaModel> Get()
        {
            return _sessionContext.Pessoas.OrderBy(x => x.Codigo).ToList();
        }

        public PessoaModel GetPessoaPorCodigo(int codigo) 
        {
            return _sessionContext.Pessoas.FirstOrDefault(x => x.Codigo == codigo);
        }

        public List<PessoaModel> GetPessoasPorEstado(string uf)
        {
            return _sessionContext.Pessoas.Where(x => x.Uf.ToLower() == uf.ToLower()).ToList();
        }

        public PessoaModel PutPessoa(PessoaModel pessoa)
        {
            _sessionContext.Pessoas.Update(pessoa);
            _sessionContext.SaveChanges();
            return pessoa;
        }

    }
}
