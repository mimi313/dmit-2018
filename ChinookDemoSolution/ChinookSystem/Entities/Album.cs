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
    [Table("Albums")]
    internal class Album
    {
        private string _ReleaseLabel;
        private int _ReleaseYear;

        [Key]
        public int AlbumId { get; set; }

        [Required(ErrorMessage ="Album title is required")]
        [StringLength(160, ErrorMessage = "Release Label is limited to 160 characters.")]
        public string Title { get; set; }

        //You don't need [ForeignKey] because the FK name is the same as the PK on the parent table
        //It always has a value, so you don't need [Required] either
        public int ArtistId { get; set; }

        //This is a required field, and the default value is 0
        //If this is a non-required field, then it would be "int?"
        public int ReleaseYear
        {
            get { return _ReleaseYear; }
            set
            {
                if (_ReleaseYear < 1950 || _ReleaseYear > DateTime.Today.Year)
                {
                    throw new Exception("Album release year is before 1950 or a year in the future.");
                }
                else
                {
                    _ReleaseYear = value;
                }
            }
        }

        [StringLength(50, ErrorMessage = "Release Label is limited to 50 characters.")]
        public string ReleaseLabel 
        {
            get { return _ReleaseLabel; }
            set { _ReleaseLabel = string.IsNullOrEmpty(value) ? null : value; }
        }

        //You can still use [NotMapped] annotations

        //Navigational properties
        //classInstanceName.propertyName.fieldPropertyName

        //Many to 1 relationship
        //Create the 1 end of the relationship in this entity, singular because an album belongs to an artist
        public virtual Artist Artist { get; set; }//the first Artist is return datatype (class name), the second is the instance name
        
        //Not valid until the Track entity is coded
        //public virtual ICollection<Track> Tracks { get; set; }
    }
}
