using System.Data.Objects;
using ContactManager.ObjectContextManagement;

namespace ContactManager.ObjectContextManagement
{
    public sealed class ScopedObjectContextManager<T> : ObjectContextManager<T> where T : ObjectContext, new()
    {
        private T _objectContext;

        /// <summary>
        /// Returns the ObjectContext instance that belongs to the current ObjectContextScope.
        /// If currently no ObjectContextScope exists, a local instance of an ObjectContext 
        /// class is returned.
        /// </summary>
        public override T ObjectContext
        {
            get
            {
                if (ObjectContextScope<T>.CurrentObjectContext != null)
                    return ObjectContextScope<T>.CurrentObjectContext;
                else
                {
                    if (_objectContext == null)
                        _objectContext = new T();

                    return _objectContext;
                }
            }
        }
    }
}