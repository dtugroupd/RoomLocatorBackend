using System.ComponentModel.DataAnnotations.Schema;

namespace RoomLocator.Domain.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string StudentId { get; set; }
        [NotMapped]
        public string FirstName { get; set; }
        [NotMapped]
        public string LastName { get; set; }

//        public string FullName
//        {
//            get { return FirstName + " " + LastName; }
//        }
//        public string FullName
//        {
//            get { return $"{FirstName} {LastName}"; }
//        }
        public string FullName => $"{FirstName} {LastName}";
    }
}