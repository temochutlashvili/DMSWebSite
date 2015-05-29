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
            while (File.Exists(Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename) + "-" + fileNumber.ToString() + Path.GetExtension(filename)))){
                fileNumber++;
            }
            return Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename) + "-" + fileNumber.ToString() + Path.GetExtension(filename));
        }

        return filename;
    }

    public static void WriteToLog(string message)
    {
        //using (StreamWriter w = File.AppendText("C:\\inetpub\\wwwroot\\telasi\\dms\\Files\\log.txt"))
        using (StreamWriter w = File.AppendText("c:\\log.txt"))
        {
            w.WriteLine(message);
            w.Flush();
            w.Close();
        }
    }
}