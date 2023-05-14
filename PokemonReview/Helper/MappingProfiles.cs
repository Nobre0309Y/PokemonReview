using AutoMapper;
using PokemonReview.Models;
using PokemonReview.DTO;

namespace PokemonReview.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDTO>();   
        }
    }
}
