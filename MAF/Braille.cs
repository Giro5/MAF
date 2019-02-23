using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAF.Braille
{
    /// <summary>
    /// dont work
    /// </summary>
    public class Braille
    {
        private string body;
        private string code;

        public Braille(string code)
        {
            body = code;
            this.code = code.Select(x => ((char)(x + 2000))).ToString();
        }
    }
}
