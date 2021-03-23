namespace FSISSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class Player
    {
        private string _MedicalAlertDetails;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Player()
        {
            PlayerStats = new HashSet<PlayerStat>();
        }

        public int PlayerID { get; set; }

        public int GuardianID { get; set; }

        public int TeamID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public int Age { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        [Required]
        [StringLength(10)]
        public string AlbertaHealthCareNumber { get; set; }

        [StringLength(250)]
        public string MedicalAlertDetails
        {
            get { return _MedicalAlertDetails; }
            set { _MedicalAlertDetails = string.IsNullOrEmpty(value) ? null : value; }
        }

        public int GamesPlayed { get; set; }

        public virtual Guardian Guardian { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlayerStat> PlayerStats { get; set; }

        public virtual Team Team { get; set; }
    }
}
