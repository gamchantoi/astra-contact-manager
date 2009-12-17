using System;
using System.Collections;
using System.Configuration;
using System.Reflection;
using ContactManager.ObjectContextManagement;

namespace ContactManager.Models
{
    public abstract class RepositoryBase<T> where T : System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Returns the current ObjectContextManager instance. Encapsulated the 
        /// _objectContextManager field to show it as an association on the class diagram.
        /// </summary>
        private ObjectContextManager<AstraEntities> ObjectContextManager { get; set; }

        /// <summary>
        /// Returns a NorthwindObjectContext object. 
        /// </summary>
        protected internal AstraEntities ObjectContext
        {
            get
            {
                if (ObjectContextManager == null)
                    InstantiateObjectContextManager();

                return ObjectContextManager.ObjectContext;
            }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RepositoryBase()
        { }

        /// <summary>
        /// Instantiates a new ObjectContextManager based on application configuration settings.
        /// </summary>
        private void InstantiateObjectContextManager()
        {
            /* Retrieve ObjectContextManager configuration settings: */
            var ocManagerConfiguration = ConfigurationManager.GetSection("ContactManager.Models.ObjectContext") as Hashtable;
            
            if (ocManagerConfiguration == null || !ocManagerConfiguration.ContainsKey("managerType"))
                throw new ConfigurationErrorsException(
                    "A ContactManager.Models.ObjectContext tag or its managerType attribute is missing in the configuration.");
            
            var managerTypeName = ocManagerConfiguration["managerType"] as string;
            if (string.IsNullOrEmpty(managerTypeName))
                throw new ConfigurationErrorsException("The managerType attribute is empty.");
            
            managerTypeName = managerTypeName.Trim().ToLower();

            try
            {
                /* Try to create a type based on it's name: */
                var frameworkAssembly = Assembly.GetAssembly(typeof (ObjectContextManager<>));
                /* We have to fix the name, because its a generic class: */
                var managerType = frameworkAssembly.GetType(managerTypeName + "`1", true, true);
                managerType = managerType.MakeGenericType(typeof (AstraEntities));

                /* Try to create a new instance of the specified ObjectContextManager type: */
                ObjectContextManager =
                    Activator.CreateInstance(managerType) as ObjectContextManager<AstraEntities>;
            }
            catch (Exception e)
            {
                throw new ConfigurationErrorsException(
                    "The managerType specified in the configuration is not valid.", e);
            }
        }

        /// <summary>
        /// Persists all changes to the underlying datastore.
        /// </summary>
        public void SaveAllObjectChanges()
        {
            ObjectContext.SaveChanges();
        }

        /// <summary>
        /// Adds a new entity object to the context.
        /// </summary>
        /// <param name="newObject">A new object.</param>
        public virtual void Add(T newObject)
        {
            ObjectContext.AddObject(newObject.GetType().Name, newObject);
        }
        /// <summary>
        /// Deletes an entity object.
        /// </summary>
        /// <param name="obsoleteObject">An obsolete object.</param>
        public virtual void Delete(T obsoleteObject)
        {
            ObjectContext.DeleteObject(obsoleteObject);
        }
    }
}
