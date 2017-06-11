using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HciProject2.Model
{
    public class FileName
    {
        private String name;

        public FileName()
        {

        }

        public FileName(String f)
        {
            this.name = f;
        }

        public String Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }
    }
}
