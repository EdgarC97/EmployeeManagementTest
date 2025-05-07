namespace EmployeeManagement.Web.Models
{
    public class CreateEmployeeViewModel : EmployeeViewModelBase
    {
        public CreateEmployeeViewModel()
        {
            DateOfBirth = DateTime.Now.AddYears(-20);
        }
    }
}
