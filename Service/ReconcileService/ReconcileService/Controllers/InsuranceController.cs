using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Reconcile.Entity;
using Reconcile.Repository;
using ReconcileService.Extensions;

namespace ReconcileService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InsuranceController : ControllerBase
    {
        private IInsuranceRepository _insuranceRepository;
        private IMemoryCache _cache;
        public InsuranceController(IInsuranceRepository insuranceRepository, IMemoryCache cache)
        {
            _insuranceRepository = insuranceRepository;
            _cache = cache;
        }

        /// <summary>
        /// Retrieves a list of all Insurance objects from the cache or repository.
        /// </summary>
        /// <returns>An asynchronous task returning an IEnumerable of Insurance objects.</returns>
        [HttpGet]
        [Route("list")]
        public async Task< IEnumerable<Insurance>> All()
        {
            var cachKey = $"insurances";
            var insurances = await _cache.GetOrSetAsync<IEnumerable<Insurance>>("Count/" + cachKey, () => _insuranceRepository.GetAll());
            Response.Headers.Add("Content-Range", insurances.Count().ToString());
            return insurances;
        }

        /// <summary>
        /// Retrieves a single Insurance object by its unique identifier.
        /// </summary>
        /// <param name="id">The identifier of the Insurance object to retrieve.</param>
        /// <returns>An asynchronous task returning the Insurance object with the matching identifier, or null if not found.</returns>
        [HttpGet]
        [Route("list/{id}")]
        public async Task<Insurance> Get(int id)
        {
            var insurance= await _insuranceRepository.GetById(id);
            return insurance;
        }

        /// <summary>
        /// Updates an existing Insurance object.
        /// </summary>
        /// <param name="id">The identifier of the Insurance object to update.</param>
        /// <param name="insurance">The Insurance object with the updated information.</param>
        /// <returns>An asynchronous task returning the updated Insurance object.</returns>
        [HttpPut]
        [Route("list/{id}")]
        public async Task<Insurance> Put(int id, Insurance insurance)
        {
            await _insuranceRepository.Update(insurance);
            return insurance;
        }

        /// <summary>
        /// Deletes an Insurance object by its unique identifier.
        /// </summary>
        /// <param name="id">The identifier of the Insurance object to delete.</param>
        /// <returns>An asynchronous task returning an IActionResult indicating success (Ok) with the deleted object's identifier.</returns>
        [HttpDelete]
        [Route("list/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _insuranceRepository.Delete(id);
            return Ok(id);
        }

        
        /// <summary>
        /// Deletes multiple Insurance objects based on a filter criteria provided in the request body.
        /// </summary>
        /// <param name="filter">A JSON string containing the filter criteria (expected to be of type IdsFilter).</param>
        /// <returns>An asynchronous task returning an IActionResult indicating success (Ok) with a list of the deleted Insurance objects.</returns>
        [HttpDelete]
        [Route("list")]
        public async Task<IActionResult> DeleteMany([FromQuery]string filter)
        {
            var insurances = new List<Insurance>();
            var idsFilter = JsonSerializer.Deserialize<IdsFilter>(filter);
            List<int> ids = idsFilter?.id;
            foreach (var id in ids)
            {
                var insurance = await _insuranceRepository.GetById(id);
                if(insurance.Id==id )
                {
                    await _insuranceRepository.Delete(id);
                    insurances.Add(insurance);
                }
            }
            return Ok(insurances);
        }

        /// <summary>
        /// Creates a new Insurance object.
        /// </summary>
        /// <param name="cp">The Insurance object to create.</param>
        /// <returns>An asynchronous task returning an IActionResult indicating success (Ok) with the created Insurance object.</returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateInsurance([FromBody] Insurance cp)
        {
            await _insuranceRepository.Create(cp);
            return Ok(cp);
        }
    }
}

