using System;

namespace OnlineFutbol.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
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
