using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW8AGU.Samples.CSharp.Search
{
    public class SuggestionProvider
    {
        private static List<string> data = null;
        public static Task<List<string>> GetSuggestionsAsync(string query)
        {
            return Task<List<string>>.Run(() =>
                {
                    PrepareData();
                    var suggestions = from d in data
                                      where d.ToUpper().Contains(query.ToUpper())
                                      select d;
                    return suggestions.Take(5).ToList();
                });

        }

        private static void PrepareData()
        {
            if (data == null)
                data = new List<string>
            {
"springfield",
"chicago",
"san francisco",
"San diego",
"philedelphia",
"orlando",
"miami",
"jacksonville",
"st.louis",
"cincinatti",
"cleveland",
"phoenix",
"tucson",
"kansas city",
"detroit",
"L.A",
"New york",
"Atlanta",
"Boston",
"Newark",
"Minniapolis",
"Indianapolis",
"Houston",
"Dallas",
"Ft.Worth",
"El Paso",
"Ft.Lauterdale",
"San Antonio",
            };
        }
    }
}
