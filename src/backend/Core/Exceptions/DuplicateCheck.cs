namespace Core.Exceptions
{
    public class DuplicateCheck
    {

        public void SuperFunction()
        {
            SuperDup1(5, 10);
            SuperDup1(10, 5);
        }

        public string SuperDup1(int a, int b)
        {
            var c = string.Empty;

            if (a > b)
            {
                c += "123";
                c += "234";
                c += "345";
            }

            c += "456";
            c += "567";

            if (b > a)
            {
                c += "123";
                c += "234";
                c += "345";
            }

            return c;
        }

        public string SuperDup2(int a, int b)
        {
            var c = string.Empty;

            if (a > b)
            {
                c += "123";
                c += "234";
                c += "345";
            }

            c += "456";
            c += "567";

            if (b > a)
            {
                c += "123";
                c += "234";
                c += "345";
            }

            return c;
        }
    }
}
