using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using ChinookSystem.ViewModels;
using WebApp.Security;

#endregion

namespace WebApp.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;

            //Test our security
            //Are you logged into the system?
            if (Request.IsAuthenticated)
            {
                //Logged in but do you have authority to be on this page?
                if (User.IsInRole(ConfigurationManager.AppSettings["customerRole"]))
                {
                    //Obtain the CustomerId on the security user record
                    SecurityController ssysmgr = new SecurityController();
                    //Pass the value of the username to the method GetCurrentCustomerId
                    //Receive the current customer database id
                    int? customerid = ssysmgr.GetCurrentUserCustomerId(User.Identity.Name);

                    //Need to convert the int? to an int value
                    //int custid = customerid == null ? default(int) : int.Parse(customerid.ToString());

                    //Short hand
                    int custid = customerid ?? default(int);

                    //Use the custid to do a standard lookup against your customer database entity
                    //TODO


                    LoggedUser.Text = custid.ToString();
                }
                else
                {
                    Response.Redirect("~/SamplePages/AccessDenied.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        #region Error Handling
        protected void SelectCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }
        protected void InsertCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Process Success", "Album has been added.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }

        protected void UpdateCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Process Success", "Album has been updated.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }
        protected void DeleteCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Process Success", "Album has been removed.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }
        #endregion

        protected void ArtistFetch_Click(object sender, EventArgs e)
        {
            TracksBy.Text = "Artist";

            if (string.IsNullOrEmpty(ArtistName.Text))
            {
                MessageUserControl.ShowInfo("Artist Search", "No artist name was supplied.");
            }
            //Hidden filed SearchArg is accessed by the property Value, not Text
            SearchArg.Value = ArtistName.Text;

            //To cause an ODS to re-execute to refresh the list view, you only need to do a .DataBind() against the control
            TracksSelectionList.DataBind();
        }


        protected void GenreFetch_Click(object sender, EventArgs e)
        {
            TracksBy.Text = "Genre";

            //There is no prompt test needed as the DDL doesn't have a prompt line
            SearchArg.Value = GenreDDL.SelectedValue.ToString(); //As an integer
            ///SearchArg.Value = GenreDDL.SelectedItem.Text; //As a string. Keep in mind that all strings have to be unique

            TracksSelectionList.DataBind();

        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {
            TracksBy.Text = "Album";

            if (string.IsNullOrEmpty(AlbumTitle.Text))
            {
                MessageUserControl.ShowInfo("Album Search", "No album name was supplied.");
            }
            SearchArg.Value = AlbumTitle.Text;

            TracksSelectionList.DataBind();
        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            //username is coming from the system via security
            //Since security has yet to be installed, a default will be setup for the username value
                //W10D3 done security
            string username = User.Identity.Name;

            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Playlist Search", "No playlist name was supplied.");
            }
            else
            {
                //Use some user friendly error handling by using MessageUserControl

                //Syntax of TryRun

                //MessageUserControl.TryRun(() => {
                //  coding block
                //},"optional message title","optional success message");

                MessageUserControl.TryRun(() =>
                {
                    //Code to execute under error handling control of MessageUserControl
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    RefreshPlaylist(sysmgr, username);
                },"Playlist Search","View the requested playlist below");
            }

        }

        private void RefreshPlaylist(PlaylistTracksController sysmgr, string username)
        {
            List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
            PlayList.DataSource = info;
            PlayList.DataBind();
        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Track Movement", "You must have a playlist name.");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Track Movement", "You must have a playlist showing.");
                }
                else
                {
                    //Was anything actually selected?
                    CheckBox songSelected = null;
                    int rowsSelected = 0;
                    MoveTrackItem moveTrack = new MoveTrackItem();

                    //Traverse the GridView
                    //You can use a foreach() instead
                    for (int i = 0; i < PlayList.Rows.Count; i++)
                    {
                        //Point to the checkbox control on the gridview row
                        songSelected = PlayList.Rows[i].FindControl("Selected") as CheckBox;
                        //Test the setting of the checkbox
                        if (songSelected.Checked)
                        {
                            rowsSelected++;
                            moveTrack.TrackId = int.Parse((PlayList.Rows[i].FindControl("TrackID") as Label).Text);
                            moveTrack.TrackNumber = int.Parse((PlayList.Rows[i].FindControl("TrackNumber") as Label).Text);
                        }
                    }
                    //Processing rule: Only one row may be moved
                    switch (rowsSelected)
                    {
                        case 0:
                            {
                                MessageUserControl.ShowInfo("Track Movement", "No song selected. You must select a single song to move.");
                                break;
                            }
                        case 1:
                            {
                                //Is it the bottom row?
                                if (moveTrack.TrackNumber == PlayList.Rows.Count)
                                {
                                    MessageUserControl.ShowInfo("Track Movement", "Song is the last song on the list. No movement necessary.");
                                }
                                else
                                {
                                    //Move the track
                                    moveTrack.Direction = "down";
                                    MoveTrack(moveTrack);
                                }
                                break;
                            }
                        default:
                            {
                                MessageUserControl.ShowInfo("Track Movement", "You must select only a single song to move.");
                                break;
                            }
                    }
                }
            }
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Track Movement", "You must have a playlist name.");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Track Movement", "You must have a playlist showing.");
                }
                else
                {
                    //Was anything actually selected?
                    CheckBox songSelected = null;
                    int rowsSelected = 0;
                    MoveTrackItem moveTrack = new MoveTrackItem();

                    //Traverse the GridView
                    //You can use a foreach() instead
                    for (int i = 0; i < PlayList.Rows.Count; i++)
                    {
                        //Point to the checkbox control on the gridview row
                        songSelected = PlayList.Rows[i].FindControl("Selected") as CheckBox;
                        //Test the setting of the checkbox
                        if (songSelected.Checked)
                        {
                            rowsSelected++;
                            moveTrack.TrackId = int.Parse((PlayList.Rows[i].FindControl("TrackID") as Label).Text);
                            moveTrack.TrackNumber = int.Parse((PlayList.Rows[i].FindControl("TrackNumber") as Label).Text);
                        }
                    }
                    //Processing rule: Only one row may be moved
                    switch (rowsSelected)
                    {
                        case 0:
                            {
                                MessageUserControl.ShowInfo("Track Movement", "No song selected. You must select a single song to move.");
                                break;
                            }
                        case 1:
                            {
                                //Is it the top row?
                                if (moveTrack.TrackNumber == 1)
                                {
                                    MessageUserControl.ShowInfo("Track Movement", "Song is the first song on the list. No movement necessary.");
                                }
                                else
                                {
                                    //Move the track
                                    moveTrack.Direction = "up";
                                    MoveTrack(moveTrack);
                                }
                                break;
                            }
                        default:
                            {
                                MessageUserControl.ShowInfo("Track Movement", "You must select only a single song to move.");
                                break;
                            }
                    }
                }
            }
        }

        protected void MoveTrack(MoveTrackItem moveTrack)
        {
            string username = "HansenB";
            moveTrack.PlaylistName = PlaylistName.Text;
            moveTrack.UserName = username;
            MessageUserControl.TryRun(() =>
            {
                PlaylistTracksController sysmgr = new PlaylistTracksController();
                sysmgr.MoveTrack(moveTrack);
                RefreshPlaylist(sysmgr, username);
            }, "Track Movement", "Selected track has been moved from the playlist");
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            string username = "HansenB";
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Track Removal", "You must have a playlist name.");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Track Removal", 
                        "You must have a playlist visible to do removals. Select from the displayed playlist.");
                }
                else
                {
                    //Collect the tracks indicated on the playlist for removal
                    List<int> trackids = new List<int>();
                    int rowsSelected = 0; //row conter
                    CheckBox trackSelection = null;

                    //Traverse the GridView
                    //You can use a foreach() instead
                    for(int i = 0; i < PlayList.Rows.Count; i++)
                    {
                        //Point to the checkbox control on the gridview row
                        trackSelection = PlayList.Rows[i].FindControl("Selected") as CheckBox;
                        //Test the setting of the checkbox
                        if (trackSelection.Checked)
                        {
                            rowsSelected++;
                            trackids.Add(int.Parse((PlayList.Rows[i].FindControl("TrackId") as Label).Text));
                        }
                    }

                    //Was a song selected?
                    if (rowsSelected == 0)
                    {
                        MessageUserControl.ShowInfo("Track Removal", "You must select at least one song to remove.");
                    }
                    else
                    {
                        MessageUserControl.TryRun(() => 
                        {
                            PlaylistTracksController sysmgr = new PlaylistTracksController();
                            sysmgr.DeleteTracks(username, PlaylistName.Text, trackids);
                            RefreshPlaylist(sysmgr, username);
                        },"Track removal","Selected track(s) have been removed from the playlist");
                    }
                }
            }
 
        }

        protected void TracksSelectionList_ItemCommand(object sender, 
            ListViewCommandEventArgs e)
        {
            string username = "HansenB";

            //Validate playlist exists
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter a playlist name.");
            }
            else
            {
                //Grab a value from the selected ListView row
                //The row is referred to as e.Item
                //To access the column use the .FindControl("xx") as ctrlType.ctrlAccess
                string song = (e.Item.FindControl("NameLabel") as Label).Text;
                //Reminder: MessageUserControl will do the error handling
                MessageUserControl.TryRun(() => {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    sysmgr.Add_TrackToPLaylist(PlaylistName.Text, username, int.Parse(e.CommandArgument.ToString()), song);

                    RefreshPlaylist(sysmgr, username);
                },"Add Track to Playlist","Track has been added to the playlist.");
            }
        }

    }
}