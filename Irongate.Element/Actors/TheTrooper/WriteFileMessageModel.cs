namespace Irongate.Element.Actors.TheTrooper
{
    internal class WriteFileMessageModel
    {
        public int Firecode { get; private set; }
        public string Message { get; private set; }

        public WriteFileMessageModel(int fireCode, string message)
        {
            Firecode = fireCode;
            Message = message;
        }
    }
}