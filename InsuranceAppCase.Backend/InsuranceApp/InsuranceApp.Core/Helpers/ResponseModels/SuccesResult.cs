namespace InsuranceApp.Core.Helpers.ResponseModels
{
    public class SuccesResult : Result, IResult
    {
        public SuccesResult(string message) : base(true, message)
        {

        }
        public SuccesResult() : base(true)
        {

        }

    }

}
