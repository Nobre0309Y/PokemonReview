using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;
using PokemonReview.DTO;

namespace PokemonReview.DTO
{
    public class PokemonDTO 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; } 
    }
}