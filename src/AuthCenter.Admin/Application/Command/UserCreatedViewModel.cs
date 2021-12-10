namespace AuthCenter.Admin.Application.Command
{
    public class UserCreatedViewModel
    {
        public Guid? Id { get; set; }
        public List<string>? Errs { get; set; }
    }
}