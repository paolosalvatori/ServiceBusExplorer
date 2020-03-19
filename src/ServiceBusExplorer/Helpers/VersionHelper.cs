#region Copyright
//=======================================================================================
// Microsoft Azure Customer Advisory Team 
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// LICENSED UNDER THE APACHE LICENSE, VERSION 2.0 (THE "LICENSE"); YOU MAY NOT USE THESE 
// FILES EXCEPT IN COMPLIANCE WITH THE LICENSE. YOU MAY OBTAIN A COPY OF THE LICENSE AT 
// http://www.apache.org/licenses/LICENSE-2.0
// UNLESS REQUIRED BY APPLICABLE LAW OR AGREED TO IN WRITING, SOFTWARE DISTRIBUTED UNDER THE 
// LICENSE IS DISTRIBUTED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY 
// KIND, EITHER EXPRESS OR IMPLIED. SEE THE LICENSE FOR THE SPECIFIC LANGUAGE GOVERNING 
// PERMISSIONS AND LIMITATIONS UNDER THE LICENSE.
//=======================================================================================
#endregion

#region Using Directives

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBusExplorer.Forms;
using Newtonsoft.Json.Linq;

#endregion


namespace Microsoft.Azure.ServiceBusExplorer.Helpers
{
    public static class VersionHelper
    {
        #region Public Static Methods

        public static async Task<string> RetrieveLatestReleaseFromGitHubAsync()
        {
            try
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri("https://api.github.com")
                };
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("ServiceBusExplorer")));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var httpResponseMessage = await httpClient.GetAsync("/repos/paolosalvatori/servicebusexplorer/releases/latest").ConfigureAwait(false);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var json = await httpResponseMessage.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(json);
                    return (string)jObject.SelectToken("name");
                }
            }
            catch (Exception ex)
            {
                MainForm.SingletonMainForm.HandleException(ex);
            }
            return null;
        }
        #endregion
    }
}
