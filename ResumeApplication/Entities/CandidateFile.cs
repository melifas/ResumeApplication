namespace ResumeApplication.Entities
{
    public class CandidateFile
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }

        public string Name { get; set; }

        public string FileType { get; set; }

        public byte[] DataFile { get; set; }


        //Navigation properties
        public virtual Candidate Candidate { get; set; }

    }
}
