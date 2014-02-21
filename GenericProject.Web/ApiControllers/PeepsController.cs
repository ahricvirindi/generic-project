using System.Collections.Generic;
using System.Web.Http;
using GenericProject.Core.Model;
using GenericProject.Core.Models;
using GenericProject.Core.Services;

namespace GenericProject.Web.ApiControllers
{
    public class PeepsController : BaseApiController
    {
        private readonly IPeepService _peepService;

        public PeepsController(IPeepService peepService) { _peepService = peepService; }

        public PagedEnumerable<Peep> GetAll([FromUri]PaginationRequest paginationRequest)
        {
            return _peepService.GetPeeps(paginationRequest);
        }

        public Peep Get(long id)
        {
            return _peepService.GetPeep(id);
        }

    }
}