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
        #region Queries
        //Due to the fact that the entities are internal, you cannot use the entity class as a return datatype
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        //Methods won't be selected by default by the system but by the developer
        public List<ArtistAlbums> Albums_GetArtistAlbums()
        {
            using (var context = new ChinookSystemContext())
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
                                                        ArtistName = x.Artist.Name,
                                                        ArtistId = x.ArtistId
                                                    };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AlbumItem> Album_List()
        {
            using (var context = new ChinookSystemContext())
            {
                IEnumerable<AlbumItem> results = from x in context.Albums
                                                    select new AlbumItem
                                                    {
                                                        AlbumId = x.AlbumId,
                                                        Title = x.Title,
                                                        ArtistId = x.ArtistId,
                                                        ReleaseYear = x.ReleaseYear,
                                                        ReleaseLabel = x.ReleaseLabel
                                                    };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public AlbumItem Album_FindById(int albumid)
        {
            using (var context = new ChinookSystemContext())
            {
                AlbumItem results = (from x in context.Albums
                                    where x.AlbumId == albumid
                                    select new AlbumItem
                                    {
                                        AlbumId = x.AlbumId,
                                        Title = x.Title,
                                        ArtistId = x.ArtistId,
                                        ReleaseYear = x.ReleaseYear,
                                        ReleaseLabel = x.ReleaseLabel
                                    }).FirstOrDefault();//Find the first instance or return the defalt of the class = null
                return results;
            }
        }
        #endregion

        #region Add, Update, Delete (CRUD)
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Albums_Add(AlbumItem item)
        {
            using (var context = new ChinookSystemContext())
            {
                //Need to move the data from the viemodel class into an entity instance before staging

                //The PK of the Albums table is an Identity() PK. Therefore you do not need to supply the AlbumId value
                Album entityItem = new Album
                {
                    Title = item.Title,
                    ArtistId = item.ArtistId,
                    ReleaseYear = item.ReleaseYear,
                    ReleaseLabel = item.ReleaseLabel
                };

                //Staging is to local memory
                context.Albums.Add(entityItem);

                //At this point, the new PK value is not available
                //The new PK value is created by the database

                //Commit is the action of sending your request to the database for action
                //Also, any validation annotation in yourentity definition classis executed during this command
                context.SaveChanges();
                //since I have an int as the return datatype, I will return the new identity value
                return entityItem.AlbumId;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Albums_Update(AlbumItem item)
        {
            using (var context = new ChinookSystemContext())
            {
                //On update, you need to supply the table's PK value
                Album entityItem = new Album
                {
                    AlbumId = item.AlbumId, //Don't forget the PK
                    Title = item.Title,
                    ArtistId = item.ArtistId,
                    ReleaseYear = item.ReleaseYear,
                    ReleaseLabel = item.ReleaseLabel
                };

                //Staging
                context.Entry(entityItem).State = System.Data.Entity.EntityState.Modified;
                //Commit
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Albums_Delete(AlbumItem item)
        {
            //This method will call the actual delete method and pass it the only needed piece of data: PK
            Albums_Delete(item.AlbumId);
        }

        public void Albums_Delete(int albumid)
        {
            using (var context = new ChinookSystemContext())
            {
                var exists = context.Albums.Find(albumid);
                context.Albums.Remove(exists);
                context.SaveChanges();
            }
        }
        #endregion
    }
}
