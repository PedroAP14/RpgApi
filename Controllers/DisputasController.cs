using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.Models;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisputasController : ControllerBase
    {
        private readonly DataContext _context;
        public DisputasController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("Arma")]
        public async Task<IActionResult> AtaqueComArmaAsync(Disputa d)
        {
            try
            {
                Personagem? atacante = await _context.TB_PERSONAGENS
                    .Include(p => p.Arma)
                    .FirstOrDefaultAsync(p => p.Id == d.AtacanteId);

                Personagem? oponente = await _context.TB_PERSONAGENS
                    .FirstOrDefaultAsync(p => p.Id == d.OponenteId);

                int dano = atacante.Arma.Dano + (new Random().Next(atacante.Forca));
                dano = dano - new Random().Next(oponente.Defesa);

                if (dano > 0)
                {
                    oponente.PontosVida = oponente.PontosVida - (int)dano;
                }
                if (oponente.PontosVida <= 0)
                {
                    d.Narracao = $"{oponente.Nome} foi derrotado!";
                }

                _context.TB_PERSONAGENS.Update(oponente);
                await _context.SaveChangesAsync();

                StringBuilder dados = new StringBuilder();
                dados.AppendFormat($" Atacante: {atacante.Nome}.");
                dados.AppendFormat($" Oponente: {oponente.Nome}.");
                dados.AppendFormat($" Pontos de vida do atacante: {atacante.PontosVida}.");
                dados.AppendFormat($" Pontos de vida do oponente: {oponente.PontosVida}.");
                dados.AppendFormat($" ArmaUtilizada: {atacante.Arma.Nome}.");
                dados.AppendFormat($" Dano: {dano}.");

                d.Narracao += dados.ToString();
                d.DataDisputa = DateTime.Now;
                _context.TB_DISPUTAS.Add(d);
                _context.SaveChanges();

                return Ok(d);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Habilidade")]
        public async Task<IActionResult> AtaqueComHabilidadeAsync(Disputa d)
        {
            try
            {
                Personagem? atacante = await _context.TB_PERSONAGENS
                    .Include(p => p.PersonagemHabilidades)
                    .ThenInclude(ph => ph.Habilidade)
                    .FirstOrDefaultAsync(p => p.Id == d.AtacanteId);

                Personagem? oponente = await _context.TB_PERSONAGENS
                    .FirstOrDefaultAsync(p => p.Id == d.OponenteId); ;

                PersonagemHabilidade? ph = await _context.TB_PERSONAGENS_HABILIDADES
                    .Include(p => p.Habilidade)
                    .FirstOrDefaultAsync(phBusca => phBusca.HabilidadeId == d.HabilidadeId && phBusca.PersonagemId == d.AtacanteId);

                if (ph == null)
                {
                    d.Narracao = $"{atacante.Nome} não possui está habilidade.";
                }
                else
                {
                    int dano = ph.Habilidade.Dano + (new Random().Next(atacante.Inteligencia));
                    dano = dano - new Random().Next(oponente.Defesa);

                    if (dano > 0)
                    {
                        oponente.PontosVida = oponente.PontosVida - dano;
                    }
                    if (oponente.PontosVida <= 0)
                    {
                        d.Narracao += $"{oponente.Nome} foi derrotado!";
                    }
                    _context.TB_PERSONAGENS.Update(oponente);
                    await _context.SaveChangesAsync();

                    StringBuilder dados = new StringBuilder();
                    dados.AppendFormat($" Atacante: {atacante.Nome}.");
                    dados.AppendFormat($" Oponente: {oponente.Nome}.");
                    dados.AppendFormat($" Pontos de vida do atacante: {atacante.PontosVida}.");
                    dados.AppendFormat($" Pontos de vida do oponente: {oponente.PontosVida}.");
                    dados.AppendFormat($" HabilidadeUtilizada: {ph.Habilidade.Nome}.");
                    dados.AppendFormat($" Dano: {dano}.");
                    d.Narracao += dados.ToString();
                    d.DataDisputa = DateTime.Now;
                    _context.TB_DISPUTAS.Add(d);
                    _context.SaveChanges();
                }

                return Ok(d);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("PersonagemRandom")]
        public async Task<IActionResult> Sorteio()
        {
            List<Personagem> personagens = await _context.TB_PERSONAGENS
                .ToListAsync();

            // Sorteio com numero da quantidade de personagens
            int sorteio = new Random().Next(personagens.Count);

            // busca  na lista pelo indice sorteado (não é o Id)
            Personagem p = personagens[sorteio];

            string msg =
                string.Format($"Número Sorteado: {sorteio}. Personagem: {p.Nome}");

            return Ok(msg);
        }

        [HttpPost("DisputaEmGrupo")]
        public async Task<IActionResult> DisputaEmGrupoAsync(Disputa d)
        {
            try
            {
                d.Resultados = new List<string>(); //Instancia a lista de resultados

                //Busca na base dos personagens informados no parametro incluindo Armas e Habilidades
                List<Personagem> personagens = await _context.TB_PERSONAGENS
                    .Include(p => p.Arma)
                    .Include(p => p.PersonagemHabilidades)
                        .ThenInclude(ph => ph.Habilidade)
                    .Where(p => d.ListaIdPersonagem.Contains(p.Id))
                        .ToListAsync();

                //Contagem de personagens vivos na lista obtida do banco de dados
                int qtdPersonagensVivos = personagens.FindAll(p => p.PontosVida > 0).Count;

                //Enquanto houver mais de um personagem vivo haverá disputa
                while (qtdPersonagensVivos > 1)
                {
                    //Seleciona personagens com pontos de vida positivo e depois faz o sorteio
                    List<Personagem> atacantes = personagens.Where(p => p.PontosVida > 0).ToList();
                    Personagem atacante = atacantes[new Random().Next(atacantes.Count)];
                    d.AtacanteId = atacante.Id;

                    //Seleciona personagens com pontos de vida positivos, exceto o atacante escolhido e depois faz o sorteio
                    List<Personagem> oponentes = personagens.Where(p => p.Id != atacante.Id && p.PontosVida > 0).ToList();
                    Personagem oponente = oponentes[new Random().Next(oponentes.Count)];
                    d.OponenteId = oponente.Id;

                    //declara e redefine a cada passagem do while o valor das variáveis que serão usados
                    int dano = 0;
                    string ataqueUsado = string.Empty;
                    string resultado = string.Empty;

                    //Sorteia entre 0 e 1: 0 é ataque com arma e 1 é ataque com habilidade
                    bool ataqueUsaArma = (new Random().Next(1) == 0);

                    if (ataqueUsaArma && atacante.Arma != null) //Ataque com Arma
                    {
                        //Sorteio da Força
                        dano = atacante.Arma.Dano + (new Random().Next(atacante.Forca));
                        dano = dano - new Random().Next(oponente.Defesa);
                        //Sorteio da Defesa
                        ataqueUsado = atacante.Arma.Nome;

                        if (dano > 0)
                        {
                            oponente.PontosVida = oponente.PontosVida - (int)dano;
                        }

                        //Formata a mensagem
                        resultado =
                            string.Format($"{atacante.Nome} atacou {oponente.Nome} usando {ataqueUsado} com o dano {dano} ");
                        d.Narracao += resultado; //Contatena o resultado com as narrações existentes
                        d.Resultados.Add(resultado); //Adiciona o resultado atual na lista de resultados
                    }
                    else if (atacante.PersonagemHabilidades.Count != 0) //Ataque com Habilidade
                    {
                        //Realiza o sorteio entre Habilidade e na linha seguinte a seleciona
                        int sorteioHabilidadeId = new Random().Next(atacante.PersonagemHabilidades.Count);
                        Habilidade habilidadeEscolhida = atacante.PersonagemHabilidades[sorteioHabilidadeId].Habilidade;
                        ataqueUsado = habilidadeEscolhida.Nome;

                        //Sorteio da inteligência somada ao dano
                        dano = habilidadeEscolhida.Dano + (new Random().Next(atacante.Inteligencia));
                        dano = dano - new Random().Next(oponente.Defesa); //Soretio da defesa

                        if (dano > 0)
                        {
                            oponente.PontosVida = oponente.PontosVida - (int)dano;
                        }

                        resultado =
                            string.Format($"{atacante.Nome} atacou {oponente.Nome} usando {ataqueUsado} com o dano {dano} ");
                        d.Narracao += resultado;
                        d.Resultados.Add(resultado);
                    }

                    if (!string.IsNullOrEmpty(ataqueUsado))
                    {
                        //Incrementa os dados dos combates
                        atacante.Vitorias++;
                        oponente.Derrotas++;
                        atacante.Disputas++;
                        oponente.Disputas++;

                        d.Id = 0; //Zera o Id para poder salvar os dados de disputa sem erro de chave
                        d.DataDisputa = DateTime.Now;
                        _context.TB_DISPUTAS.Add(d);
                        await _context.SaveChangesAsync();
                    }

                    qtdPersonagensVivos = personagens.FindAll(p => p.PontosVida > 0).Count;

                    if (qtdPersonagensVivos == 1) //Havendo só um personagem vivo, existe um CAMPEÃO
                    {
                        string resultadoFinal =
                            $"{atacante.Nome.ToUpper()} é CAMPEÃO com {atacante.PontosVida} pontos de vida estantes!";

                        d.Narracao += resultadoFinal;
                        d.Resultados.Add(resultadoFinal);

                        break; //Vai parar o While
                    }

                }

                //Código após o fechamento do While. Atualizará os pontos de vida,
                //disputas, vitórias e derrotas de todos personagens ao final das batalhas
                _context.TB_PERSONAGENS.UpdateRange(personagens);
                await _context.SaveChangesAsync();

                return Ok(d);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("ApagarDisputas")]
        public async Task<IActionResult> DeleteAsync()
        {
            try
            {
                List<Disputa> disputas = await _context.TB_DISPUTAS.ToListAsync();
                _context.TB_DISPUTAS.RemoveRange(disputas);
                await _context.SaveChangesAsync();
                return Ok("Disputas apagadas");
            }
            catch (System.Exception ex)
            { return BadRequest(ex.Message); }
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarAsync()
        {
            try
            {
                List<Disputa> disputas =
                await _context.TB_DISPUTAS.ToListAsync();
                return Ok(disputas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }






    }
}