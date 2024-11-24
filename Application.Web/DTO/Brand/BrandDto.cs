using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Web.DTO.Brand
{
    public class BrandDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Brand Name")]
        public string BrandName { get; set; }
    }
}
