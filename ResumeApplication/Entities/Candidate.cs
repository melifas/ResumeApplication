namespace ResumeApplication.Entities
{
    /// <summary>
    /// The candidate entity.
    /// </summary>
    public class Candidate
    {
        public int Id { get; set; }

        public string  LastName { get; set; }

        public string  FirstName { get; set; }

        public string  Email { get; set; }

        public string  Mobile { get; set; }

        public string  Cv { get; set; }

        public DateTime CreationTime { get; set; }


        public virtual ICollection<CandidateFile> CandidateFiles { get; set; } = new HashSet<CandidateFile>();

        public virtual ICollection<Degree> Degrees { get; set; } = new HashSet<Degree>();
    }
}
