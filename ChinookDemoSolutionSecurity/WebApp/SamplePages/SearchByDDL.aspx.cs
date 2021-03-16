using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Nameplaces
using ChinookSystem.BLL;
using ChinookSystem.ViewModels;
#endregion

namespace WebApp.SamplePages
{
    public partial class SearchByDDL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //This is the first time to load the page
                LoadArtistList();
            }
        }

        protected void LoadArtistList()
        {
            //User friendly error handling (aka try/catch)
            //Use the usercontrol MessageUserControl to manage the error handling for the web page 
            //  when control leaves the web page and goes to the class library
            MessageUserControl.TryRun(() => {
                //What goes inside the coding block? -> Your code that would normally be inside the try portion of a try/catch

                ArtistController sysmgr = new ArtistController();
                List<SelectionList> info = sysmgr.Artists_DDLList();

                //Let's assume the data collection needs to be sorted - this is redundant as it's already been sorted
                info.Sort((x, y) => x.DisplayField.CompareTo(y.DisplayField)); //DisplayField = Name

                //Setup the DDL
                ArtistList.DataSource = info;
                //ArtistList.DataTextField = "DisplayField";
                ArtistList.DataTextField = nameof(SelectionList.DisplayField);
                ArtistList.DataValueField = nameof(SelectionList.ValueField);
                ArtistList.DataBind();

                //Setup of a prompt line
                ArtistList.Items.Insert(0, new ListItem("select...", "0"));
                //Everything should be a string
                //"select..." goes into DisplayField
                //"0" goes into ValueField
            }, "Success title message", "the success title and body message are optional");
        }

        #region Error Handling Methods for ODS
        protected void SelectCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }
        #endregion

        protected void SearchAlbums_Click(object sender, EventArgs e)
        {
            if(ArtistList.SelectedIndex == 0) //Am I on the first physical line (prompt line) of the DDL?
            {
                MessageUserControl.ShowInfo("Search Selection Concern", "Select an artist for the search.");
                ArtistAlbumList.DataSource = null;
                ArtistAlbumList.DataBind();
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    AlbumController sysmgr = new AlbumController();
                    List<ChinookSystem.ViewModels.ArtistAlbums> info = sysmgr.Albums_GetAlbumsForArtist(int.Parse(ArtistList.SelectedValue));
                    //Testing if abort had happened
                    //throw new Exception("This is a test to see an abort from the web page code");
                    ArtistAlbumList.DataSource = info;
                    ArtistAlbumList.DataBind();
                },"Search Results", "The list of albums for the selected artist");
            }
        }
    }
}