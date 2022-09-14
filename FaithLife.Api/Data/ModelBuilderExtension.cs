using FaithLife.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace FaithLife.Api.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasData(

                new Event
                {
                    EventId = 1,
                    EventName = "The Holy spirit",
                    EventDescription = "God will send his Holy spirit to you",
                    Minister = "Pastor Abraham",
                    //Services = Enums.Services.FaithClinic,
                },
                 new Event
                 {
                     EventId = 2,
                     EventName = "The Holy spirit",
                     EventDescription = "God will send his Holy spirit to you",
                     Minister = "Pastor Abraham",
                     //Services = Enums.Services.FaithClinic,
                 },

                  new Event
                  {
                      EventId = 3,
                      EventName = "The Holy spirit",
                      EventDescription = "God will send his Holy spirit to you",
                      Minister = "Pastor Abraham",
                      //Services = Enums.Services.FaithClinic,
                  }


                );
        }
    }
}
