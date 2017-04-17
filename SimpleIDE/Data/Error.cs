namespace SimpleIDE.Data
{
    public class Error
    {
        public string Message { get; set; }
        public string Description { get; set; }
        public int Line { get; set; }
        public string FileName { get; set; }
        public int Code { get; set; }
    }
}