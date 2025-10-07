using System;
using System.Text;
using System.Collections.Generic;

namespace FileFragmentationProject.View
{
    public class FileView
    {
        public string GetParagraph()
        {
            Console.WriteLine("Enter your paragraph (Press TAB to finish)");
            StringBuilder paragraph=new StringBuilder();
            while(true)
            {
                ConsoleKeyInfo key=Console.ReadKey(intercept: true);
                if (key.Key==ConsoleKey.Tab)
                {
                    Console.WriteLine(); 
                    break;
                }
                else if (key.Key==ConsoleKey.Enter)
                {
                    paragraph.AppendLine(); 
                    Console.WriteLine();  
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (paragraph.Length>0)
                    {
                        paragraph.Length--;
                        int cursorLeft=Console.CursorLeft;
                        int cursorTop=Console.CursorTop;
                        if (cursorLeft==0 && cursorTop>0)
                        {
                            cursorTop--;
                            cursorLeft=Console.BufferWidth-1;
                        }
                        else
                        {
                            cursorLeft--;
                        }
                        Console.SetCursorPosition(cursorLeft,cursorTop);
                        Console.Write(" "); 
                        Console.SetCursorPosition(cursorLeft,cursorTop);
                    }
                }
                else
                {
                    paragraph.Append(key.KeyChar);
                    Console.Write(key.KeyChar); 
                }
            }

            return paragraph.ToString();
        }

        public int GetFragmentSize()
        {
            Console.WriteLine("Enter fragment size");
            int size; 
            while (!int.TryParse(Console.ReadLine(),out size)||size <= 0)
            {
                Console.WriteLine("Invalid input\nEnter a positive number");
            }
            return size;
        }
        public string GetFileName()
        {
            Console.WriteLine("Enter fragment file name to check");
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
