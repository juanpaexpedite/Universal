using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universal.Managers
{
    public class UriManager
    {
        public static Uri GetFilUriFromString(string uri)
        {
            string appprefix = "ms-appx:///";
            if (uri.Contains(appprefix))
                return new Uri(uri);
            else
            {
                uri.TrimStart('/');
                return new Uri($"{appprefix}{uri}");
            }
        }

    }
}
