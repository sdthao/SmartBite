
namespace SmartBite.Common
{
    public interface IFactory<TOut> 
        where TOut : class
    {
        IResult<TOut> Create();
    }

    public interface IFactory<TIn, TOut>
        where TIn : class
        where TOut : class
    {
        IResult<TOut> Create(TIn value);
    }
}
