namespace Microsoft.Samples.VisualStudio.MenuCommands
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Traducciones
    {
        public int ID { get; set; }

        public int? LenguageID { get; set; }

        [StringLength(300)]
        public string KEY { get; set; }

        public string Value { get; set; }

        [StringLength(200)]
        public string Pantalla { get; set; }
    }
}
