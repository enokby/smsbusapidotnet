using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web;
using RestSharp;
using System.Net;
using System.Net.Http;

namespace SMSBusAPI
{
    public class SMS
    {
        private string SMSBusAPIURL = ConfigurationManager.AppSettings["SMSBusAPIURL"];
        private IRestClient APIClient { get; set; }
        public static SMS Instance { get; } = new SMS();

        public SMS() {

        }
        public string Envoi(string Numero, string Message) {
            try
            {
                APIClient = new RestClient(SMSBusAPIURL 
                    + "?id=saham2015sms&msg={{MESSAGE}}&dnr={{TO}}&srn=SAHAM"
                    .Replace("{{TO}}",Numero)
                    .Replace("{{MESSAGE}}",Message));
                var Request = new RestRequest("", Method.POST);
                IRestResponse Response = APIClient.Execute(Request);
                if (Response.IsSuccessful)
                {
                    return "ENVOI_OK";
                }
                else
                {
                    throw new ApplicationException("Echec de la requette SMSBus: " + Response.ErrorMessage);
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException("Echec de la requette SMSBus: " + e.Message);
            }
        }
    }
}
