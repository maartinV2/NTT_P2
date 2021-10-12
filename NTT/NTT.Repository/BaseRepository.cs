using System;

namespace NTT.Repository
{
    public class RepositoryBase : IDisposable
    {
        public DatabaseConnection _db { get; }

        public RepositoryBase(string connectionString)
        {
            _db = new DatabaseConnection(connectionString);
        }

        public RepositoryBase(DatabaseConnection db)
        {
            _db = new DatabaseConnection(db);
        }

       

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }

        ~RepositoryBase()
        {
            Dispose(false);
        }
        #endregion
    }
}