namespace AgendaFrontEnd.Data
{
    public class ResponseObject
    {
        public ResponseObject()
        {
            Obj = null;
            Status = 0;

        }

        public string Mensaje { get; set; }

        public dynamic Obj { get; set; }

        public byte Status { get; set; }
    }
}
