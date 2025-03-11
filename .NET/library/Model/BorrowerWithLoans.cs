namespace OneBeyondApi.Model
{
    public class BorrowerWithLoans
    {
        public Borrower Borrower { get; set; } 
        public List<string> LoanedBooks { get; set; }
    }
}
