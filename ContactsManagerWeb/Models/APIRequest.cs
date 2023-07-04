using static ContactsManagerUtility.SD;

namespace ContactsManagerWeb.Models {
    public class APIRequest {

        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public APIParams Headers { get; set; }

        public class APIParams
        {
            public Dictionary<string, string> Params { get; set; }
        }

    }

}
