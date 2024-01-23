namespace Infrastructure.UOW
{
    public interface IUow
    {
        void SaveChanges();
        Task SaveChangesAsync();
    }
}