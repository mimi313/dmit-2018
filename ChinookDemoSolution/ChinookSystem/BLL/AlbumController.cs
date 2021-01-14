using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Entities;
using ChinookSystem.DAL;
using ChinookSystem.ViewModels;
using System.ComponentModel; // for ODS
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class AlbumController//public
    {
        //Due to the fact that the entities are internal, you cannot use the entity class as a return datatype
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        //Methods won't be selected by default by the system but by the developer
        public List<ArtistAlbums> Albums_GetArtistAlbums()
        {
            using(var context = new ChinookSystemContext())
            {
                //Linq to Entity
                IEnumerable<ArtistAlbums> results = from x in context.Albums
                                                    select new ArtistAlbums //creating a new record
                                                    {
                                                        Title = x.Title,
                                                        ReleaseYear = x.ReleaseYear,
                                                        ArtistName = x.Artist.Name
                                                    };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ArtistAlbums> Albums_GetAlbumsForArtist(int artistid)
        {
            using (var context = new ChinookSystemContext())
            {
                IEnumerable<ArtistAlbums> results = from x in context.Albums
                                                    where x.ArtistId == artistid
                                                    select new ArtistAlbums
                                                    {
                                                        Title = x.Title,
                                                        ReleaseYear = x.ReleaseYear,
                                                        ArtistName = x.Artist.Name
                                                    };
                return results.ToList();
            }
        }
    }
}
