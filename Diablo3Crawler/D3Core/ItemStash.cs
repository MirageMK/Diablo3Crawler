using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using log4net;

namespace D3Core
{
    public static class ItemStash
    {
        private static readonly ILog log = LogManager.GetLogger
            (MethodBase.GetCurrentMethod().DeclaringType);

        private const string spreadsheetId = "11H_H_s805b-k7jWFoe8dXNjoy7QZa6ptr3GfgJslsrQ";

        private static List<string> _ownedItems;
        public static List<string> OwnedItems => _ownedItems ?? GetOwnedItems();

        private static List<string> GetOwnedItems()
        {
            try
            {
                log.Error("called");
                SheetsService service = GoogleSheetsWraper.GetSheetsService();
                log.Error("suceeed");
                // Define request parameters.
                string range = "Items!A1:D";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

                ValueRange response = request.Execute();
                IList<IList<object>> values = response.Values;
                if(values != null && values.Count > 0)
                {
                    _ownedItems = new List<string>();
                    foreach(IList<object> row in values.Skip(1))
                    {
                        _ownedItems.Add(row[0].ToString());
                    }
                }
            }
            catch(Exception ex)
            {
                log.Error("EX", ex);
            }
            return _ownedItems;
        }
    }
}
