using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SLB.WellMaps.Models;
using System.Net;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using ModernHttpClient;


namespace SLB.WellMaps.Services
{
    public class WellMapsAPI
    {       
        private HttpClient client;
        private string baseURL;

        public WellMapsAPI(Credential credential)
        {
            baseURL = credential.URL;
            
            client = new HttpClient(new HttpClientHandler { Credentials = new NetworkCredential(credential.UserName, credential.Password, "DIR") });
            //client = new HttpClient(new NativeMessageHandler() { DisableCaching = true });

            client.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");  
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public async Task<WellItem> GetWellByName (string wellName)
        {
            var uri = new Uri(baseURL + "/_vti_bin/listdata.svc/Well()?$filter=GRID_NAME eq '" + wellName + "'");           
            WellItem result = new WellItem();

            try
            {
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode) 
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var contentTemp = JsonConvert.DeserializeObject<WellResponseJSON>(content);
                    List<ResultWell> resultTemp = contentTemp.d.results;

                    if (resultTemp.Count == 1)
                    {
                        result.WellName = resultTemp[0].GRID_NAME;
                        result.Latitude = Convert.ToDouble(resultTemp[0].Latitude);
                        result.Longitude = Convert.ToDouble(resultTemp[0].Longitude);
                        result.Description = resultTemp[0].Description;

                        return result;
                    }
                    else
                    {
                        Messenger.Default.Send(new NotificationMessage("Well Not Found !"));
                    }
                }                                

            }
            catch (Exception e)
            {
                Messenger.Default.Send(new NotificationMessage(e.Message));
            }
            
            return null;
        }

        public async Task UpdateTicket(string ticketID, JObject bodyToSend)
        {
            var uri = new Uri(baseURL + "/_vti_bin/listdata.svc/Issue("+ ticketID + ")");

            var content = new StringContent(bodyToSend.ToString(), System.Text.Encoding.UTF8,"application/json");
            client.DefaultRequestHeaders.Add("X-HTTP-Method", "MERGE");
            client.DefaultRequestHeaders.Add("If-Match", "*");

            try
            {
                var response = await client.PostAsync(uri, content);               
                Messenger.Default.Send(new NotificationMessage("Modification saved!"));
            }
            catch(Exception e)
            {
                Messenger.Default.Send(new NotificationMessage(e.Message));
            }

        }

        public async Task<List<TicketItem>> GetOpenTicket()
        {
            var uri = new Uri(baseURL + "/_vti_bin/listdata.svc/Issue()?$filter=StatusValue eq 'Assigned'&$expand=SpvInCharge,CrewInCharge");
            List<TicketItem> result = new List<TicketItem>();            

            try
            {                
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var contentTemp = JsonConvert.DeserializeObject<TicketResponseJSON>(content);

                    if(contentTemp.d.results.Count > 0)
                    {
                        foreach (var ticket in contentTemp.d.results)
                        {
                            TicketItem ticketTemp = new TicketItem();
                            ticketTemp.ID = ticket.Issue_ID;
                            ticketTemp.JobName = ticket.JobNameValue;
                            ticketTemp.Location = ticket.Location;
                            ticketTemp.Status = ticket.StatusValue;
                            ticketTemp.Detail = ticket.JobNameValue + " | " + ticket.Location + " | " + ticket.StatusValue;
                            ticketTemp.Remarks = ticket.Remarks;
                            ticketTemp.IssueDate = ticket.IssueDate;
                            ticketTemp.SpvInCharge = ticket.SpvInCharge;
                            ticketTemp.CrewInCharge = ticket.CrewInCharge.results;

                            result.Add(ticketTemp);
                        }

                        return result;
                    }
                    else
                    {
                        Messenger.Default.Send(new NotificationMessage("No Ticket !"));
                    }
                    
                }     
                else
                {
                    Messenger.Default.Send(new NotificationMessage(response.StatusCode.ToString()));
                }
            }
            catch (Exception e)
            {
                Messenger.Default.Send(new NotificationMessage(e.Message));
            }

            return null;
        }
    }
}
