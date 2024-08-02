using System;

namespace AwesomeShop.Services.Orders.Application.Dtos.IntegrationDtos
{
    public class GetCustomerByIdDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public AddrresDto Adrres { get; set; }
    }

    public class AddrresDto
    {
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}
