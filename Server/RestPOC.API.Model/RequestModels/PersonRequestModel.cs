using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestPOC.API.Model.RequestModels {
    
    public class PersonRequestModel {

        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public int BirthYear { get; set; }
    }
}
