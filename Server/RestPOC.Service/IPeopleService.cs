namespace RestPOC.Service
{
    using RestPOC.Domain;

    public interface IPeopleService {

        // EveryOne
        OperationResult<Person> GetById(int personId);

        // User Role
        OperationResult<Person> AddNewUser(Person person);
        // User Role
        OperationResult<Person> UpdateUser(Person person);

        // SuperUser Role
        OperationResult<PaginatedList<Person>> GetAll(int pageIndex, int pageSize);
    }
}
