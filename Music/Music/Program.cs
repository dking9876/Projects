using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music
{
    class Program
    {   //מקבלת תעודת זהות ומערך תזמורות
        //מחזירה באיזה תזמורת הבנאדם עם התעודת זהות נמצא הכי הרבה שנים
        public static string OrchestraWithMostYears(int id, Orchestra[] arrOrchestras)
        {
            int maxYears = 0;
            string orcehstraName = "";
            for (int i = 0; i < arrOrchestras.Length; i++)
            {
                Orchestra OrchestraInArr = arrOrchestras[i];
                Musician[] arrMusician = OrchestraInArr.GetArrMusicians();
                for (int j = 0; j < arrMusician.Length; j++)
                {
                    if (arrMusician[j] != null)
                    {
                        if (arrMusician[j].GetId() == id)
                        {
                            if (arrMusician[j].GetYearsInOrchestra() > maxYears)
                            {
                                maxYears = arrMusician[j].GetYearsInOrchestra();
                                orcehstraName = OrchestraInArr.GetName();
                            }
                        }
                    }
                }

            }
            return orcehstraName;
        }
        //מקבלת מערך מחרוזות שם של כלי נגינה ומערך ריק 
        //מחזירה מערך עם התעודות זהות של כל הנגנים שמחשקים בכלי נגינה 
        public static int[] AllMusiansPlayInstrument(Orchestra[] arrOrchestras, string instrument, int[] arrId)
        {
            int moneId = 0;
            for (int i = 0; i < arrOrchestras.Length; i++)
            {
                Orchestra OrchestraInArr = arrOrchestras[i];
                Musician[] arrMusician = OrchestraInArr.GetArrMusicians();
                for (int j = 0; j < arrMusician.Length; j++)
                {
                    if (arrMusician[j] != null) 
                    {
                        string[] arrInstruments = arrMusician[j].GetArrInstrument();
                        for (int k = 0; k < arrInstruments.Length; k++)
                        {
                            if(arrInstruments[k] == instrument)
                            {
                                bool inArr = false;
                                for(int g = 0; g < arrId.Length; g++)
                                {
                                    if(arrId[g] == arrMusician[j].GetId())
                                    {
                                        inArr = true;
                                    }
                                }
                                if(inArr == false)
                                {
                                    arrId[moneId] = arrMusician[j].GetId();
                                    moneId++;
                                }
                            }
                        }

                    }
                }
            }
            return arrId;
        }
        //מקבלת מערך של תזמורות שם של תזמורת ומערך של כלי נגינה 
        //מחזירה את מספר הנגנים שמנגנים את כל הכלים שנמצאים במערך
        public static int NumberOfMusiciansPlayInstruments(Orchestra[] arrOrchestras, string orchestraName, string[] mr)
        {
            int moneMusicians = 0;
            int moneSameInstruments = 0;
            for (int i = 0; i < arrOrchestras.Length; i++)
            {
                if (arrOrchestras[i].GetName() == orchestraName)
                {
                    Orchestra OrchestraInArr = arrOrchestras[i];
                    Musician[] arrMusician = OrchestraInArr.GetArrMusicians();
                    for (int j = 0; j < arrMusician.Length; j++)
                    {
                        if (arrMusician[j] != null)
                        {
                            string[] arrInstruments = arrMusician[j].GetArrInstrument();
                            moneSameInstruments = 0;
                            for (int k = 0; k < mr.Length; k++)
                            {
                                for (int g = 0; g < arrInstruments.Length; g++)
                                {
                                    if(mr[k] == arrInstruments[g])
                                    {
                                        moneSameInstruments++;
                                    }
                                }
                            }
                            if(moneSameInstruments == mr.Length)
                            {
                                moneMusicians++;
                            }

                        }
                    }
                }

            }
            return moneMusicians;
        }

       
        
        static void Main(string[] args)
        {
            Test.TestEx1();
            Test.TestEx2();
            Test.TestEx3();

        }
    }
}
