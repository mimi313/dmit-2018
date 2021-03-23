using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using FSISSystem.DAL;
#endregion

namespace FSISSystem.BLL
{
    public class TeamController
    {
		public int Teams_FindByName(string teamname)
		{
			using (var context = new FSISSystemContext())
			{
				var result = (from x in context.Teams
							  where x.TeamName == teamname
							  select x.TeamID).FirstOrDefault();
				return result;
			}
		}
	}
}
