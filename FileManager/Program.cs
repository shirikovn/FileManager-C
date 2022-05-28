using System;
using System.IO;

namespace FileManager
{
    class Program
    {

        static string msg;

        static void Main(string[] args)
        {

            while (true)
            {
                Screen();
                Console.WriteLine("");

                if (msg != "")
                {
                    Console.WriteLine(msg);
                }
                msg = "";

                Console.Write("- ");
                string command = Console.ReadLine();
                string[] splittedCommand = command.Split(" ");

                if(splittedCommand[0] == "exit")
                {
                    break;
                } 

                if (splittedCommand[0] == "cd") CD(splittedCommand[1]);

                else if (splittedCommand[0] == "createFile") CreateFile(splittedCommand[1]);
                else if (splittedCommand[0] == "deleteFile") DeleteFile(splittedCommand[1]);

                else if (splittedCommand[0] == "createDir") CreateDirectory(splittedCommand[1]);
                else if (splittedCommand[0] == "deleteDir") DeleteDirectory(splittedCommand[1]);

                else msg = "Command not found";
            }

        }

        static void Screen()
        {
            Console.Clear();

            var files = Directory.GetFiles(Directory.GetCurrentDirectory());
            var dirs = Directory.GetDirectories(Directory.GetCurrentDirectory());

            for (int i = 0; i < dirs.Length; i++)
            {
                Console.WriteLine(dirs[i].Split('\\')[dirs[i].Split('\\').Length - 1]);
            }

            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine(files[i].Split('\\')[files[i].Split('\\').Length - 1]);
            }

        }

        static void CreateFile(string fileName)
        {
            if (File.Exists(fileName)) { msg = "File already exists"; }
            else
            {
                var outFile = File.Create(fileName);
                outFile.Close();
            }
        }

        static void  CreateDirectory(string directoryName)
        {
            if (Directory.Exists(directoryName)) msg = "Directory already exists";
            else Directory.CreateDirectory(directoryName);
      
        }

        static void DeleteDirectory(string directoryName)
        {
            if (!Directory.Exists(directoryName)) msg = "There is no such directory";
            else {
                Console.WriteLine("Delete? Y / N");
                string ans = Console.ReadLine();

                if (ans == "Y")
                {
                    Directory.Delete(directoryName);
                }
            }
        }

        static void DeleteFile(string fileName)
        {
            if (!File.Exists(fileName)) msg = "There is no such file";
            else
            {
                Console.WriteLine("Delete? Y / N");
                string ans = Console.ReadLine();

                if (ans == "Y")
                {
                    File.Delete(fileName);
                }
            }
        }

        static void CD(string fullDirectory)
        {
            if (!fullDirectory.Contains("\\"))
            {
                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\" + fullDirectory)) { msg = "Cannot find such directory"; }
                else { Directory.SetCurrentDirectory(Directory.GetCurrentDirectory() + "\\" + fullDirectory); }                
                return;
            }

            if(fullDirectory == "..")
            {
                string[] a = Directory.GetCurrentDirectory().Split("\\");

                a[a.Length - 1] = "";

                string directory = "";

                for (int i = 0; i < a.Length; i++)
                {
                    directory += a[i];
                }

                Directory.SetCurrentDirectory(directory);
            }

            if (!Directory.Exists(fullDirectory)) msg = "Cannot find such directory";
            else Directory.SetCurrentDirectory(fullDirectory);
        }

    }
}
