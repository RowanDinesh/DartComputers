using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Web.Models
{
    public class Category : BaseModel
    {
        [Required]
        [DisplayName("Category Name")]
        public string CategoryName {  get; set; }
    }
}
