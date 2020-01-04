using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lucky.Models
{
    public class DatabaseContext:DbContext
    {
        public DbSet<UserRegister> UserRegisters { get; set; }

        public System.Data.Entity.DbSet<Lucky.Models.AddWinningNumber> AddWinningNumbers { get; set; }

        public System.Data.Entity.DbSet<Lucky.Models.PrizeDetail> PrizeDetails { get; set; }

        public System.Data.Entity.DbSet<Lucky.Models.WinnerList> WinnerLists { get; set; }
    }
}