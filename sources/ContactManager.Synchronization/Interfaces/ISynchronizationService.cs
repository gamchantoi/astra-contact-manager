namespace ContactManager.Synchronization.Interfaces
{
    public interface ISynchronizationService
    {
        bool SyncToHost();
        bool SyncFromHost();
    }
}