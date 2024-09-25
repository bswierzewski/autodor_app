using Application.Invoices.Commands.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPrintService
    {
        public byte[] Print(Contractor contractor, Order order, List<Pozycje> items);
    }
}
