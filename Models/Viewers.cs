using System;
using System.Collections.Generic;

namespace OnlineFutbol.Models
{
    public partial class Viewers
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public string Uid { get; set; }
        public DateTime Added { get; set; }
    }
}
