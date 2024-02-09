namespace Domain.Entity.EntityHelper
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
        void BringBack();
        void Delete();
    }
}
