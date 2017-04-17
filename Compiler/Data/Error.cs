namespace Compiler.Data
{
    public struct Error
    {
        public Error(string message, int line = 0)
        {
            Message = message;
            Line = line;
        }

        public string Message { get; }
        public int Line { get; set; }
    }
}