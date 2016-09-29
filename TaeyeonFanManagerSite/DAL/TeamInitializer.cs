using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaeyeonFanManagerSite.Models;

namespace TaeyeonFanManagerSite.DAL
{
    public class TeamInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TeamContext>
    {
        protected override void Seed(TeamContext context)
        {
            var fans = new List<Fan>
            {
            new Fan{FirstMidName="Phạm Hoàng",LastName="Kiên",JoinedDate=DateTime.Parse("2009-09-01")},
            new Fan{FirstMidName="Phạm Quang",LastName="Hưng",JoinedDate=DateTime.Parse("2012-09-01")},
            new Fan{FirstMidName="Trần Huy",LastName="Hoàng",JoinedDate=DateTime.Parse("2013-09-01")},
            new Fan{FirstMidName="Nguyễn Văn",LastName="Thái",JoinedDate=DateTime.Parse("2012-09-01")},
            new Fan{FirstMidName="Yan",LastName="Li",JoinedDate=DateTime.Parse("2012-09-01")},
            new Fan{FirstMidName="Trần Thùy",LastName="Linh",JoinedDate=DateTime.Parse("2011-09-01")},
            new Fan{FirstMidName="Lê Thanh",LastName="Tuấn",JoinedDate=DateTime.Parse("2013-09-01")},
            new Fan{FirstMidName="Phan Mạnh",LastName="Tùng",JoinedDate=DateTime.Parse("2007-09-01")}
            };

            fans.ForEach(s => context.Fans.Add(s));
            context.SaveChanges();

            var offlines = new List<Offline>
            {
            new Offline{OfflineID=1050,Title="LoveSoShi",TicketPrices=300000,},
            new Offline{OfflineID=4022,Title="LiveWithSNSD",TicketPrices=300000,},
            new Offline{OfflineID=4041,Title="Tell Me Yours Wish",TicketPrices=300000,},
            new Offline{OfflineID=1045,Title="Reflection",TicketPrices=400000,},
            new Offline{OfflineID=3141,Title="Love Is The Moment",TicketPrices=400000,},
            new Offline{OfflineID=2021,Title="Buffterfly Kiss",TicketPrices=300000,},
            new Offline{OfflineID=2042,Title="I Love Taeyeon",TicketPrices=400000,}
            };

            offlines.ForEach(s => context.Offlines.Add(s));
            context.SaveChanges();

            var joineddates = new List<JoinedDate>
            {
            new JoinedDate{FanID=1,OfflineID=1050,Rank=Rank.A},
            new JoinedDate{FanID=1,OfflineID=4022,Rank=Rank.C},
            new JoinedDate{FanID=1,OfflineID=4041,Rank=Rank.B},
            new JoinedDate{FanID=2,OfflineID=1045,Rank=Rank.B},
            new JoinedDate{FanID=2,OfflineID=3141,Rank=Rank.S},
            new JoinedDate{FanID=2,OfflineID=2021,Rank=Rank.S},
            new JoinedDate{FanID=3,OfflineID=1050},
            new JoinedDate{FanID=4,OfflineID=1050,},
            new JoinedDate{FanID=4,OfflineID=4022,Rank=Rank.S},
            new JoinedDate{FanID=5,OfflineID=4041,Rank=Rank.C},
            new JoinedDate{FanID=6,OfflineID=1045},
            new JoinedDate{FanID=7,OfflineID=3141,Rank=Rank.A},
            };
            joineddates.ForEach(s => context.JoinedDates.Add(s));
            context.SaveChanges();
        }
    }
}