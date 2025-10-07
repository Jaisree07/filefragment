using System;
using System.Collections.Generic;

namespace FileFragmentationProject.View
{
    public class FileView
    {
        public string GetParagraph()
        {
            Console.WriteLine("Enter paragraph");
            string paragraph=Console.ReadLine();
            return paragraph;
        }
        public int GetFragmentSize()
        {
            Console.WriteLine("Enter fragment size:");
            int size; 
            while (!int.TryParse(Console.ReadLine(), out size)||size <= 0)
            {
                Console.WriteLine("Invalid input\nEnter a positive number");
            }
            return size;
        }
        public string GetFileName()
        {
            Console.WriteLine("Enter fragment file name to verify (e.g., 001.txt):");
            string fileName = Console.ReadLine(); 
            return fileName;
        }
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
        public void ShowFiles(List<string> files)
        {
            Console.WriteLine("Created fragment files");
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
        }
        public void ShowContent(string content)
        {
            Console.WriteLine("Content");
            Console.WriteLine(content);
        }
    }
}
