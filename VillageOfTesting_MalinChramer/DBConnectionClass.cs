using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageOfTesting_MalinChramer
{
    public class DBConnectionClass

    {
        public virtual int GetWood() 
        {
            return 3;
        }

        public void Save(Village village)
        {
             /* Save tar ALLA variabler som är värda att spara, 
             * och skall spara dem till databasen… fast ni behöver
             * inte faktiskt implementera den.
             */
        }

        public virtual void Load()
        {          
            // denna ska mockas.
            // Load() är en funktion som inte ger tillbaka någonting
        }
    }
}
