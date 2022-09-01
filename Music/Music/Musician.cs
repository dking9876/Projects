using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music
{
    class Musician
    {// הגדרת תכונות
        private int id;
        private int yearsInOrchestra;
        private string[] arrInstrument;

        //פעולה בונה 
        public Musician(int id, int yearsInOrchestra, string[] arrInstrument)
        {
            this.id = id;
            this.yearsInOrchestra = yearsInOrchestra;
            this.arrInstrument = arrInstrument;
        }//מחזירה את התעודת זהות
        public int GetId()
        {
            return this.id;
        }
        //מחזירה את מספר השנים בתזמורת 
        public int GetYearsInOrchestra()
        {
            return this.yearsInOrchestra;
        }
        //מחזירה את אוסף הכלים שהנגן משחק 
        public string[] GetArrInstrument()
        {
            return arrInstrument;
        }
        //לשנות את התעודת זהות
        public void SetId(int id )
        {
            this.id = id;
        }
        //לשנות את מספר השנים בתזמורת
        public void SetYearsInOrchestra(int YearsInOrchestra)
        {
            this.id = YearsInOrchestra;
        }



    }
}
