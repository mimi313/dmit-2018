namespace FSISSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class Game
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Game()
        {
            PlayerStats = new HashSet<PlayerStat>();
        }

        public int GameID { get; set; }

        public DateTime GameDate { get; set; }

        public int HomeTeamID { get; set; }

        public int VisitingTeamID { get; set; }

        public int HomeTeamScore { get; set; }

        public int VisitingTeamScore { get; set; }

        public virtual Team Team { get; set; }

        public virtual Team Team1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlayerStat> PlayerStats { get; set; }
    }
}
