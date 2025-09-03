
namespace SmartBite.Common
{
    public interface IMapper<TIn, TOut>
        where TIn : class
        where TOut : class
    {
        IResult<TOut> Map(TIn value);
    }
}
