using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using ChinookSystem.ViewModels;

#endregion

namespace WebApp.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
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
            string username = "HansenB";

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
                    List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
                    PlayList.DataSource = info;
                    PlayList.DataBind();
                },"Playlist Search","View the requested playlist below");
            }

        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move track
 
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void TracksSelectionList_ItemCommand(object sender, 
            ListViewCommandEventArgs e)
        {
            //code to go here
            
        }

    }
}