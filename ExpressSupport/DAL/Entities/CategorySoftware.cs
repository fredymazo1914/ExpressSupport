using System.ComponentModel.DataAnnotations;

namespace ExpressSupport.DAL.Entities
{
    public class CategorySoftware : Entity
    {
        [Display(Name = "Categoría Software")]
        [MaxLength(80)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }

        public string Description { get; set; }

    }
}
