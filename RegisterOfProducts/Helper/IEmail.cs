namespace RegisterOfProducts.Helper
{
    public interface IEmail
    {
        bool Sent(string email, string topic, string message);
    }
}
