using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


#region Additional Nameplaces
using FSISSystem.ModelViews;
using FSISSystem.BLL;
#endregion

namespace PracticeForExam.PracticePages
{
    public partial class PlayerParentOLTP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Register_Click(object sender, EventArgs e)
        {
            GardianItem GardianToAdd = new GardianItem();
            GardianToAdd.FirstName = GardianFirstName.Text;
            GardianToAdd.LastName = GardianLastName.Text;
            GardianToAdd.EmergencyPhoneNumber = EmargencyPhoneNumber.Text;
            GardianToAdd.EmailAddress = EmailAddress.Text;

            TeamController sysmng = new TeamController();
            int teamId = sysmng.Teams_FindByName(TeamName.Text);

            PlayerItem PlayerToAdd = new PlayerItem();
            PlayerToAdd.TeamID = teamId;
            PlayerToAdd.FirstName = FirstName.Text;
            PlayerToAdd.LastName = LastName.Text;
            PlayerToAdd.Age = int.Parse(Age.Text);
            PlayerToAdd.Gender = Gender.SelectedValue;
            PlayerToAdd.AlbertaHealthCareNumber = AHCN.Text;
            PlayerToAdd.MedicalAlertDetails = string.IsNullOrEmpty(MedicalAleart.Text) ? null : MedicalAleart.Text;

            

            MessageUserControl.TryRun(() =>
            {
                PlayerGardianController smng = new PlayerGardianController();
                smng.Registar_Player(PlayerToAdd, GardianToAdd);
            }, "Add Player", "Player has been added.");
        }

        protected void Clear_Click(object sender, EventArgs e)
        {

        }
    }
}