namespace ContactManager.Models
{
    public class BaseRepository
    {
        #region Constructors
        public BaseRepository() : this(new AstraEntities())
        {
        }

        public BaseRepository(AstraEntities entities)
        {
            Entities = entities;
        }
        #endregion

        public AstraEntities Entities { get; set; }
    }
}
