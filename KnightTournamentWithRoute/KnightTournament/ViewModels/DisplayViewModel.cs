namespace KnightTournament.ViewModels
{
    public class DisplayViewModel<TEntity> 
    {
        public ICollection<TEntity> Entities { get; set; } = new List<TEntity>();
    }
}
