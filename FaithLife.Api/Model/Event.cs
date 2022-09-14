using FaithLife.Api.Data.Enums;
using System;

namespace FaithLife.Api.Model
{
    public class Event
    {
        public int EventId { get; set; }

        public string EventName { get; set; }

        public string EventDescription { get; set; }

        public DateTime Created { get; set; }

       // public Services Services { get; set; }

        public string Minister { get; set; }


    }
}
