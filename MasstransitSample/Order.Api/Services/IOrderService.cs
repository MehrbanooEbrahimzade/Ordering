namespace Order.Api.Services
{
    public interface IOrderService
    {
        public Task<bool> Post(OrderCommand command);
    }
}
