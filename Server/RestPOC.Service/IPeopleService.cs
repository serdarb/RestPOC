namespace RestPOC.Service
{
    using System.Collections.Generic;

    using RestPOC.Domain;

    public interface IPeopleService {
        OperationResult AddNewUser(Person person);
        OperationResult UpdateUser(Person person);

        OperationResult<Person> GetById(int personId);

        OperationResult<PaginatedList<Person>> GetAll(int pageIndex, int pageSize);
    }
}
