using Application.Common.Interfaces;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using PolcarDistributorsSalesClient;

namespace Infrastructure.Services.Polcar;

public class DistributorsSalesService(IMapper mapper, IUserSetting userSetting) : IDistributorsSalesService
{
    private readonly UserSetting _userSetting = userSetting.GetCurrentUserSetting();
    private readonly DistributorsSalesServiceSoapClient _client = new DistributorsSalesServiceSoapClient(DistributorsSalesServiceSoapClient.EndpointConfiguration.DistributorsSalesServiceSoap12);

    public async Task<IEnumerable<Order>> GetOrdersAsync(DateTime date)
    {
        if (_userSetting == null)
            throw new Exception("User doesn't have a settings. Please contact with admin");

        var response = await _client
            .GetListOfOrdersV3Async(
            distributorCode: _userSetting.PolcarSetting.DistributorCode,
            getOpenOrdersOnly: false,
            branchId: _userSetting.PolcarSetting.BranchId,
            dateFrom: date.Date,
            dateTo: date.AddDays(1).Date,
            getOrdersHeadersOnly: false,
            login: _userSetting.PolcarSetting.Login,
            password: _userSetting.PolcarSetting.Password,
            languageId: _userSetting.PolcarSetting.LanguageId
            );

        var responseBody = response.Body.GetListOfOrdersV3Result;

        if (responseBody.ErrorCode != "0")
            throw new Exception($"{responseBody.ErrorCode} - {responseBody.ErrorInformation}");

        if (responseBody.ListOfOrders?.Count > 0)
            return mapper.Map<IEnumerable<Order>>(responseBody.ListOfOrders);

        return new List<Order>();
    }
}