namespace RestPOC.Service
{
    using RestPOC.Domain;

    public interface IPeopleService {

        // User Role
        OperationResult<Person> AddNewUser(Person person);
        // User Role
        OperationResult<Person> UpdateUser(Person person);
        // User Role
        OperationResult<Person> GetById(int personId);

        // SuperUser Role
        OperationResult<PaginatedList<Person>> GetAll(int pageIndex, int pageSize);
    }
}
