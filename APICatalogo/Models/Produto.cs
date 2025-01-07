using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

// Implementing IValidatableObject allows for custom validation
[Table("Produtos")]
public class Produto: IValidatableObject
{
    [Key]
    public int ProdutoId { get; set; }

    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)]
    public string? Descricao { get; set; }
    [Required]
    [Column(TypeName ="decimal(10,2)")]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; }


    [JsonIgnore] // Prevents serialization of Categoria
    public Categoria? Categoria { get; set; }

    // Implementing a complex validation logic
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(this.Nome))
        {
            var primeiraLetra = this.Nome[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                // yield indicates the method is an interator (returns separately)
                yield return new
                    ValidationResult("A primeira letra do produto deve ser maiúscula",
                    new[]
                    { nameof(this.Nome) }
                    );
            }
        }

        if (this.Estoque <= 0)
        {
            yield return new
                   ValidationResult("O estoque deve ser maior que zero",
                   new[]
                   { nameof(this.Estoque) }
                   );
        }
    }

}
