using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Results
{
    public class ResultsGeneric <T> where T : class
    {
        public bool IsSuccessed { get; set; }
        public string Messsage { get; set; } = "";
        public T? Data { get; set; }

        public static ResultsGeneric<T> Success (T data, string message)
        {
            return new ResultsGeneric<T>
            {
                IsSuccessed = true,
                Messsage = message,
                Data = data
            };
        }

        public static ResultsGeneric<T> Fail( string message)
        {
            return new ResultsGeneric<T>
            {
                IsSuccessed = false,
                Messsage = message,
                Data = null,
            };
        }
    }
}
