namespace CryptographyTest
{
    public class Throw
    {
        public Throw(int value1, int value2)
        {
            Value = (value1 >= value2) 
                ? value1 * 10 + value2 
                : value2 * 10 + value1;
        }

        public int Value { get; set; }
    }
}
