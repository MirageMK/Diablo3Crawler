using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using log4net;

namespace D3Core
{
    class GoogleSheetsWraper
    {
        private static readonly ILog log = LogManager.GetLogger
            (MethodBase.GetCurrentMethod().DeclaringType);
        
        private static readonly string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        private const string APPLICATION_NAME = "Hearthstone Crawler";

        public static SheetsService GetSheetsService()
        {
            log.Error("TEST");
            SheetsService service = new SheetsService(new BaseClientService.Initializer
                                                      {
                                                          ApiKey = ConfigurationManager.AppSettings["APIKey"]
                                                      });

            log.Error(ConfigurationManager.AppSettings["Environment"]);
            if (ConfigurationManager.AppSettings["Environment"] == "Debug")
            {
                log.Error("1");
                UserCredential credential;

                using (FileStream stream =
                    new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
                {
                    string credPath = Environment.GetFolderPath(
                                                                Environment.SpecialFolder.Personal);
                    log.Error(credPath);
                    credPath = Path.Combine(credPath, ".credentials/D3C");
                    log.Error(credPath);
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                                                                             GoogleClientSecrets.Load(stream).Secrets,
                                                                             Scopes,
                                                                             "user",
                                                                             CancellationToken.None,
                                                                             new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                    log.Error(credPath);
                }

                // Create Google Sheets API service.
                service = new SheetsService(new BaseClientService.Initializer
                                            {
                                                HttpClientInitializer = credential,
                                                ApplicationName = APPLICATION_NAME
                                            });
            }
            return service;
        }
    }
}
