namespace TaeyeonFanManagerSite.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TaeyeonFanManagerSite.DAL;
    using TaeyeonFanManagerSite.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TaeyeonFanManagerSite.DAL.TeamContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TaeyeonFanManagerSite.DAL.TeamContext context)
        {
            var fans = new List<Fan>
            {
                new Fan { FirstMidName = "Phạm Hoàng",   LastName = "Kiên", 
                    JoinedDate = DateTime.Parse("2010-09-01") },
                new Fan { FirstMidName = "Phạm Quang", LastName = "Hưng",    
                    JoinedDate = DateTime.Parse("2012-09-01") },
                new Fan { FirstMidName = "Trần Huy",   LastName = "Hoàng",     
                    JoinedDate = DateTime.Parse("2013-09-01") },
                new Fan { FirstMidName = "Nguyễn Văn",    LastName = "Thái", 
                    JoinedDate = DateTime.Parse("2012-09-01") },
                new Fan { FirstMidName = "Yan",      LastName = "Li",        
                    JoinedDate = DateTime.Parse("2012-09-01") },
                new Fan { FirstMidName = "Trần Thùy",    LastName = "Linh",   
                    JoinedDate = DateTime.Parse("2011-09-01") },
                new Fan { FirstMidName = "Lê Thanh",    LastName = "Tuấn",    
                    JoinedDate = DateTime.Parse("2013-09-01") },
                new Fan { FirstMidName = "Phan Mạnh",     LastName = "Tùng",  
                    JoinedDate = DateTime.Parse("2009-08-11") }
            };
            fans.ForEach(s => context.Fans.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var idols = new List<Idol>
            {
                new Idol { FirstMidName = "Kim",     LastName = "Taeyeon", 
                    MeetDate = DateTime.Parse("2015-03-11") },
                new Idol { FirstMidName = "Im",    LastName = "Yoona",    
                    MeetDate = DateTime.Parse("2016-07-06") },
                new Idol { FirstMidName = "Kwon",   LastName = "Yuri",       
                    MeetDate = DateTime.Parse("2015-07-01") },
                new Idol { FirstMidName = "Park", LastName = "Sooyoung",      
                    MeetDate = DateTime.Parse("2014-01-15") },
                new Idol { FirstMidName = "Lee",   LastName = "Sunny",      
                    MeetDate = DateTime.Parse("2016-02-12") }
            };
            idols.ForEach(s => context.Idols.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var concepts = new List<Concept>
            {
                new Concept { Name = "Mr Mr",     Budget = 350000, 
                    StartDate = DateTime.Parse("2007-09-01"), 
                    IdolID  = idols.Single( i => i.LastName == "Taeyeon").ID },
                new Concept { Name = "I Got A Boy", Budget = 100000, 
                    StartDate = DateTime.Parse("2007-09-01"), 
                    IdolID  = idols.Single( i => i.LastName == "Yoona").ID },
                new Concept { Name = "Party", Budget = 350000, 
                    StartDate = DateTime.Parse("2007-09-01"), 
                    IdolID  = idols.Single( i => i.LastName == "Yuri").ID },
                new Concept { Name = "Lion Heart",   Budget = 100000, 
                    StartDate = DateTime.Parse("2007-09-01"), 
                    IdolID  = idols.Single( i => i.LastName == "Sooyoung").ID }
            };
            concepts.ForEach(s => context.Concepts.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var offlines = new List<Offline>
            {
                new Offline {OfflineID = 1050, Title = "LoveSoShi",      TicketPrices = 300000,
                  ConceptID = concepts.Single( s => s.Name == "Party").ConceptID,
                  Idols = new List<Idol>() 
                },
                new Offline {OfflineID = 4022, Title = "LiveWithSNSD", TicketPrices = 300000,
                  ConceptID = concepts.Single( s => s.Name == "Lion Heart").ConceptID,
                  Idols = new List<Idol>() 
                },
                new Offline {OfflineID = 4041, Title = "Tell Me Yours Wish", TicketPrices = 300000,
                  ConceptID = concepts.Single( s => s.Name == "Lion Heart").ConceptID,
                  Idols = new List<Idol>() 
                },
                new Offline {OfflineID = 1045, Title = "Reflection",       TicketPrices = 400000,
                  ConceptID = concepts.Single( s => s.Name == "I Got A Boy").ConceptID,
                  Idols = new List<Idol>() 
                },
                new Offline {OfflineID = 3141, Title = "Love Is The Moment",   TicketPrices = 400000,
                  ConceptID = concepts.Single( s => s.Name == "I Got A Boy").ConceptID,
                  Idols = new List<Idol>() 
                },
                new Offline {OfflineID = 2021, Title = "Buffterfly Kiss",    TicketPrices = 300000,
                  ConceptID = concepts.Single( s => s.Name == "Mr Mr").ConceptID,
                  Idols = new List<Idol>() 
                },
                new Offline {OfflineID = 2042, Title = "I Love Taeyeon",     TicketPrices = 400000,
                  ConceptID = concepts.Single( s => s.Name == "Mr Mr").ConceptID,
                  Idols = new List<Idol>() 
                },
            };
            offlines.ForEach(s => context.Offlines.AddOrUpdate(p => p.OfflineID, s));
            context.SaveChanges();

            var fanSigns = new List<FanSign>
            {
                new FanSign { 
                    IdolID = idols.Single( i => i.LastName == "Yoona").ID, 
                    Location = "Seoul 15" },
                new FanSign { 
                    IdolID = idols.Single( i => i.LastName == "Yuri").ID, 
                    Location = "Tokyo 20" },
                new FanSign { 
                    IdolID = idols.Single( i => i.LastName == "Sooyoung").ID, 
                    Location = "Hà Nội 04" },
            };
            fanSigns.ForEach(s => context.FanSigns.AddOrUpdate(p => p.IdolID, s));
            context.SaveChanges();

            AddOrUpdateIdol(context, "LoveSoShi", "Sooyoung");
            AddOrUpdateIdol(context, "LoveSoShi", "Yuri");
            AddOrUpdateIdol(context, "LiveWithSNSD", "Sunny");
            AddOrUpdateIdol(context, "Tell Me Yours Wish", "Sunny");

            AddOrUpdateIdol(context, "Reflection", "Yoona");
            AddOrUpdateIdol(context, "Love Is The Moment", "Yuri");
            AddOrUpdateIdol(context, "Buffterfly Kiss", "Taeyeon");
            AddOrUpdateIdol(context, "I Love Taeyeon", "Taeyeon");

            context.SaveChanges();


            var joineddates = new List<JoinedDate>
            {
                new JoinedDate { 
                    FanID = fans.Single(s => s.LastName == "Kiên").ID, 
                    OfflineID = offlines.Single(c => c.Title == "LoveSoShi" ).OfflineID, 
                    Rank = Rank.A 
                },
                 new JoinedDate { 
                    FanID = fans.Single(s => s.LastName == "Kiên").ID,
                    OfflineID = offlines.Single(c => c.Title == "LiveWithSNSD" ).OfflineID, 
                    Rank = Rank.C 
                 },                            
                 new JoinedDate { 
                    FanID = fans.Single(s => s.LastName == "Kiên").ID,
                    OfflineID = offlines.Single(c => c.Title == "Tell Me Yours Wish" ).OfflineID, 
                    Rank = Rank.B
                 },
                 new JoinedDate { 
                     FanID = fans.Single(s => s.LastName == "Hưng").ID,
                    OfflineID = offlines.Single(c => c.Title == "Reflection" ).OfflineID, 
                    Rank = Rank.B 
                 },
                 new JoinedDate { 
                     FanID = fans.Single(s => s.LastName == "Hưng").ID,
                    OfflineID = offlines.Single(c => c.Title == "Love Is The Moment" ).OfflineID, 
                    Rank = Rank.B 
                 },
                 new JoinedDate {
                    FanID = fans.Single(s => s.LastName == "Hưng").ID,
                    OfflineID = offlines.Single(c => c.Title == "Buffterfly Kiss" ).OfflineID, 
                    Rank = Rank.B 
                 },
                 new JoinedDate { 
                    FanID = fans.Single(s => s.LastName == "Hoàng").ID,
                    OfflineID = offlines.Single(c => c.Title == "LoveSoShi" ).OfflineID
                 },
                 new JoinedDate { 
                    FanID = fans.Single(s => s.LastName == "Hoàng").ID,
                    OfflineID = offlines.Single(c => c.Title == "LiveWithSNSD").OfflineID,
                    Rank = Rank.B         
                 },
                new JoinedDate { 
                    FanID = fans.Single(s => s.LastName == "Thái").ID,
                    OfflineID = offlines.Single(c => c.Title == "LoveSoShi").OfflineID,
                    Rank = Rank.B         
                 },
                 new JoinedDate { 
                    FanID = fans.Single(s => s.LastName == "Li").ID,
                    OfflineID = offlines.Single(c => c.Title == "Buffterfly Kiss").OfflineID,
                    Rank = Rank.B         
                 },
                 new JoinedDate { 
                    FanID = fans.Single(s => s.LastName == "Linh").ID,
                    OfflineID = offlines.Single(c => c.Title == "I Love Taeyeon").OfflineID,
                    Rank = Rank.B         
                 }
            };

            foreach (JoinedDate e in joineddates)
            {
                var joineddateInDataBase = context.JoinedDates.Where(
                    s =>
                         s.Fan.ID == e.FanID &&
                         s.Offline.OfflineID == e.OfflineID).SingleOrDefault();
                if (joineddateInDataBase == null)
                {
                    context.JoinedDates.Add(e);
                }
            }
            context.SaveChanges();
        }

        void AddOrUpdateIdol(TeamContext context, string offlineTitle, string idolName)
        {
            var off = context.Offlines.SingleOrDefault(c => c.Title == offlineTitle);
            var inst = off.Idols.SingleOrDefault(i => i.LastName == idolName);
            if (inst == null)
                off.Idols.Add(context.Idols.Single(i => i.LastName == idolName));
        }
    }
}
