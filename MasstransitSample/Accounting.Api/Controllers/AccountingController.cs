using Accounting.Api.Commands;
using Accounting.Api.Entities;
using Accounting.Api.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountingController : ControllerBase
    {
        private readonly ILogger<AccountingController> _logger;
        private readonly IAccountingRepository _accountingRepository;

        public AccountingController(ILogger<AccountingController> logger, IAccountingRepository accountingRepository)
        {
            _logger = logger;
            _accountingRepository = accountingRepository;
        }

        // Get all Accounts from AccountingRepository
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var accounts = await _accountingRepository.GetAll();
            return Ok(accounts);
        }

        // Get Account by UserName from AccountingRepository
        [HttpGet("{userName}")]
        public async Task<IActionResult> Get(string userName)
        {
            var account = await _accountingRepository.GetBy(userName);
            return Ok(account);
        }

        // Add Account to AccountingRepository
        [HttpPost]
        public async Task<IActionResult> Post(AddAccountingCommand account)
        {
            await _accountingRepository.AddAccount(account);
            return Ok();
        }



    }
}