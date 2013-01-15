using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestSharp
{
    public partial class RestClient
    {
        public async Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest request) where T : new()
        {
            return (await new TaskFactory().FromAsync<IRestClient, IRestRequest, IRestResponse<T>>(BeginExecuteAsync<T>, EndExecuteAsync<T>, (IRestClient)this, (IRestRequest)request, null));
        }

        public async Task<IRestResponse> ExecuteAsync(IRestRequest request)
        {
            return (await new TaskFactory().FromAsync<IRestClient, IRestRequest, IRestResponse>(BeginExecuteAsync, EndExecuteAsync, (IRestClient)this, (IRestRequest)request, null));
        }

        private IAsyncResult BeginExecuteAsync<T>(IRestClient client, IRestRequest request, AsyncCallback asyncCallback, object state) where T : new()
        {
            var result = new RestResponseAsyncResult<T>();
            client.ExecuteAsync<T>(request,
                r =>
                {
                    result.RestResponse = r;
                    result.IsCompleted = true;
                    asyncCallback(result);
                });
            return result;
        }

        private IRestResponse<T> EndExecuteAsync<T>(IAsyncResult asyncResult) where T : new()
        {
            var restResponseAsyncResult = (RestResponseAsyncResult<T>)asyncResult;

            return restResponseAsyncResult.RestResponse;
        }

        private IAsyncResult BeginExecuteAsync(IRestClient client, IRestRequest request, AsyncCallback asyncCallback, object state)
        {
            var result = new RestResponseAsyncResult();
            client.ExecuteAsync(request,
                r =>
                {
                    result.RestResponse = r;
                    result.IsCompleted = true;
                    asyncCallback(result);
                });
            return result;
        }

        private IRestResponse EndExecuteAsync(IAsyncResult asyncResult)
        {
            var restResponseAsyncResult = (RestResponseAsyncResult)asyncResult;

            return restResponseAsyncResult.RestResponse;
        }
    }

    internal class RestResponseAsyncResult<T> : AsyncResult
    {
        public IRestResponse<T> RestResponse { get; internal set; }
    }

    internal class RestResponseAsyncResult : AsyncResult
    {
        public IRestResponse RestResponse { get; internal set; }
    }

    internal class AsyncResult : IAsyncResult
    {
        public object AsyncState
        {
            get { throw new NotImplementedException(); }
        }

        public WaitHandle AsyncWaitHandle
        {
            get { throw new NotImplementedException(); }
        }

        public bool CompletedSynchronously
        {
            get { return false; }
        }

        public bool IsCompleted { get; internal set; }
    }
}