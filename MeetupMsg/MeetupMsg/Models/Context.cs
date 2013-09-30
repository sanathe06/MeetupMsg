using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MeetupMsg.Models
    {
    public class Context : DbContext
        {
        public DbSet<StoredCredential> StoredCredentials { get; set; }

        public Context()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        }
    }