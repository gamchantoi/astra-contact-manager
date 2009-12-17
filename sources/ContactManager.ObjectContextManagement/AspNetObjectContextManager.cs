using System;
using System.Data.Objects;
using System.Web;

namespace ContactManager.ObjectContextManagement
{
    /// <summary>
    /// Creates one ObjectContext instance per HTTP request. This instance is then
    /// shared during the lifespan of the HTTP request.
    /// </summary>
    public sealed class AspNetObjectContextManager<T> : ObjectContextManager<T> where T : ObjectContext, new()
    {
        private readonly object _lockObject;

        /// <summary>
        /// Returns a shared ObjectContext instance.
        /// </summary>
        public override T ObjectContext
        {
            get
            {
                string ocKey = "ocm_" + HttpContext.Current.GetHashCode().ToString("x");

                lock (_lockObject)
                {
                    if (!HttpContext.Current.Items.Contains(ocKey))
                    {
                        HttpContext.Current.Items.Add(ocKey, new T());

                        System.Diagnostics.Debug.WriteLine("AspNetObjectContextManager: Created new NorthwindObjectContext");
                    }
                }
                return HttpContext.Current.Items[ocKey] as T;
            }
        }

        public AspNetObjectContextManager()
        {
            if (HttpContext.Current == null)
                throw new InvalidOperationException("An AspNetObjectContextManager can only be used in a HTTP context.");

            _lockObject = new object();
        }
    }
}