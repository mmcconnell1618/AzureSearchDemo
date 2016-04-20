using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDemo.Data
{
    public class Utilities
    {
        private Random r = new Random(System.DateTime.Now.Millisecond);
        public Utilities()
        {
           
        }

        public string PadStringLeft(string sourceString, int maxLength, string padCharacter)
        {

            string result = "";

            if (sourceString.Length > maxLength)
            {
                result = sourceString.Substring(0, maxLength);
            }
            else
            {
                result = sourceString;
                while (result.Length < maxLength)
                {
                    result = padCharacter + result;
                }
            }

            return result;
        }

        public int RandomInteger(int minNumber, int maxNumber)
 		{
 			if (maxNumber<minNumber) { 
 				int temp = maxNumber; 
 				maxNumber = minNumber; 
 				minNumber = temp; 
 			}  
 			// .NET random function is not inclusive of max value so add one 
 			maxNumber = maxNumber + 1; 
 
            return r.Next(minNumber, maxNumber); 
 		} 


    }
}
