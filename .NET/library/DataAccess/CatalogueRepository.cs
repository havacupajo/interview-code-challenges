﻿using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class CatalogueRepository : ICatalogueRepository
    {
        public CatalogueRepository()
        {
        }
        public List<BookStock> GetCatalogue()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Catalogue
                    .Include(x => x.Book)
                    .ThenInclude(x => x.Author)
                    .Include(x => x.OnLoanTo)
                    .ToList();
                return list;
            }
        }

        public List<BookStock> SearchCatalogue(CatalogueSearch search)
        {
            using (var context = new LibraryContext())
            {
                var list = context.Catalogue
                    .Include(x => x.Book)
                    .ThenInclude(x => x.Author)
                    .Include(x => x.OnLoanTo)
                    .AsQueryable();

                if (search != null)
                {
                    if (!string.IsNullOrEmpty(search.Author)) {
                        list = list.Where(x => x.Book.Author.Name.Contains(search.Author));
                    }
                    if (!string.IsNullOrEmpty(search.BookName)) {
                        list = list.Where(x => x.Book.Name.Contains(search.BookName));
                    }
                }
                    
                return list.ToList();
            }
        }

        public List<BorrowerWithLoans> GetBorrowersWithActiveLoans()
        {
            using (var context = new LibraryContext())
            {
                var borrowersWithLoans = context.Catalogue
                    .Where(x => x.OnLoanTo != null)
                    .GroupBy(x => x.OnLoanTo)
                    .Select(g => new BorrowerWithLoans
                    {
                        Borrower = g.Key,
                        LoanedBooks = g.Select(x => x.Book.Name).ToList()
                    })
                    .ToList();

                return borrowersWithLoans;
            }
        }
    }
}
