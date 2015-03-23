using FastForward.Models;

namespace FastForward.Core.Business
{
    public interface IVmFactory<out T, in TK> where T : IVm where TK : IModel
    {
        T BuildVm(TK model);
    }
}