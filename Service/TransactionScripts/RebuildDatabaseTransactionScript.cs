using Service.DAL;

namespace Service.TransactionScripts
{
    public class RebuildDatabaseTransactionScript
    {
        public void RebuildDatabase(TodoContext context)
        {
            var database = context.Database;
            if (database.Exists())
            {
                database.Delete();
            }

            database.Create();
        }
    }
}