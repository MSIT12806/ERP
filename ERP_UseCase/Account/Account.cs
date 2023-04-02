namespace UseCase
{
    public class Account
    {
        public Account(string account)
        {
            Initialize();
            AccountID = account;
        }


        public readonly string AccountID;

        public AccountToken Login(string id)
        {
            if (_db.Match(id))
            {

            }

            return null;
        }
        private void Initialize()
        {
            //TODO 所有要初始化的東東。
            throw new NotImplementedException();
        }
        IAccountPersistent _db;
    }
}