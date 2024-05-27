using Application.Common.Interfaces;
using Application.Common.Options;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Options;
using PolcarDistributorsSalesClient;

namespace Infrastructure.Services.Polcar;

public class DistributorsSalesService(IMapper mapper, IOptions<PolcarOptions> polcarOptions) : IDistributorsSalesService
{
    private readonly DistributorsSalesServiceSoapClient _client = new DistributorsSalesServiceSoapClient(DistributorsSalesServiceSoapClient.EndpointConfiguration.DistributorsSalesServiceSoap12);

    public async Task<IEnumerable<Order>> GetOrdersAsync(DateTime date)
    {
        var response = await _client
            .GetListOfOrdersV3Async(
                distributorCode: polcarOptions.Value.DistributorCode,
                getOpenOrdersOnly: false,
                branchId: polcarOptions.Value.BranchId,
                dateFrom: date.Date,
                dateTo: date.AddDays(1).Date,
                getOrdersHeadersOnly: false,
                login: polcarOptions.Value.Login,
                password: polcarOptions.Value.Password,
                languageId: polcarOptions.Value.LanguageId
            );

        var responseBody = response.Body.GetListOfOrdersV3Result;

        if (responseBody.ErrorCode != "0")
            throw new Exception($"{responseBody.ErrorCode} - {responseBody.ErrorInformation}");

        if (responseBody.ListOfOrders?.Count > 0)
            return mapper.Map<IEnumerable<Order>>(responseBody.ListOfOrders);

        return new List<Order>();
    }
}