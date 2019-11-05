namespace RoomLocator.Domain.Models
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int SurveyAnswerId { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public virtual SurveyAnswer SurveyAnswer { get; set; }
        public virtual Question Question { get; set; }
    }
}
