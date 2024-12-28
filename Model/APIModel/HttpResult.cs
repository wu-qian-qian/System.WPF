using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    //接口返回

    public class HttpResult
    {
        public int Code {  get; set; }

        public bool IsSuccess { get; set; }

        public object Result { get; set; }
    }
    public class HttpResult<T>
    {
        public int Code { get; set; }

        public bool IsSuccess { get; set; }

        public T Result { get; set; }
    }
}
