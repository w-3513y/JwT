using System.Threading.Tasks;
using JwT.Models;
using JwT.Repositories;
using JwT.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwT.Controllers
{
    [Route("v1/account")]

    public class SpyController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] Spy model)
        {
            var spy = SpyRepository.Get(model.Username, model.Password);
            if (spy == null)
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }
            var token = TokenService.GenerateToken(spy);
            spy.Password = "";
            return new
            {
                spy = spy,
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";
//qualquer token pode acessar
        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => string.Format("Autenticado - {0}", User.Identity.Name);
//somente tokens da regra: gerente, funcionário pode acessar
        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee, manager")]
        public string Employee() => "Funcionario";
//somente tokens da regra: gerente pode acessar
        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Manager";





    }
}