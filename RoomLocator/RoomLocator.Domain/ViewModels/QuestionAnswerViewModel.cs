namespace RoomLocator.Domain.ViewModels
{
    public class QuestionAnswerViewModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int Score { get; set; }
        public string Text { get; set; }
    }
}
