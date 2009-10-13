using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactManager.Models
{
    public class BaseRepository
    {
        private AstraEntities _entities;

        #region Constructors
        public BaseRepository() : this(new AstraEntities())
        {
        }

        public BaseRepository(AstraEntities entities)
        {
            _entities = entities;
        }
        #endregion

        public AstraEntities Entities
        {
            get
            {
                return _entities;
            }
            set
            {
                _entities = value;
            }
        }
    }
}
