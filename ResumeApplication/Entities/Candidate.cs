namespace ResumeApplication.Entities
{
    /// <summary>
    /// The candidate entity.
    /// </summary>
    public class Candidate
    {
        public int Id { get; set; }

        public int DegreeId { get; set; }

        public string  LastName { get; set; }

        public string  FirstName { get; set; }

        public string  Email { get; set; }

        public string  Mobile { get; set; }

        public DateTime CreationTime { get; set; }

        //Navigation properties
        public virtual CandidateFile CandidateFile { get; set; }

        public virtual Degree Degree { get; set; }
    }
}
