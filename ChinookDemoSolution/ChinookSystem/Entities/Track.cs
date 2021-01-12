using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion

namespace ChinookSystem.Entities
{
    [Table("Tracks")]
    internal class Track
    {
        private int? _AlbumId;
        private int? _GenreId;
        private string _Composer;
        private int? _Bytes;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]//Identity Specification = No
        public int TrackId { get; set; }

        [Required(ErrorMessage ="Track name is required.")]
        [StringLength(200, ErrorMessage = "Track name is limited to 200 characters.")]
        public string Name { get; set; }

        public int? AlbumId { get; set; }

        public int MediaTypeId { get; set; }

        public int? GenreId { get; set; }

        [StringLength(220, ErrorMessage = "Composer is limited to 220 characters.")]
        public string Composer
        {
            get { return _Composer; }
            set { _Composer = string.IsNullOrEmpty(value) ? null : value; }
        }

        public int Milliseconds { get; set; }

        public int? Bytes { get; set; }

        public decimal UnitPrice { get; set; }//numeric on db


        //navigational propeties
        //child to parent (many to one)
        public virtual Album Album { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual MediaType MediaType { get; set; }
    }
}
