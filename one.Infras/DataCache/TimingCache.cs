using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace one.Infras.DataCache
{


    public class TimingCache
    {







    }




}


/*


private static readonly object locker = new object();
 ...
 string key = "myCustomObjKey";
 
// attempt to retrieve the data from the cache
 CustomObj customObj = HttpRuntime.Cache[ key ] as CustomObj;
 
 
// now check the local variable
 if ( customObj == null )
 {
     // lock access here
    lock ( locker )
     {
        // check one more time
        customObj = HttpRuntime.Cache[ key ] as CustomObj;
 
 
        // now check the local variable
         if ( customObj == null )
         {
             // the object was null.  We need to repopulate it
             customObj = GetCustomObj();
 
             // place it in the cache
             HttpRuntime.Cache.Insert(
                 key
                 , customObj
                 , null
                 , DateTime.Now.AddMinutes( 10 )
                 , Cache.NoSlidingExpiration
                 );
         }
    }
}
 
 // now it is assumed to be set.  We return it to the caller
 return customObj;

*/