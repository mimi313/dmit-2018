using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSISSystem.ModelViews
{
    public class PlayerItem
    {
        private string _MedicalAlertDetails;

        public int TeamID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public string AlbertaHealthCareNumber { get; set; }

        public string MedicalAlertDetails
        {
            get { return _MedicalAlertDetails; }
            set { _MedicalAlertDetails = string.IsNullOrEmpty(value) ? null : value; }
        }
    }
}
