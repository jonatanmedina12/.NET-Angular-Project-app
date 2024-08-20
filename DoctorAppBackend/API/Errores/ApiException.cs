namespace API.Errores
{
    public class ApiException :ApiErrorResponse
    {
        public ApiException(int statusCode, string mensaje, string detalle=null):base(statusCode,mensaje)
        {
         
            Detalle = detalle;
        }

        public string Detalle { get; set; }
    }
}
