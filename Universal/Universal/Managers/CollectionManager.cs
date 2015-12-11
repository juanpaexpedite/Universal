using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Universal.Managers.ExceptionManager;

namespace Universal.Managers
{
    public class CollectionManager
    {
        public static List<object> ToObjectList(object source)
        {
            try
            {
                var ilist = source as IList;
                return ilist.Cast<object>().ToList();
            }
            catch (Exception ex)
            {
                Launch(ex);
                return null;
            }
        }
    }
}
