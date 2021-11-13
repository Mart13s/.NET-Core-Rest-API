using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Dtos
{
    public record CreateReviewDto([Required]int rating, [Required]string reviewName, [Required]string reviewText);

}
