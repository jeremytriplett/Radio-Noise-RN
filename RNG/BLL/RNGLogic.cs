using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RNG.BLL
{
    class RNGLogic
    {

        public static Stack<bool> GetRandomBitsStack(string fileName)
        {

            Stack<bool> randBits = new Stack<bool>();
            
            byte[] dataBytes = File.ReadAllBytes(fileName).Skip(44).ToArray();

            BitArray dataBits = new BitArray(dataBytes);

            int position = 0;

            foreach (bool bit in dataBits)
            {

                if (position % 16 == 0)
                {
                    randBits.Push(bit);
                }

                position++;

            }

            return randBits;

        }

        public static int GetRandomNumber(int min, int max, Stack<bool> randBits)
        {

            int range = max - min;

            int numBitsToTake = (int)Math.Floor(Math.Log((double)range, 2) + 1);

            int value;
           
                do
                {
                    string bitsString = "";

                    for (int i = 0; i < numBitsToTake; i++)
                    {
                        bitsString += randBits.Pop() == true ? "1": "0";
                    }

                    value = Convert.ToInt32(bitsString, 2);

                }
                while (value > range);

            return (value + Convert.ToInt32(min));

        }
    }
}