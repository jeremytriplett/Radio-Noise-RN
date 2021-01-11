using RNG.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RNG
{
    class RNGController
    {
        //public static void Add(string s)
        //{
        //    OracleDatabase.Add(s);
        //}

        //public static void InsertRNGBit(int bitValue, int bitNum)
        //{
        //    OracleDatabase.InsertRNGBit(bitValue, bitNum);
        //}

        public static Stack<bool> GetRandomBitsStack(string fileName)
        {
            return RNGLogic.GetRandomBitsStack(fileName);
        }

        public static int GetRandomNumber(string min, string max, Stack<bool> randBits)
        {
            return RNGLogic.GetRandomNumber(Convert.ToInt32(min), Convert.ToInt32(max), randBits);
        }
    }

}
