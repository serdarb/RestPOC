namespace RestPOC.API.Model.RequestModels
{
    using System.ComponentModel.DataAnnotations;

    public class PersonRequestModel
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
        
        [Required, EmailAddress]
        public string Email { get; set; }
        
        public string Telephone { get; set; }
        public int BirthYear { get; set; }

        // For Demo Purposes ...
        [CreditCard]
        public string CreditCard { get; set; }
    }
}
