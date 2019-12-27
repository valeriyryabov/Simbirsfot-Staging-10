using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimbirsfotStaging10.BLL.Infrastructure
{
    public class OperationDetail
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }

        public async static Task<OperationDetail> TryCatchWithDetailAsync( Func<Task> action)
        {
            try
            {
                await action();
                return new OperationDetail { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new OperationDetail { Succeeded = false, Message = ex.Message };
            }                
        }
    }
}
