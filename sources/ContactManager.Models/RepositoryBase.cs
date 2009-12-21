using System;
using System.Web;

namespace ContactManager.Models
{
    public abstract class RepositoryBase<T> where T : System.Data.Objects.DataClasses.EntityObject
    {
        private readonly object _lockObject;

        protected RepositoryBase()
        {
            if (HttpContext.Current == null)
                throw new InvalidOperationException("An AspNetObjectContextManager can only be used in a HTTP context.");

            _lockObject = new object();
        }

        protected internal AstraEntities ObjectContext
        {
            get
            {
                var ocKey = "ocm_" + HttpContext.Current.GetHashCode().ToString("x");

                lock (_lockObject)
                {
                    if (HttpContext.Current.Session[ocKey] == null)
                    {
                        HttpContext.Current.Session.Add(ocKey, new AstraEntities());

                        //System.Diagnostics.Debug.WriteLine("AspNetObjectContextManager: Created new AstraObjectContext");
                    }
                }
                return HttpContext.Current.Session[ocKey] as AstraEntities;
            }
        }

        //private void InstantiateObjectContextManager()
        //{
        //    /* Retrieve ObjectContextManager configuration settings: */
        //    var ocManagerConfiguration = ConfigurationManager.GetSection("ContactManager.Models.ObjectContext") as Hashtable;
            
        //    if (ocManagerConfiguration == null || !ocManagerConfiguration.ContainsKey("managerType"))
        //        throw new ConfigurationErrorsException(
        //            "A ContactManager.Models.ObjectContext tag or its managerType attribute is missing in the configuration.");
            
        //    var managerTypeName = ocManagerConfiguration["managerType"] as string;
        //    if (string.IsNullOrEmpty(managerTypeName))
        //        throw new ConfigurationErrorsException("The managerType attribute is empty.");
            
        //    managerTypeName = managerTypeName.Trim().ToLower();

        //    try
        //    {
        //        /* Try to create a type based on it's name: */
        //        var frameworkAssembly = Assembly.GetAssembly(typeof (ObjectContextManager<>));
        //        /* We have to fix the name, because its a generic class: */
        //        var managerType = frameworkAssembly.GetType(managerTypeName + "`1", true, true);
        //        managerType = managerType.MakeGenericType(typeof (AstraEntities));

        //        /* Try to create a new instance of the specified ObjectContextManager type: */
        //        ObjectContextManager =
        //            Activator.CreateInstance(managerType) as ObjectContextManager<AstraEntities>;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ConfigurationErrorsException(
        //            "The managerType specified in the configuration is not valid.", e);
        //    }
        //}

        //public virtual void Add(T newObject)
        //{
        //    ObjectContext.AddObject(newObject.GetType().Name, newObject);
        //}
    }
}
