using StarNet.Bl;

namespace StarNet.Services
{
    public class BaseDbAccessService : BaseService
    {
        public UnitOfWork UnitOfWork { get; protected set; }

        public int UserId { get; protected set; } = 0;

        public BaseDbAccessService()
            : base()
        {
            UnitOfWork = new UnitOfWork();
        }
    }
}
