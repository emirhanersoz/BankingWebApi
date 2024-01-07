using DigitalBankApi.Enums;
using DigitalBankApi.Interfaces.IServices;

namespace DigitalBankApi.Services
{
    public class DailyPaymentService : IDailyPaymentService, IHostedService, IDisposable
    {
        private readonly ILogger<DailyPaymentService> _logger;
        private readonly PayeeService _payeeService;
        private readonly IAccountService _accountService;
        private Timer _timer;

        public DailyPaymentService(ILogger<DailyPaymentService> logger, PayeeService payeeService, IAccountService accountService)
        {
            _logger = logger;
            _payeeService = payeeService;
            _accountService = accountService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Daily Payment Service is starting.");

            var now = DateTime.Now;
            var dueTime = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59) - now;
            _timer = new Timer(DoWork, null, dueTime, TimeSpan.FromHours(24));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Daily Payment Service is working.");
            _ = _payeeService.Payment(accountId, type);
            _accountService.CheckAccountId(accountId);
            _accountService.ResetTotalDailyTransferLimit();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Daily Payment Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        //Default Parameters
        int accountId = 0;
        PayeeType type = PayeeType.Phone;
    }
}
