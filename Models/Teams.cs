using System;
using System.Collections.Generic;

namespace OnlineFutbol.Models
{
    public partial class Teams
    {
        public Teams()
        {
            MatchesTeamAway = new HashSet<Matches>();
            MatchesTeamHome = new HashSet<Matches>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }

        public virtual ICollection<Matches> MatchesTeamAway { get; set; }
        public virtual ICollection<Matches> MatchesTeamHome { get; set; }
    }
}
