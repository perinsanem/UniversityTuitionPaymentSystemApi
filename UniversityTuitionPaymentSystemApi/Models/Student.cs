namespace UniversityTuitionPaymentSystemApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string StudentNumber { get; set; }

        public ICollection<Payment> Payments { get; } = new List<Payment>();

        public ICollection<Tuition> Tuitions { get; } = new List<Tuition>();
    }
}
