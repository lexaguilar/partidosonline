using System;
using System.Collections.Generic;

namespace OnlineFutbol.Models
{
    public partial class Matches
    {
        public int Id { get; set; }
        public int TeamHomeId { get; set; }
        public int TeamAwayId { get; set; }
        public DateTime EventDate { get; set; }
        public string Url { get; set; }
        public int Priority { get; set; }
        public bool? IsLive { get; set; }

        public virtual Teams TeamAway { get; set; }
        public virtual Teams TeamHome { get; set; }
    }

    public class ViewMatch{
         public int Id { get; set; }
        public string TeamHome { get; set; }
        public string TeamAway { get; set; }
        public DateTime EventDate { get; set; }
        public string Url { get; set; }
        public int Priority { get; set; }
        public bool IsLive { get; set; }
    }
}
