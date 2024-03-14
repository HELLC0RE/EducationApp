using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp
{
    public class DealInfo
    {
        public int id {  get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string midlename {  get; set; }
        public DateTime birthday { get; set; }
        public string address { get; set; }
        public string passport_data { get; set; }
        public string phone_number { get; set; }
        public string educationPrograms { get; set; }
        public string total_cost { get; set; }
        public DealInfo ( ) { }
        public DealInfo (int id, string firstname, string lastname, string midlename, DateTime birthday, string address, string passport_data, string phone_number, string educationPrograms, string total_cost)
        {
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;   
            this.midlename = midlename;
            this.birthday = birthday;
            this.address = address;
            this.passport_data = passport_data;
            this.phone_number = phone_number;   
            this.educationPrograms = educationPrograms;
            this.total_cost = total_cost;
        }
    }
}
