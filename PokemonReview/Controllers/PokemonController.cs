using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReview.Data;
using PokemonReview.Interfaces;
using PokemonReview.Models;
using PokemonReview.DTO; 
using System.Collections.Generic;

namespace PokemonReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public PokemonController(IPokemonRepository pokemonRepository, IMapper mapper, DataContext context)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
            _context = context;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public ActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDTO>>(_pokemonRepository.GetPokemons());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemons);
        }


        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]

        public ActionResult GetPokemon(int pokeId)
        {
            if ( !_pokemonRepository.PokemonExists(pokeId) ) return NotFound();
            var pokemon = _mapper.Map < PokemonDTO > (_pokemonRepository.GetPokemon(pokeId));
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(pokemon);
        }

        [HttpGet("{pokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId)) return NotFound();
            var rating = _pokemonRepository.GetPokemonRating(pokeId);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(rating);
        }

        [HttpPost]
        [Route("/pokemons")]
        public ActionResult AddPokemon(PokemonDTO model)
        {
            _context.Pokemon.Add(_pokemonRepository.ToPokemon(model));
            _context.SaveChanges();

            return Ok(model);
        }
    }
}
