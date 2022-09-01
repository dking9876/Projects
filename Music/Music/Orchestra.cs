using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music
{
    class Orchestra
    {
        //הגדרת תכונות
        private string name;
        private Musician[] arrMusicians = new Musician[20];
        private int place = 0;
        //פעולה בונה
        public Orchestra(string name)
        {
            this.name = name;
        }
        //מחזירה את השם
        public string GetName()
        {
            return this.name;
        }
        //מחזירה את הרשימה של הנגנים
        public Musician[] GetArrMusicians()
        {
            return this.arrMusicians;
        }
        //משנה את השם
        public void SetName(string name)
        {
            this.name = name;
        }
        //מוסיפה נגן לרשימת הנגנים 
        public void AddMusician(Musician m)
        {
            this.arrMusicians[place] = m;
            this.place++;
        }
    }
}
