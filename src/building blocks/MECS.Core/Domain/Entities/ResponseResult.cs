using System.Collections.Generic;

namespace MECS.Core.Domain.Entities
{
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
