using CHLeMudra.Data;
using CHLeMudra.Proxy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CHLeMudra.Helper
{

    public class CommonApi
    {

        public static bool IsTokenValid(string token)
        {
            bool isValid = false;
            if (!string.IsNullOrEmpty(token))
            {
                string key = CommonHelper.Decrypt(token);
                string[] parts = key.Split(new char[] { '|' });
                if (parts.Length > 1)
                {
                    int UserId = Convert.ToInt32(parts[0]);
                    //DateTime expireon = Convert.ToDateTime(parts[1]);
                    string name = Convert.ToString(parts[1]);
                    IUserRepository _user = new UserRepository();
                    var TokenDetails = _user.GetTokenDetails(UserId);
                    if (TokenDetails != null)
                    {
                        string Token = CommonHelper.Decrypt(TokenDetails.Token);
                        isValid = key.Equals(Token) ? true : false;                        
                    }
                 
                }
            }

            return isValid;

        }
        public static dynamic GetApiURLData(string serviceApiURL, string accessToken, bool responseEntity = false,bool isBasic= false)
        {
            ResponseEntity objResponseEntity = new ResponseEntity();
            string ServiceApiURL = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-Chl-ImplId", "2");
                if (isBasic) // token is required
                {
                    // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", accessToken);
                }
                HttpResponseMessage response = client.GetAsync(serviceApiURL).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsAsync<object>();
                    var result = data.Result;
                    objResponseEntity.Status = 1;
                    objResponseEntity.Message = "Data Found";
                    objResponseEntity.Data = result;
                    
                }
                else
                {
                    var data = response.Content.ReadAsAsync<object>();
                    var result = data.Result;
                    objResponseEntity.Status = 0;
                    objResponseEntity.Message = "Data Not Found";
                    objResponseEntity.Data = result;
                  //  return objResponseEntity;
                    //string error = Convert.ToString(result.error);
                    //objApiResponse.responsestatus = result.responsestatus;
                    //objApiResponse.result = error;

                }
            }
            return objResponseEntity;

        }


        public static dynamic GetPGWApiURLData(string serviceApiURL, object jsonInput, string accessToken, bool responseEntity = false, string sourceType = null)
        {  ////string _userName = ConfigurationManager.AppSettings["UserName"];
            ////string _pwd = ConfigurationManager.AppSettings["Password"];
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //var byteArray = Encoding.ASCII.GetBytes("" + _userName + ":" + _pwd + "");
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                //Trust all certificates
                //System.Net.ServicePointManager.ServerCertificateValidationCallback =
                //    ((sender, certificate, chain, sslPolicyErrors) => true);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpResponseMessage response = client.GetAsync(serviceApiURL).Result;
                ResponseEntity objResponseEntity = new ResponseEntity();
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsAsync<object>();
                    var result = data.Result;
                    objResponseEntity.Status = 1;
                    objResponseEntity.Message = "Data Found";
                    objResponseEntity.Data = result;
                    return objResponseEntity;
                }
                else
                {
                    var data = response.Content.ReadAsAsync<object>();
                    var result = data.Result;
                    objResponseEntity.Status = 0;
                    objResponseEntity.Message = "Data Not Found";
                    objResponseEntity.Data = result;
                    return objResponseEntity;
                    //string error = Convert.ToString(result.error);
                    //objApiResponse.responsestatus = result.responsestatus;
                    //objApiResponse.result = error;

                }
            }

        }


        public static dynamic POSTApiUrlEncodedData(string serviceApiURL, Dictionary<string,string> jsonInput, string accessToken, bool responseEntity = false, string sourceType = null)
        {

            using (var client = new HttpClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var content = new FormUrlEncodedContent(jsonInput);                
                var response = client.PostAsync(serviceApiURL, content).Result;           
                ResponseEntity objResponseEntity = new ResponseEntity();
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsAsync<object>();                   
                    objResponseEntity.Status = 1;
                    objResponseEntity.Message = "Data Found";
                    objResponseEntity.Data = data.Result;
                    return objResponseEntity;
                }
                else
                {
                    var data = response.Content.ReadAsAsync<object>();                    
                    objResponseEntity.Status = 0;
                    objResponseEntity.Message = "Data Not Found";
                    objResponseEntity.Data = data.Result;
                    return objResponseEntity;
                   
                }
            }

        }

        public static dynamic PostApiURLData(string serviceApiURL, object jsonInput, string accessToken, bool responseEntity = false,bool isBasic=false)
        {

            ApiResponse objApiResponse = new ApiResponse();

            string ServiceApiURL = string.Empty;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-Chl-ImplId", "2");
                if (isBasic) // if token is required
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", accessToken);
                }
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpResponseMessage response = client.PostAsJsonAsync(serviceApiURL, jsonInput).Result;
                ResponseEntity objResponseEntity = new ResponseEntity();
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsAsync<object>();
                    objResponseEntity.Status = 1;
                    objResponseEntity.Message = "Data Found";
                    objResponseEntity.Data = data.Result;
                    return objResponseEntity;
                }
                else
                {
                    var data = response.Content.ReadAsAsync<object>();
                    objResponseEntity.Status = 0;
                    objResponseEntity.Message = "Data Not Found";
                    objResponseEntity.Data = data.Result;
                    return objResponseEntity;

                }
                //if (response.IsSuccessStatusCode)
                //{
                //    var data = response.Content.ReadAsAsync<ApiResponse>();
                //    var result = data.Result;
                //    if (result.responsestatus == 1)
                //    {
                //        string res = Convert.ToString(result.result);
                //        objApiResponse.responsestatus = result.responsestatus;
                //        if (responseEntity)
                //        {
                //            ResponseEntity responseEntityData = JsonConvert.DeserializeObject<ResponseEntity>(res);
                //            objApiResponse.objResponseEntity = responseEntityData;
                //        }
                //        else
                //        {
                //            objApiResponse.result = res;
                //        }
                //    }
                //}
                //else
                //{
                //    var data = response.Content.ReadAsAsync<ApiResponse>();
                //    var result = data.Result;

                //    string error = Convert.ToString(result.error);
                //    objApiResponse.responsestatus = result.responsestatus;
                //    objApiResponse.result = error;

                //}
            }
            
        }

        public static dynamic PutApiURLData(string serviceApiURL, object jsonInput, string accessToken, bool responseEntity = false, string sourceType = null)
        {

            ApiResponse objApiResponse = new ApiResponse();

            string ServiceApiURL = string.Empty;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (Convert.ToString(ConfigurationManager.AppSettings["IsTokenRequired"]) == "1") // if token is required
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }
                if (!string.IsNullOrEmpty(sourceType))
                {
                    client.DefaultRequestHeaders.Add("X-SEW-SourceType", sourceType);
                }
                HttpResponseMessage response = client.PutAsJsonAsync(serviceApiURL, jsonInput).Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsAsync<ApiResponse>();
                    var result = data.Result;
                    if (result.responsestatus == 1)
                    {
                        string res = Convert.ToString(result.result);
                        objApiResponse.responsestatus = result.responsestatus;
                        if (responseEntity)
                        {
                            ResponseEntity responseEntityData = JsonConvert.DeserializeObject<ResponseEntity>(res);
                            objApiResponse.objResponseEntity = responseEntityData;
                        }
                        else
                        {
                            objApiResponse.result = res;
                        }
                    }
                }
                else
                {
                    var data = response.Content.ReadAsAsync<ApiResponse>();
                    var result = data.Result;

                    string error = Convert.ToString(result.error);
                    objApiResponse.responsestatus = result.responsestatus;
                    objApiResponse.result = error;

                }
            }
            return objApiResponse;
        }


    
      
        public static dynamic GetPaymentApiURLData(string serviceApiURL, string accessToken, bool responseEntity = false, string sourceType = null)
        {
          
            ResponseEntity objApiResponse = new ResponseEntity();

            string ServiceApiURL = string.Empty;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (Convert.ToString(ConfigurationManager.AppSettings["IsTokenRequired"]) == "1") // token is required
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }

                if (!string.IsNullOrEmpty(sourceType))
                {
                    client.DefaultRequestHeaders.Add("X-SEW-SourceType", sourceType);
                }
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;
                var response = client.GetAsync(serviceApiURL).Result;

                if (response.IsSuccessStatusCode)
                {
                    //var data = response.Content.ReadAsAsync<ApiResponse>();
                    var data = response.Content.ReadAsAsync<ResponseEntity>();
                    var result = data.Result;
                    if (result.Data != null)
                    {
                        //var res = Convert.ToString(result.Data);
                        objApiResponse.Status = result.Status;
                        objApiResponse.Data = Convert.ToString(result.Data); //JsonConvert.DeserializeObject<ApiResponse>(res);
                        objApiResponse.Message = result.Message;
                    }
                    else
                    {
                        objApiResponse.Status = result.Status;
                        objApiResponse.Data = Convert.ToString(result.Data);
                        objApiResponse.Message = result.Message;
                    }
                }
                else
                {
                    var data = response.Content.ReadAsAsync<ResponseEntity>();
                    var result = data.Result;
                    if (result.Status == -1)
                    {
                        objApiResponse.Status = result.Status;
                        objApiResponse.Data = Convert.ToString(result.Data);
                        objApiResponse.Message = result.Message;
                    }
                }
            }
            return objApiResponse;
        }
    }

    public class ApiTokenResponse
    {
        /// <summary>
        /// api token
        /// </summary>
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string error { get; set; }
        public string error_description { get; set; }
        public int responsestatus { get; set; }
        public string Message { get; set; }
    }
    public class ApiResponse
    {
        /// <summary>
        /// API Version
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// Response Status Code
        /// </summary>
        public int statuscode { get; set; }

        /// <summary>
        /// Response Status Code
        /// </summary>
        public int responsestatus { get; set; }

        /// <summary>
        /// Api Request Time
        /// </summary>
        public string RequestTimeStamp { get; set; }

        /// <summary>
        /// Api Response Time
        /// </summary>
        public string ResponseTimeStamp { get; set; }

        /// <summary>
        /// Records Per Request
        /// </summary>
        public int RecordPerRequest { get; set; }

        /// <summary>
        /// Total Record Found
        /// </summary>
        public int ResultCount { get; set; }

        /// <summary>
        /// Error Message
        /// </summary>
        public object error { get; set; }

        /// <summary>
        /// Messagess
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Result (Response Data)
        /// </summary>
        public object result { get; set; }


        public ResponseEntity objResponseEntity { get; set; }
    }
}
