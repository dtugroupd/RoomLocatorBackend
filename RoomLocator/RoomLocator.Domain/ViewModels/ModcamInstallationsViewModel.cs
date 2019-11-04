namespace RoomLocator.Domain.ViewModels
{
    public class ModcamInstallationsViewModel
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string NameSpace { get; set; } // maybe not necessary to have it
        public string[] Components { get; set; }
      
    }
}