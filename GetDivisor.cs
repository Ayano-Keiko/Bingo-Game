using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoGame
{
    internal class GetDivisor
    {
        public static (int row, int col) getDivisor(int number)
        {
            /*
            Get Two numbers that addition of them is greatest
            The Logic: To get two divisors has most close distance
            */

            int closestDiatance = number;
            int rowNumber = 0;
            int colNumber = 0;

            for (int i = 2; i <= number / 2; ++i)
            {


                if (number % i == 0)
                {
                    // get another divisor
                    int aDivisor = number / i;
                    // get the diatance(Absolute Value) between two divisor
                    int current_distance = Math.Abs(aDivisor - i);

                    // if current distance shorter than the closest value,
                    // change closest value
                    if (current_distance < closestDiatance)
                    {
                        closestDiatance = current_distance;
                        rowNumber = i;
                        colNumber = aDivisor;
                    }
                }

            }

            return (rowNumber, colNumber);
        }
    }
}
