namespace EShop.Handlers
{
    public interface ITransactionProvider
    {
        void Save();
        TransactionProvider.ITransaction GetTransaction();
    }
}