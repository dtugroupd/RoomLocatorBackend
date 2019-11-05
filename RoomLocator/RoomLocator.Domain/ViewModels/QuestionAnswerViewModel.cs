namespace RoomLocator.Domain.ViewModels
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class QuestionAnswerViewModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int Score { get; set; }
        public string Text { get; set; }
    }
}
