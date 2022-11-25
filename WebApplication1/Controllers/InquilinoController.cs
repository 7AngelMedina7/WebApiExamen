using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTOs;
using WebApplication1.Entidades;

namespace WebApplication1.Controllers
{
        [ApiController]
        [Route("/Inquilino")]
        [AllowAnonymous]
        
    public class InquilinoController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public InquilinoController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;

        }
        [HttpPost("AgregarInquilino")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult> Post([FromBody] InquilinoDTO inquiDTO)
        {
            var inquiAux = mapper.Map<Inquilino>(inquiDTO);
            dbContext.Add(inquiAux);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("{Nombre:string}")]
        [AllowAnonymous]
        public async Task <ActionResult<List<Inquilino>>> Get(string Nombre)
        {
            return await dbContext.Inquilinos.ToListAsync();
        }
    }
}
