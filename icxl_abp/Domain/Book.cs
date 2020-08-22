using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace icxl_abp.Domain
{

    public enum BookType
    {
        Undefined,
        Adventure,
        Biography,
        Dystopia,
        Fantastic,
        Horror,
        Science,
        ScienceFiction,
        Poetry
    }

    public class Book : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }

        public Book()
        {
        }
        public Book(Guid id, string name, BookType type, DateTime publishDate, float price)
        {
            this.Id = id;
            Name = name;
            Type = type;
            PublishDate = publishDate;
            Price = price;
        }
    }
}
