using System.Collections.Generic;

namespace MECS.WebApp.MVC.Models
{
    public class ErrorViewModel
    {
        public int ErroCode { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

    }
    public class ResponseResult
    {
      
        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorMessages Errors { get; set; } = new ResponseErrorMessages();
    }
    public class ResponseErrorMessages
    {
        public ResponseErrorMessages()
        {
            Messages = new List<string>();
        }
        public IEnumerable<string> Messages { get; set; }
    }
}
