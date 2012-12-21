using RestPOC.API.Model.Validation;

namespace RestPOC.API.Model.RequestCommands {

    public class PaginatedRequestCommand : IRequestCommand {

        [Minimum(1)]
        public int Page { get; set; }

        [Minimum(1)]
        [Maximum(50)]
        public int Take { get; set; }
    }
}