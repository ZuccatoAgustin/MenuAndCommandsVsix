namespace Microsoft.Samples.VisualStudio.MenuCommands
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelRofex : DbContext
    {
        public ModelRofex()
            : base("data source = serverdevnet2; initial catalog = Rofex_WebSite; persist security info=True;user id = sa; password=Pa$$w0rd;MultipleActiveResultSets=True;App=EntityFramework")
        {
            
        }

        public virtual DbSet<Traducciones> Traducciones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
