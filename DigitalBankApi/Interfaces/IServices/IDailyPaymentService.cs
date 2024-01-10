namespace DigitalBankApi.Interfaces.IServices
{
    public interface IDailyPaymentService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
        void Dispose();
    }
}
