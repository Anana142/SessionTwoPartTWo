using SessionTwoPartTWo.DataBaseFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionTwoPartTWo.Tools
{
    public class DataBase
    {
        private static User04Context instance1;

        public DataBase()
        {

        }
        public static User04Context instance { get { 
            
                if(instance1 == null)
                    instance1 = new User04Context();
                return instance1;
            
            } set => instance1 = value; }
    }
}
