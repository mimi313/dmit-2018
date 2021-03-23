namespace FSISSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class PlayerStat
    {
        [Key]
        public int PlayerStatsID { get; set; }

        public int GameID { get; set; }

        public int PlayerID { get; set; }

        public int Goals { get; set; }

        public int Assists { get; set; }

        public bool YellowCard { get; set; }

        public bool RedCard { get; set; }

        public virtual Game Game { get; set; }

        public virtual Player Player { get; set; }
    }
}
