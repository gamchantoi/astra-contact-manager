using System.Data.Objects;
using ContactManager.ObjectContextManagement;

namespace ContactManager.ObjectContextManagement
{
    /// <summary>
    /// Maintains an ObjectContext instance in static field. This instance is then
    /// shared during the lifespan of the AppDomain.
    /// </summary>
    public sealed class StaticObjectContextManager<T> : ObjectContextManager<T> where T : ObjectContext, new()
    {
        private static T _objectContext;
        private static readonly object _lockObject = new object();

        /// <summary>
        /// Returns a shared NorthwindObjectContext instance.
        /// </summary>
        public override T ObjectContext
        {
            get
            {
                lock (_lockObject)
                {
                    if (_objectContext == null)
                        _objectContext = new T();
                }

                return _objectContext;
            }
        }
    }
}