using System.Data.Objects;

namespace ContactManager.ObjectContextManagement
{
    public abstract class ObjectContextManager<T> where T : ObjectContext, new()
    {
        /// <summary>
        /// Returns a reference to an ObjectContext instance.
        /// </summary>
        public abstract T ObjectContext
        {
            get;
        }
    }
}