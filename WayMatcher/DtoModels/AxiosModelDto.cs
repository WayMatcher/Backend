using WayMatcherBL.Enums;

namespace WayMatcherBL.DtoModels
{
    public class AxiosModelDto<T> where T : class
    {
        public T? Data { get; set; }
        public RESTCode Status { get; set; }
        public string StatusText { get; set; }
    }
}
