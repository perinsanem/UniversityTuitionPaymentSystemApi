namespace UniversityTuitionPaymentSystemApi.Models
{
    public class Tuition
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Term { get; set; }
        public float? Amount { get; set; }
        public Student Student { get; set; }
    }
}
