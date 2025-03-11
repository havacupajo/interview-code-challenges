﻿using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OnLoanController : ControllerBase
    {
        private readonly ILogger<OnLoanController> _logger;
        private readonly ICatalogueRepository _catalogueRepository;

        public OnLoanController(ILogger<OnLoanController> logger, ICatalogueRepository catalogueRepository)
        {
            _logger = logger;
            _catalogueRepository = catalogueRepository;
        }

        [HttpGet]
        [Route("GetBorrowersWithActiveLoans")]
        public IList<BorrowerWithLoans> GetBorrowersWithActiveLoans()
        {
            return _catalogueRepository.GetBorrowersWithActiveLoans();
        }
    }
}
