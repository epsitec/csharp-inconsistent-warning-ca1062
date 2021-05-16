//	Copyright © 2021, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

namespace InconsistentWarning
{
    class Program
    {
        static void Main()
        {
            _ = new Foo ("Hello");
        }
    }

    public class Foo
    {
        public Foo(string x)
        {
            this.x = x; // does not warn
            this.len = x.Length; // warns (OK) -- CA1062
        }

        public void SetX(string x)
        {
            this.x = x; // does not warn
            this.len = x.Length; // warns (OK) -- CA1062
        }

        public string X => this.x;
        public int Len => this.len;

        private string x;
        private int len;
    }
}
