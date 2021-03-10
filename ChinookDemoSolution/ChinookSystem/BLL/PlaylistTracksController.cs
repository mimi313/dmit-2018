using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Entities;
using ChinookSystem.ViewModels;
using ChinookSystem.DAL;
using System.ComponentModel;
using FreeCode.Exceptions;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        //Class level variable that will hold multiple strings representing any number of error messages
        List<Exception> brokenRules = new List<Exception>();

        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookSystemContext())
            {

                var results = from x in context.PlaylistTracks
                              where x.Playlist.Name.Equals(playlistname) && x.Playlist.UserName.Equals(username)
                              orderby x.TrackNumber
                              select new UserPlaylistTrack
                              {
                                  TrackID = x.TrackId,
                                  TrackNumber = x.TrackNumber,
                                  TrackName = x.Track.Name,
                                  Milliseconds = x.Track.Milliseconds,
                                  UnitPrice = x.Track.UnitPrice
                              };

                return results.ToList();
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid, string song)
        {
            Playlist playlistExists = null;
            PlaylistTrack playlisttrackExists = null;
            int tracknumber = 0;

            using (var context = new ChinookSystemContext())
            {
                //This class is in what is called the Business Logic Layer
                //Business Logic is the rules of your business
                // Rule: a track can only exist once on a playlist
                // Rule: each track on a playlist is assigned a continous track number

                //The BLL method should also ensure that data exists for the processing of the transaction
                if (string.IsNullOrEmpty(playlistname))
                {
                    //There is a data error
                    //Setting up an error message
                    brokenRules.Add(new BusinessRuleException<string>("Playlist name is missing. Unable to add track.", nameof(playlistname), 
                        playlistname));
                }
                else if(string.IsNullOrEmpty(username))
                {
                    brokenRules.Add(new BusinessRuleException<string>("User name was not supplied.", nameof(username),
                        username));
                }
                //else
                //{
                    //Does the playlist exist?
                    playlistExists = (from x in context.Playlists
                                      where (x.Name.Equals(playlistname) && x.UserName.Equals(username))
                                      select x).FirstOrDefault();

                    if (playlistExists == null)
                    {
                        //The playlist does not exist
                        //Tasks:
                        //      Create a new instance of a playlist object
                        //      Load the instance with data
                        //      Stage the add of the new instance (at this moment, the instance isn't on the database)
                        //      Set a variable representing the tracknumber to 1
                        playlistExists = new Playlist()
                                        {
                                            //Instance initializer
                                            Name = playlistname,
                                            UserName = username
                                        };
                        context.Playlists.Add(playlistExists); //Stage only, not yet added to the db
                        tracknumber = 1;
                    }
                    else
                    {
                        //The playlist already exists
                        //Verify track not already on playlist
                        //What is the next tracknumber
                        //Add 1 to the tracknumber
                        playlisttrackExists = (from x in context.PlaylistTracks
                                               where x.Playlist.Name.Equals(playlistname) && x.Playlist.UserName.Equals(username)
                                                    && x.TrackId == trackid
                                               select x).FirstOrDefault();
                        if (playlisttrackExists == null)
                        {
                            tracknumber = (from x in context.PlaylistTracks
                                           where x.Playlist.Name.Equals(playlistname) && x.Playlist.UserName.Equals(username)
                                           select x.TrackNumber).Max();
                            tracknumber++;
                        }
                        else
                        {
                            brokenRules.Add(new BusinessRuleException<string>("Track already on playlist.", nameof(song),
                                            song));
                        }
                    }

                    //Create the playlist track
                    playlisttrackExists = new PlaylistTrack();

                    //Load of the plalist track
                    playlisttrackExists.TrackId = trackid;
                    playlisttrackExists.TrackNumber = tracknumber;

                    //What is the playlistid?
                    //If the playlist exists, then we know the id
                    //But if the playlist is new, we don't know the id. Better said, we don't have an id for the new playlist yet

                    //In the first case the id is known but in the second case, the new record is only staged,
                    //no PK value has been generated yet
                    //If you access the nwe playlist record, the PK would be 0 (default numeric)

                    //The solution to both of these scenarios is to use navigational properties during the actual .Add command
                    //for the new playlisttrack record
                    //The entity framework will, on your beharlf, ensure that the adding of records to the database will be done 
                    //in the appropriate order and will add the missing compound PK value (PlaylistId) to the new playlisttrack record

                    //Not like this. This is wrong
                    //context.PlaylistTracks.Add(playlisttrackExists);

                    //Instead, do the staging using the parent.navproperty.Add(xxx);
                    playlistExists.PlaylistTracks.Add(playlisttrackExists);

                    //Time to commit to SQL
                    //Check: Are there any errors in this transaction?
                    //brokenRules is a List<Exceptions>
                    if(brokenRules.Count() > 0)
                    {
                        //At least one error was recorded during the processing of the transaction
                        throw new BusinessRuleCollectionException("Add Playlist Track Concerens: ", brokenRules);
                    }
                    else
                    {
                        //Commit the transaction
                        //The ALL the staged records to SQL for processing
                        context.SaveChanges();
                    }
                //}

            }
        }//eom
        public void MoveTrack(MoveTrackItem moveTrack)
        {
            using (var context = new ChinookSystemContext())
            {
                //code to go here 

            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookSystemContext())
            {
                if (string.IsNullOrEmpty(playlistname))
                {
                    //There is a data error
                    //Setting up an error message
                    brokenRules.Add(new BusinessRuleException<string>("Playlist name is missing. Unable to add track.", nameof(playlistname),
                        playlistname));
                }
                if (string.IsNullOrEmpty(username))
                {
                    brokenRules.Add(new BusinessRuleException<string>("User name was not supplied.", nameof(username),
                        username));
                }
                if (trackstodelete.Count == 0)
                {
                    brokenRules.Add(new BusinessRuleException<int>("You did not select any tracks to delete", "track count", 0));
                }
                Playlist exists = (from x in context.Playlists
                                   where x.Name.Equals(playlistname) && x.UserName.Equals(username)
                                   select x).FirstOrDefault();
                if (exists == null)
                {
                    brokenRules.Add(new BusinessRuleException<string>("Playlist does not exist", nameof(playlistname), playlistname));
                }
                else
                {
                    //List of all tracks that are to be kept
                    var trackskept = context.PlaylistTracks.Where(tr => tr.Playlist.Name.Equals(playlistname) 
                                                                    && tr.Playlist.UserName.Equals(username)
                                                                    && !trackstodelete.Any(tod => tod == tr.TrackId))//Not a tracks to delete
                                            .OrderBy(tr => tr.TrackNumber).Select(tr => tr);

                    //Removed the desired tracks
                    PlaylistTrack item = null;
                    foreach (int deletetrackid in trackstodelete)
                    {
                        item = context.PlaylistTracks.Where(tr => tr.Playlist.Name.Equals(playlistname)
                                                                    && tr.Playlist.UserName.Equals(username)
                                                                    && tr.TrackId == deletetrackid)
                                            .Select(tr => tr).FirstOrDefault();
                        if (item != null)
                        {
                            //Staged
                            //Pagent.navproperty.Remove(xxx);
                            exists.PlaylistTracks.Remove(item);
                        }
                    }

                    //Re-sequence (re-number) the kept tracks
                    //Option a) use a list and update the records of the list
                    //Option b) delete all children records and re-add only the necessary kept records

                    //Within this example, you will see how to update specific column(s) of a record (Option a)

                    int tracknumber = 1;
                    foreach (var track in trackskept)
                    {
                        track.TrackNumber = tracknumber;
                        context.Entry(track).Property(nameof(PlaylistTrack.TrackNumber)).IsModified = true; //Staged
                        //for the current instance, go to the property tracknumber and flag it as changed
                        tracknumber++;
                    }

                    //Save the wrok
                    if (brokenRules.Count > 0)
                    {
                        throw new BusinessRuleCollectionException("Track Removal Concerns: ", brokenRules);
                    }
                    else
                    {
                        context.SaveChanges();
                    }
                }

            }
        }//eom
    }
}
