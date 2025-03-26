
using CHLeMudra.API;
using CHLeMudra.API.Helper;
using CHLeMudra.API.Proxy;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
public interface IApiHelper
{
    string GenerateAccessToken();
    ResponseEntity PostApiURLData(string serviceApiURL, object jsonInput);
    dynamic GetApiURLData(string serviceApiURL);
}
public class ApiHelper : IApiHelper
{
    private readonly IOptions<ServiceSettings> _serviceSettings;
    public ApiHelper(IOptions<ServiceSettings> serviceSettings)
    {
        _serviceSettings = serviceSettings;
    }

    public ResponseEntity PostApiURLData(string serviceApiURL, object jsonInput)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("X-Chl-ImplId", "2");
            // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", _serviceSettings.Value.AccessToken);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", GenerateAccessToken());

            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var json = JsonConvert.SerializeObject(jsonInput);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(serviceApiURL, data).Result;
            ResponseEntity objResponseEntity = new ResponseEntity();
            if (response.IsSuccessStatusCode)
            {
                objResponseEntity.Status = true;
                objResponseEntity.Message = "Data Found";
                objResponseEntity.Data = response.Content.ReadAsStringAsync().Result;
                return objResponseEntity;
            }
            else
            {
                objResponseEntity.Status = false;
                objResponseEntity.Message = "Data Not Found";
                objResponseEntity.Data = response.Content.ReadAsStringAsync().Result;
                return objResponseEntity;

            }

        }
    }


    public dynamic GetApiURLData(string serviceApiURL)
    {

        ResponseEntity apiResponse = new ResponseEntity();
        string ServiceApiURL = string.Empty;
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(appSettings.Value.FGApiCredentials);
            //string val = System.Convert.ToBase64String(plainTextBytes);
            //client.DefaultRequestHeaders.Add("Authorization", "Basic " + val);

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", _serviceSettings.Value.AccessToken);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", GenerateAccessToken());
            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;
            //string serviceApiURL = _serviceSettings.Value.EMudraSignApiUrl + apiMethod;
            var response = client.GetAsync(serviceApiURL).Result;
            if (response.IsSuccessStatusCode)
            {
                apiResponse.Status = true;
                apiResponse.Data = response.Content.ReadAsStringAsync().Result;
                apiResponse.Message = "Data Found";
            }
            else
            {
                apiResponse.Status = true;
                apiResponse.Data = response.Content.ReadAsStringAsync().Result;
                apiResponse.Message = "No Data Found";


            }
        }
        return apiResponse;
    }

    public string GenerateAccessToken()
    {
        string serviceApiURL = _serviceSettings.Value.EMudraSignApiUrl + ApiMethod.ValidateLogin;
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("AppName", "TranslatorApp");
            client.DefaultRequestHeaders.Add("SecretKey", _serviceSettings.Value.SecretKey);
            //client.DefaultRequestHeaders.Add("X-Chl-ImplId", "2");

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", _serviceSettings.Value.AuthToken);

            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var jsonInput = new
            {
                UserName = _serviceSettings.Value.UserName,
                Password = _serviceSettings.Value.Password,

            };
            var json = JsonConvert.SerializeObject(jsonInput);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(serviceApiURL, data).Result;
            ResponseEntity objResponseEntity = new ResponseEntity();
            if (response.IsSuccessStatusCode)
            {
                ValidateLoginProxy objData = JsonConvert.DeserializeObject<ValidateLoginProxy>(response.Content.ReadAsStringAsync().Result.ToString());
                if (objData != null && objData.IsSuccess && objData.Response != null)                
                    return objData.Response.AuthToken;                
                else
                    return "";
            }
            else
                return "";
        }
    }
}




