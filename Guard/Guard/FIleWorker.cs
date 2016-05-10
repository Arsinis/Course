using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Guard
{
    public class FIleWorker
    {
        public List<String> Read(String fileName) 
        {
        List<String> data = new List<String>();
            try
            {
                StreamReader fs = new StreamReader(fileName);
                try
                {
                    while (true)
                    {
                        string temp = fs.ReadLine();
                        if (temp == null)
                            break;
                        data.Add(temp);
                    }
                }
                finally
                {
                    fs.Close();
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            return data;
	    }
    }
}
