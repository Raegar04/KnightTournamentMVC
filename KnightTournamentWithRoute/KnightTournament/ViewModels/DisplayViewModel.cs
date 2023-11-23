namespace KnightTournament.ViewModels
{
    public class DisplayViewModel<TEntity> 
    {
        public ICollection<TEntity> Entities { get; set; } = new List<TEntity>();

        public List<string> SearchItems { get; set; }

        public string SelectedSearchOption { get; set; }

        public string SearchString { get; set; }
    }
}
