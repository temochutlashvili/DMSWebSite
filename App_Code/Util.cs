using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

public static class Util
{
    public static string GenerateUniqueFileName(string filename)
    {
        int fileNumber = 1;

        if (File.Exists(filename))
        {
            while (File.Exists(Path.Combine(filename, "-", fileNumber.ToString()))){
                fileNumber++;
            }
            return Path.Combine(filename, "-", fileNumber.ToString());
        }

        return filename;
    }
}