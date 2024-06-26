using CLIENT.Models.ViewModels;
using CLIENT.Services.Contracts;
using System.Text;
using System.Text.Json;

namespace CLIENT.Services
{
    public class ServiceMovimientosTarjeta : IServiceMovimientosTarjeta
    {
        private readonly HttpClient _httpClient;

        public ServiceMovimientosTarjeta(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GuardarMovimiento(MovimientosTarjetaVM movimiento)
        {
            var jsonData = JsonSerializer.Serialize(movimiento);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5100/api/Movimiento", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return jsonString;
            }
            else
            {
                throw new Exception($"Error al obtener los movimientos de la tarjeta. Código de estado: {response.StatusCode}");
            }
        }

        public async Task<List<MovimientosTarjetaVM>> ObtenerMovimientosTarjeta(int idTarjeta, string numeroTarjeta, int tipo, int mes, int anio)
        {
            var requestData = new
            {
                idTarjeta,
                numeroTarjeta,
                tipo,
                mes,
                anio
            };

            var jsonData = JsonSerializer.Serialize(requestData);


            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");


            HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5100/api/Movimiento/movimientos", content);


            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var tarjetaCreditoVM = JsonSerializer.Deserialize<List<MovimientosTarjetaVM>>(jsonString);
                return tarjetaCreditoVM;
            }
            else
            {
                throw new Exception($"Error al obtener los movimientos de la tarjeta. Código de estado: {response.StatusCode}");
            }
        }
    }
}
