using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using RpgApi.Models.Enuns;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonagensExercicioController : ControllerBase
    {
        private static List<Personagem> personagens = new List<Personagem>()
        {
            //Personagens aqui
            new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=39, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=80, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=95, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }
        };

        [HttpGet("GetByNome/{nome}")]
        public IActionResult GetByNome(string nome)
        {
            Personagem nomeBusca = personagens.Find(n => n.Nome == nome);
            if (nomeBusca == null)
            {
                return BadRequest("NotFound");
            }
            else
            {
                return Ok(nomeBusca);
            }

        }

        [HttpGet("GetClerigoMago")]
        public IActionResult GetClerigomago()
        {
            List<Personagem> buscaMC = personagens.FindAll(c => c.Classe != ClasseEnum.Cavaleiro).OrderByDescending(p => p.PontosVida).ToList();
            return Ok(buscaMC);
        }

        [HttpGet("GetEstatisticas")]
        public IActionResult GetEstatisticas()
        {
            int qtdPersonagem = personagens.Count();
            int somaInteligencia = personagens.Sum(i => i.Inteligencia);

            return Ok($"Quantidade de Personagens: {qtdPersonagem}\nSoma das Inteligências: {somaInteligencia}");
        }

        [HttpPost("PostValidacao")]
        public IActionResult PostValidacao(Personagem novoPersonagem)
        {
            if (novoPersonagem.Defesa < 10 || novoPersonagem.Inteligencia > 30)
            {
                if (novoPersonagem.Defesa < 10 && novoPersonagem.Inteligencia > 30)
                    return BadRequest("Erro!!!\nA defesa do personagem é menor que 10 e a inteligência é maior que 30");

                else if (novoPersonagem.Defesa < 10)
                    return BadRequest("Erro!!!\nA defesa do personagem é menor que 10");

                else
                    return BadRequest("Erro!!!\nA inteligência do personagem é maior que 30");
            }
            else
                personagens.Add(novoPersonagem);
            return Ok(personagens);
        }

        [HttpPost("PostValidacaoMago")]
        public IActionResult PostValidacaoMago(Personagem novoPersonagem)
        {
            if (novoPersonagem.Classe == ClasseEnum.Mago)
            {
                if (novoPersonagem.Inteligencia < 35)
                {
                    return BadRequest("Erro!!!\nA inteligência do personagem é menor que 35");
                }
                else
                    personagens.Add(novoPersonagem);
                    return Ok(personagens);
            }
            else
                return BadRequest("Erro!!!\nA classe do personagem não é mago");
        }

        [HttpGet("GetByClass/{classe}")]
        public IActionResult GetByClass(int classe)
        {
            if (classe == 1)
            {
                List<Personagem>cavaleiro = personagens.FindAll(c => c.Classe == ClasseEnum.Cavaleiro);
                return Ok(cavaleiro);
            }
            else if (classe == 2)
            {
                List<Personagem>mago = personagens.FindAll(c => c.Classe == ClasseEnum.Mago);
                return Ok(mago);
            }
            else if (classe == 3)
            {
                List<Personagem>clerigo = personagens.FindAll(c => c.Classe == ClasseEnum.Clerigo);
                return Ok(clerigo);
            }
            else
                 return BadRequest("Erro!!!\nEssa classe não existe");
        }
        


    }
}