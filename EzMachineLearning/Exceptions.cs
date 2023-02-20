using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EzMachineLearning.Exceptions
{
    public class NonMatchingArraySizeException : ArgumentException
    {
        public NonMatchingArraySizeException() : base("Function expected two arrays of the same length and the arrays were not the same length.") { }
    }
}
