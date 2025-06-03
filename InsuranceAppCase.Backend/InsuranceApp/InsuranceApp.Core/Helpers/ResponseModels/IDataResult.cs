namespace InsuranceApp.Core.Helpers.ResponseModels
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; }
    }

}
