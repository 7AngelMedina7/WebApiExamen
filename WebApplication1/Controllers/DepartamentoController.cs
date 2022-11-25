using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTOs;
using WebApplication1.Entidades;

namespace WebApplication1.Controllers
{
        [ApiController]
        [Route("/Departamentos")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class DepartamentoController:ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public DepartamentoController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;

        }
        [HttpGet]
        //Un edificio de departamentos, 
        //    requiere un sistema que le permita visualizar los departamentos que tiene en su totalidad,
        //    si están disponibles u ocupados, quién es el inquilino, cuanto se paga de renta, si está pagado.
        public async Task<ActionResult<List<GetDepartamentoDTO>>> Get()
        {
            var depasAux= await dbContext.Departamentos.ToListAsync();
            return mapper.Map<List<GetDepartamentoDTO>>(depasAux);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult> Post([FromBody] DepartamentoDTO depaDTO)
        {
            var depaAux = mapper.Map<Departamento>(depaDTO);
            dbContext.Add(depaAux);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
