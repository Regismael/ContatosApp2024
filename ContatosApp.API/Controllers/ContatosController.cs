using ContatosApp.API.Models;
using ContatosApp.Data.Entities;
using ContatosApp.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContatosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        /// <summary>
        /// Método para cadastro do contato na API
        /// </summary>
        [HttpPost]
        public IActionResult Post(ContatosPostModel model)
        {
            try
            {


                var contato = new Contato
                {
                    Id = Guid.NewGuid(),
                    DataHoraCadastro = DateTime.Now,
                    Nome = model.Nome,
                    Email = model.Email,
                    Telefone = model.Telefone,
                    Ativo = 1
                };

                var contatoRepository = new ContatoRepository();
                contatoRepository.Insert(contato);

                return StatusCode(201, new
                {
                    message = "Contato cadastrado com sucesso! ",
                    contato
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    e.Message
                });

            }
        }

        /// <summary>
        /// Método para atualizar um contato na API
        /// </summary>
        [HttpPut]
        public IActionResult Put(ContatosPutModel model)
        {
            try
            {
                var contatoRepository = new ContatoRepository();
                var contato = contatoRepository.GetById(model.Id.Value);

                if(contato != null)
                {
                    //modificando os dados do contato
                    contato.Nome = model.Nome;
                    contato.Email = model.Email;
                    contato.Telefone = model.Telefone;

                    contatoRepository.Update(contato);

                    return StatusCode(200, new
                    {
                        message = "Contato atualizado com sucesso.",
                        contato
                    });
                }
                else
                {
                    return StatusCode(400, new
                    {
                        message = "Contato não encontrado. Verifique o ID."
                    });
                }
            }
            catch(Exception e)
            {
                return StatusCode(500, new
                {
                    e.Message
                });
            }
        }

        /// <summary>
        /// Método para excluir um contato na API 
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                //consultando o contato atraves do ID
                var contatoRepository = new ContatoRepository();
                var contato  = contatoRepository.GetById(id);

                //verificando se o contato foi encontrado
                if(contato != null)
                {
                    
                    //excluindo o contato
                    contatoRepository.Delete(contato);
                    return StatusCode(200, new
                    {
                        message = "Contato excluído com sucesso.",
                        contato
                    });
                }
                else
                {
                    return StatusCode(400, new
                    {
                        message = "Contato não encontrado. Verifique o ID."
                    });
                }
            }
            catch (Exception e)
            {

                return StatusCode(500, new
                {
                    e.Message
                });
            }
        }

        /// <summary>
        /// Método para consultar um contato na API 
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var contatoRepository = new ContatoRepository();
                var contatos = contatoRepository.GetAll();

                return StatusCode(200,contatos);
            }
            catch(Exception e) 
            {
                return StatusCode(500, new
                {
                    e.Message
                });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var contatoRepository = new ContatoRepository();
                var contato = contatoRepository.GetById(id);

                if(contato != null)
                {
                    //HTTP 200 (OK)
                    return StatusCode(200, contato);
                }
                else
                {
                    return NoContent(); //HTTP 204
                }
            }
            catch(Exception e)
            {
                return StatusCode(500, new
                {
                    e.Message
                });
            }

        }

    }
}
