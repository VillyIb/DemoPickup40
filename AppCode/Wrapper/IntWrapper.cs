namespace AppCode.Wrapper
{
    public class IntWrapper
    {
        protected int Payload { get; set; }


        public static implicit operator int (IntWrapper source)
        {
            return source.Payload;
        }


        public static explicit operator IntWrapper (int source)
        {
            return new IntWrapper (source);
        }


        public IntWrapper(int source)
        {
            Payload = source;
        }

        public void X(IntWrapper value)
        {
            
        }

        public void Test()
        {
            IntWrapper t1 = (IntWrapper)10;


            X(t1);

            X(new IntWrapper(15));
        }



    }


}
