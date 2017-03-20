namespace CoreLeaf
{
    public class Startup
    {
        static void Main(string[] args)
        {
            var bs = new Bootstrapper();
            bs.Run(args);
        }
    }
}