namespace UniversityTuitionPaymentSystemApi.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Term { get; set; }
        public float Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public Student Student { get; set; }
    }
}
