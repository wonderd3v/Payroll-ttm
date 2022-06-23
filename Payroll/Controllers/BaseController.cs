using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Payroll.Core.BaseModels;
using Payroll.Filters;
using Payroll.Services.Services;

namespace Payroll.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController<TEntity, TDto> : ControllerBase
        where TEntity : IBaseEntity
        where TDto : IBaseEntityDto
    {
        protected readonly IBaseService<TEntity, TDto> _service;

        public BaseController(IBaseService<TEntity, TDto> service)
        {
            _service = service;
        }
        
        [HttpGet]
        [EnableQuery]
        public virtual async Task<IActionResult> Get()
        {
            var query = _service.AsQuery();
            var result = await _service.ProjectToDto(query);
            return Ok(result);
        }

        [HttpGet("Query")]
        [ODataFeature]
        public virtual async Task<IActionResult> Query(ODataQueryOptions<TEntity> queryOptions)
        {
            var query = _service.AsQuery();

            var odataQuery = queryOptions.ApplyTo(query).Cast<TEntity>();

            var result = await _service.ProjectToDto(odataQuery);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result is null)
                return NotFound($"The record with id {id} was not found");

            return Ok(result);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TDto dto)
        {
            var result = await _service.AddAsync(dto);

            if (result.IsSuccess is false)
                return UnprocessableEntity(result);

            return CreatedAtAction(WebRequestMethods.Http.Get, new { id = result.Entity.Id }, result.Entity);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put([FromRoute] int id, [FromBody] TDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);

            if (result is null)
                return NotFound($"The record with id {id} was not found");

            if (result.IsSuccess is false)
                return UnprocessableEntity(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _service.DeleteByIdAsync(id);

            if (result is null)
                return NotFound($"The record with id {id} was not found");

            return Ok(result);
        }
    }
}