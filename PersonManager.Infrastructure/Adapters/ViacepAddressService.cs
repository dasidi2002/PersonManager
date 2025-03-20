using PersonManager.Domain.Entities;
using PersonManager.Domain.Ports;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PersonManager.Infrastructure.Adapters
{
    public class ViacepAddressService : IAddressService
    {
        private readonly HttpClient _httpClient;

        public ViacepAddressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://viacep.com.br/ws/");
        }

        public async Task<Address> GetAddressByZipCodeAsync(string zipCode)
        {
            var response = await _httpClient.GetAsync($"{zipCode}/json/");
            response.EnsureSuccessStatusCode();

            var viacepResponse = await response.Content.ReadFromJsonAsync<ViacepResponse>();

            if (viacepResponse.Erro)
            {
                throw new HttpRequestException($"CEP {zipCode} não encontrado");
            }

            return new Address(
                viacepResponse.Logradouro,
                string.Empty, 
                viacepResponse.Complemento,
                viacepResponse.Bairro,
                viacepResponse.Localidade,
                viacepResponse.Uf,
                viacepResponse.Cep
            );
        }
    }

    public class ViacepResponse
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Ibge { get; set; }
        public string Gia { get; set; }
        public string Ddd { get; set; }
        public string Siafi { get; set; }
        public string Estado { get; set; }
        public string Regiao { get; set; }
        public string Unidade { get; set; }

        [JsonPropertyName("erro")]
        public bool Erro { get; set; }
    }
}