using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Configuration;
using System.IO;


namespace BusinessLayer
{
    public static class clsUtil
    {
       public static bool CheckEmail(string email)
        {
           if (email.Length>64)
                return false;
            Regex regex = new Regex(ConfigurationManager.AppSettings["EmailRegExPattern"]);
            return regex.IsMatch(email);
        }

        public static bool CreateDirectory(string path)
        {
            if (Directory.Exists(path))
                return true;
            else
            {
                try
                {
                    Directory.CreateDirectory(path);
                    if (Directory.Exists(path))
                        return true;
                    else
                        return false;


                }
                catch (Exception e )
                {

                    throw new Exception("Can Not create Directory",e);
                }
             
            }
        }
        public static void WriteOnFile(string path, string content)
        {
            if (string.IsNullOrEmpty(content))
                return;
            using (StreamWriter writer = new StreamWriter(path,true))
            {
                try
                {
                    writer.WriteLine(content);
                }
                catch (Exception e)
                {
                    throw new Exception("Can not write on file", e);
                }
                
                
            }



        }
        public static void SaveSerializationOnFile(string JsonContent)
        {
            // return AppData file 
            string RomaingPath=Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appDirectory = Path.Combine(RomaingPath, "MyTasks");
            string FileToWriteOn = Path.Combine(appDirectory, "Tasks.json");
            try
            {
                CreateDirectory(appDirectory);
                WriteOnFile(FileToWriteOn, JsonContent);
            }
            catch (Exception e)
            {
                throw new Exception("Can not save on file ",e);
            }


        }
        public static readonly string SerializedObjectPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MyTasks\Tasks.json";
        public static async Task<List<string>> ReadFormSavedFileAsync(string Path)
        {
            if(!File.Exists(Path))
                return null;

            using (StreamReader reader = new StreamReader(Path))
            {
                List<string> result = new List<string>();
                string line;
                try
                {
                    while ((line = await reader.ReadLineAsync())!=null)
                    {
                        
                        result.Add(line);
                    }

                    return result;
                }
                catch (Exception e)
                {

                    throw new Exception("Can not read from file",e);
                }


            }
        }



    }
}
