using Newtonsoft.Json;

namespace RequestManager.Model
{
    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
