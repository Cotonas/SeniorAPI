using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorAPI.Models
{
    [Table("Pessoa")]
    public class PessoaModel
    {

        [Key]
        [Column("Codigo")]
        public int Codigo { get; set; }
        
        [Column("Nome")]
        public string Nome { get; set; }
        
        [Column("Cpf")]
        public string Cpf { get; set; }
        
        [Column("DataNascimento")]
        public DateTime DataNascimento { get; set; }
        
        [Column("uf")]
        public string Uf { get; set; }

        public PessoaModel(int codigo, string nome, string cpf, DateTime dataNascimento, string uf)
        {
            Codigo = codigo;
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            Cpf = cpf ?? throw new ArgumentNullException(nameof(cpf));
            DataNascimento = dataNascimento;
            Uf = uf ?? throw new ArgumentNullException(nameof(uf));
        }

    }
}
