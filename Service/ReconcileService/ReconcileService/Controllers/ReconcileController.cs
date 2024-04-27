using System;
using System.Collections;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Reconcile.BusinessLogic;
using Reconcile.Entity;
using Reconcile.Repository;
using ReconcileService.Extensions;

namespace ReconcileService.Controllers
{
    /// <summary>
    /// Controller class for handling API requests related to reconcile reports.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ReconcileController : ControllerBase
    {
        private readonly IReconcileReport _reconcileReport;
        private readonly IArapJdeRepository _arapJdeRepository;
        private IMemoryCache _cache;
        private ILogger<ArapJdeController> _logger;

        public ReconcileController(IArapJdeRepository arapRepository, IReconcileReport reconcileReport,
            IMemoryCache cache, ILogger<ArapJdeController> logger)
        {
            _arapJdeRepository = arapRepository;
            _reconcileReport = reconcileReport;
            _cache = cache;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a list of ReconcileItem objects based on optional sorting, filtering, and pagination parameters.
        /// Logs information about the request parameters for debugging purposes.
        /// </summary>
        /// <param name="sort">Sorting criteria (optional).</param>
        /// <param name="range">Pagination range (optional, format: "startIndex,count").</param>
        /// <param name="filter">Filtering criteria (optional).</param>
        /// <returns>An asynchronous task returning an IEnumerable of ReconcileItem objects.</returns>
        [HttpGet]
        [Route("list")]
        public async Task<IEnumerable<ReconcileItem>> GetList([FromQuery] string sort, [FromQuery] string range, [FromQuery] string filter)
        {
            _logger.LogInformation($"Hit Recon.GetList(sort={sort}, range={range}, filter={filter})");
            var cachKey = $"recon-{sort}-{range}-{filter}";
            var count = await _cache.GetOrSetAsync<int>("Count/" + cachKey, () => _arapJdeRepository.GetCount());
            var offsetSize = range.ToOffsetAndSize();
            sort = sort.Replace("\"", "").ToLower();
            var arapJdes =await _cache.GetOrSetAsync<IEnumerable<ArapJde>>("Arap/" + cachKey,
                    () => _arapJdeRepository.GetList(filter, sort, offsetSize.Item1, offsetSize.Item2));
           
            var reconList = await _cache.GetOrSetAsync <IEnumerable < ReconcileItem >> ("Recon/" + cachKey,
                    () => _reconcileReport.Recon(arapJdes));

            Response.Headers.Add("Content-Range", count.ToString());
            return reconList;
        }
    }
}

