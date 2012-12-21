using RestPOC.API.Model.Validation;

namespace RestPOC.API.Model.RequestCommands {

    public class PaginatedRequestCommand : IRequestCommand {

        [Minimum(1)]
        public int PageIndex { get; set; }

        [Minimum(1)]
        [Maximum(50)]
        public int PageSize { get; set; }
    }
}