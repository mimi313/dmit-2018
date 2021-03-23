using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using FSISSystem.DAL;
using FSISSystem.Entities;
using FSISSystem.ModelViews;
using FreeCode.Exceptions;
#endregion

namespace FSISSystem.BLL
{
    public class PlayerGardianController
    {
        List<Exception> brokenRules = new List<Exception>();
        public void Registar_Player(PlayerItem player, GardianItem gardian)
        {
            Guardian gardianExists = null;
            Player playerExists = null;

            using (var context = new FSISSystemContext())
            {
                gardianExists = (from x in context.Guardians
                                 where x.FirstName.Equals(gardian.FirstName) && x.LastName.Equals(gardian.LastName)
                                    && x.EmergencyPhoneNumber.Equals(gardian.EmergencyPhoneNumber)
                                 select x).FirstOrDefault();

                if (gardianExists == null)
                {
                    gardianExists = new Guardian()
                                    {
                                        FirstName = gardian.FirstName,
                                        LastName = gardian.LastName,
                                        EmergencyPhoneNumber = gardian.EmergencyPhoneNumber,
                                        EmailAddress = gardian.EmailAddress
                                    };
                    context.Guardians.Add(gardianExists);
                }
                else
                {
                    playerExists = (from x in context.Players
                                    where x.FirstName.Equals(player.FirstName) && x.LastName.Equals(player.LastName)
                                            && x.AlbertaHealthCareNumber.Equals(player.AlbertaHealthCareNumber)
                                    select x).FirstOrDefault();

                    if (playerExists == null)
                    {
                        playerExists = new Player()
                        {
                            TeamID = player.TeamID,
                            FirstName = player.FirstName,
                            LastName = player.LastName,
                            Age = player.Age,
                            Gender = player.Gender,
                            AlbertaHealthCareNumber = player.AlbertaHealthCareNumber,
                            MedicalAlertDetails = player.MedicalAlertDetails,
                            GamesPlayed = 0
                        };
                    }
                    else
                    {
                        brokenRules.Add(new BusinessRuleException<string>("Player already exists.", nameof(player),
                                            player.LastName + ", " + player.FirstName));
                    }

                    gardianExists.Players.Add(playerExists);

                    if (brokenRules.Count() > 0)
                    {
                        throw new BusinessRuleCollectionException("Add Player Concerens: ", brokenRules);
                    }
                    else
                    {
                        context.SaveChanges();
                    }

                }
            }
        }
    }
}
