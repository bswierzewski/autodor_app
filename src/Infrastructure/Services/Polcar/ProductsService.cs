using Application.Common.Interfaces;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Extensions;
using Infrastructure.Models;
using Microsoft.Extensions.Logging;
using PolcarProductsClient;

namespace Infrastructure.Services.Polcar;

public class ProductsService(ILogger<ProductsService> logger,IUserSetting userSetting) : IProductsService
{
    private readonly UserSetting _userSetting = userSetting.GetCurrentUserSetting();
    private readonly ProductsSoapClient _client = new(ProductsSoapClient.EndpointConfiguration.ProductsSoap12);

    public async Task<IDictionary<string, Domain.Entities.Product>> GetProductsAsync()
    {
        if (_userSetting == null)
            throw new Exception("User doesn't have a settings. Please contact with admin");

        try
        {
            var response = await _client.GetEAN13ListAsync(
                        _userSetting.PolcarSetting.Login,
                        _userSetting.PolcarSetting.Password,
                        1,
                        1);

            var products = response.Body.GetEAN13ListResult.Deserialize<ProductRoot>();

            return products.Items.Select(x => new Domain.Entities.Product
            {
                EAN13Code = x.EAN13Code,
                Name = x.PartName,
                Number = x.Number
            }).ToDictionary(x => x.Number);
        }
        catch (Exception ex)
        {
            logger.LogInformation(ex.Message);

            return new Dictionary<string, Domain.Entities.Product>();
        }
    }
}
