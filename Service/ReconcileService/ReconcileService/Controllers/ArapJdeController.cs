using System;
using System.Diagnostics.Metrics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Reconcile.Entity;
using Reconcile.Repository;
using ReconcileService.Extensions;

namespace ReconcileService.Controllers
{
    /// <summary>
    /// Controller class for handling API requests related to ArapJde data.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ArapJdeController : ControllerBase
    {

        private IArapJdeRepository _arapJdeRepository;
        private IMemoryCache _cache;
        ILogger<ArapJdeController> _logger;

        public ArapJdeController(IArapJdeRepository ArapJdeRepository, IMemoryCache cache, ILogger<ArapJdeController> logger)
        {
            _arapJdeRepository = ArapJdeRepository;
            _cache = cache;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a list of ArapJde objects based on optional sorting, filtering, and pagination parameters.
        /// </summary>
        /// <param name="sort">Sorting criteria (optional).</param>
        /// <param name="range">Pagination range (optional).</param>
        /// <param name="filter">Filtering criteria (optional).</param>
        /// <returns>An asynchronous task returning an IEnumerable of ArapJde objects.</returns>
        [HttpGet]
        [Route("list")]
        public async Task<IEnumerable<ArapJde>> GetList([FromQuery] string sort,[FromQuery] string range, [FromQuery] string filter)
        {
            _logger.LogInformation($"Hit GetList(sort={sort}, range={range}, filter={filter})");
            var cachKey = $"arap-{sort}-{range}-{filter}";
            var count = await _cache.GetOrSetAsync<int>("Count/"+cachKey,  () => _arapJdeRepository.GetCount());
            var offsetSize = range.ToOffsetAndSize();
            sort = sort.Replace("\"", "").ToLower();
            var arapJdes = await _cache.GetOrSetAsync<IEnumerable<ArapJde>>("List/" + cachKey,
                    () => _arapJdeRepository.GetList(filter, sort, offsetSize.Item1, offsetSize.Item2));
            Response.Headers.Add("Content-Range", count.ToString());
            
            return arapJdes;
        }

        /// <summary>
        /// Retrieves a single ArapJde object by its unique identifier.
        /// </summary>
        /// <param name="id">The identifier of the ArapJde object to retrieve.</param>
        /// <returns>An asynchronous task returning the ArapJde object with the matching identifier, or null if not found.</returns>
        [HttpGet]
        [Route("list/{id}")]
        public async Task<ArapJde> Get(int id)
        {
            var arapJde = await _arapJdeRepository.GetById(id);
            return arapJde;
        }

        /// <summary>
        /// Updates an existing ArapJde object.
        /// </summary>
        /// <param name="id">The identifier of the ArapJde object to update.</param>
        /// <param name="arapJde">The ArapJde object with the updated information.</param>
        /// <returns>An asynchronous task returning the updated ArapJde object.</returns>
        [HttpPut]
        [Route("list/{id}")]
        public async Task<ArapJde> Put(int id, ArapJde arapJde)
        {
            await _arapJdeRepository.Update(arapJde);
            return arapJde;
        }

        /// <summary>
        /// Deletes an ArapJde object by its unique identifier.
        /// </summary>
        /// <param name="id">The identifier of the ArapJde object to delete.</param>
        /// <returns>An asynchronous task returning an IActionResult indicating success (Ok) with the deleted object's identifier.</returns>
        [HttpDelete]
        [Route("list/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _arapJdeRepository.Delete(id);
            return Ok(id);
        }

        [HttpDelete]
        [Route("list")]
        public async Task<IActionResult> DeleteMany([FromQuery] string filter)
        {
            var arapJdes = new List<ArapJde>();
            var idsFilter = JsonSerializer.Deserialize<IdsFilter>(filter);
            List<int> ids = idsFilter?.id;
            foreach (var id in ids)
            {
                var arapJde = await _arapJdeRepository.GetById(id);
                if (arapJde.Id == id)
                {
                    await _arapJdeRepository.Delete(id);
                    arapJdes.Add(arapJde);
                }
            }
            return Ok(arapJdes);
        }

        /// <summary>
        /// Creates a new ArapJde object.
        /// </summary>
        /// <param name="cp">The ArapJde object to create.</param>
        /// <returns>An asynchronous task returning an IActionResult indicating success (Ok) with the created object.</returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateArapJde([FromBody] ArapJde cp)
        {
            await _arapJdeRepository.Create(cp);
            return Ok(cp);
        }
    }
}

