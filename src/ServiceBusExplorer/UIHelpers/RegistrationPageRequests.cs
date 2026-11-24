using System;
using System.Threading.Tasks;

namespace ServiceBusExplorer.UIHelpers
{
    // Called on the owning control's UI thread, including the continuations after loading.
    internal sealed class RegistrationPageRequests<TQuery> where TQuery : struct
    {
        private Request current;

        public RegistrationPageRequests(TQuery initialQuery) => CommittedQuery = initialQuery;

        public TQuery CommittedQuery { get; private set; }
        public bool CanPage => current == null || !current.FirstPage;
        public bool IsLoading => current != null;

        public async Task RunAsync<TPage>(
            string continuationToken,
            TQuery? selection,
            Func<TQuery, string, Task<TPage>> load,
            Action<bool, TPage> apply,
            Func<bool> canApply)
        {
            var firstPage = string.IsNullOrWhiteSpace(continuationToken);
            if (!firstPage && !CanPage)
            {
                return;
            }

            // Refresh and post-create reload belong to the newest selected query,
            // even when its first page has not been committed yet.
            var query = firstPage ? selection ?? current?.Query ?? CommittedQuery : CommittedQuery;
            var request = new Request(query, firstPage);
            current = request;

            try
            {
                var page = await load(query, firstPage ? null : continuationToken);
                if (current != request || !canApply())
                {
                    return;
                }

                apply(firstPage, page);
                if (firstPage)
                {
                    CommittedQuery = query;
                }
            }
            catch (Exception) when (current != request || !canApply())
            {
                // An older result or error must not replace a newer view or show a stale error.
            }
            finally
            {
                if (current == request)
                {
                    current = null;
                }
            }
        }

        private sealed class Request
        {
            public Request(TQuery query, bool firstPage)
            {
                Query = query;
                FirstPage = firstPage;
            }

            public TQuery Query { get; }
            public bool FirstPage { get; }
        }
    }
}
