using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace Alchemy_Black_Desert.Model
{
    public class ReagentCollection
    {
        public static ObservableCollection<Potion> Potions { get; set; }
        public ReagentCollection()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Potions = new ObservableCollection<Potion>(db.Potions
                    .Include(t => t.Type)
                    .ToList());
            }
        }
    }
}
