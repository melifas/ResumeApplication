namespace ResumeApplication.Entities
{
    /// <summary>
    /// The degree entity.
    /// </summary>
    public class Degree
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationTime { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }

    }
}
