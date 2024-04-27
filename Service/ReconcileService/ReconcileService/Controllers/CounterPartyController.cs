using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Reconcile.Entity;
using Reconcile.Repository;
using ReconcileService.Extensions;

namespace ReconcileService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CounterPartyController : ControllerBase
    {

        private ICounterPartyRepository _CounterPartyRepository;
        private IMemoryCache _cache;

        public CounterPartyController(ICounterPartyRepository CounterPartyRepository, IMemoryCache cache)
        {
            _CounterPartyRepository = CounterPartyRepository;
            _cache = cache;
        }


        /// <summary>
        /// Retrieves a list of all CounterParty objects from the cache or repository.
        /// </summary>
        /// <returns>An asynchronous task returning an IEnumerable of CounterParty objects.</returns>
        [HttpGet]
        [Route("list")]
        public async Task<IEnumerable<CounterParty>> All()
        {
            var cachKey = $"cp";
            var CounterPartys = await _cache.GetOrSetAsync < IEnumerable < CounterParty >> ("Count/" + cachKey, () => _CounterPartyRepository.GetAll());

            Response.Headers.Add("Content-Range", CounterPartys.Count().ToString());
            return CounterPartys;
        }

        /// <summary>
        /// Retrieves a single CounterParty object by its unique identifier.
        /// </summary>
        /// <param name="id">The identifier of the CounterParty object to retrieve.</param>
        /// <returns>An asynchronous task returning the CounterParty object with the matching identifier, or null if not found.</returns>
        [HttpGet]
        [Route("list/{id}")]
        public async Task<CounterParty> Get(int id)
        {
            var CounterParty = await _CounterPartyRepository.GetById(id);
            return CounterParty;
        }

        /// <summary>
        /// Updates an existing CounterParty object.
        /// </summary>
        /// <param name="id">The identifier of the CounterParty object to update.</param>
        /// <param name="counterParty">The CounterParty object with the updated information.</param>
        /// <returns>An asynchronous task returning the updated CounterParty object.</returns>
        [HttpPut]
        [Route("list/{id}")]
        public async Task<CounterParty> Put(int id, CounterParty CounterParty)
        {
            await _CounterPartyRepository.Update(CounterParty);
            return CounterParty;
        }

        /// <summary>
        /// Deletes a CounterParty object by its unique identifier.
        /// </summary>
        /// <param name="id">The identifier of the CounterParty object to delete.</param>
        /// <returns>An asynchronous task returning an IActionResult indicating success (Ok) with the deleted object's identifier.</returns>
        [HttpDelete]
        [Route("list/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _CounterPartyRepository.Delete(id);
            return Ok(id);
        }

        /// <summary>
        /// Deletes multiple CounterParty objects based on a filter criteria provided in the request body.
        /// </summary>
        /// <param name="filter">A JSON string containing the filter criteria (expected to be of type IdsFilter).</param>
        /// <returns>An asynchronous task returning an IActionResult indicating success (Ok) with a list of the deleted CounterParty objects.</returns>
        [HttpDelete]
        [Route("list")]
        public async Task<IActionResult> DeleteMany([FromQuery] string filter)
        {
            var CounterPartys = new List<CounterParty>();
            var idsFilter = JsonSerializer.Deserialize<IdsFilter>(filter);
            List<int> ids = idsFilter?.id;
            foreach (var id in ids)
            {
                var CounterParty = await _CounterPartyRepository.GetById(id);
                if (CounterParty.Id == id)
                {
                    await _CounterPartyRepository.Delete(id);
                    CounterPartys.Add(CounterParty);
                }
            }
            return Ok(CounterPartys);
        }

        /// <summary>
        /// Creates a new CounterParty object.
        /// </summary>
        /// <param name="cp">The CounterParty object to create.</param>
        /// <returns>An asynchronous task returning an IActionResult indicating success (Ok) with the created CounterParty object.</returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCounterParty([FromBody] CounterParty cp)
        {
            await _CounterPartyRepository.Create(cp);
            return Ok(cp);
        }
    }

}

