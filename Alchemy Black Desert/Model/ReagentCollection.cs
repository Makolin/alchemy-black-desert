using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace Alchemy_Black_Desert.Model
{
    public class ReagentCollection
    {
        public static ObservableCollection<Recipe> Recipes { get; set; }
        public ReagentCollection()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Recipes = new ObservableCollection<Recipe>(db.Recipes
                    .Include(t => t.Potion)
                    .ToList());
            }
        }
    }
}
