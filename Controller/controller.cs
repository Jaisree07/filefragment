using System;
using System.Collections.Generic;
using FileFragmentationProject.Model;
using FileFragmentationProject.View;

namespace FileFragmentationProject.Controller
{
    public class FileController
    {
        private readonly FileModel model;
        private readonly FileView view;
        public FileController(FileModel model, FileView view)
        {
            this.model = model;
            this.view = view;
        }
        public void Run()
        {
            bool restart=true;

            while(restart)
            {
                try
                {
                    model.DeleteAllFiles();
                    string paragraph=view.GetParagraph();
                    model.CreateFile(paragraph);

                    int fragmentSize = view.GetFragmentSize();
                    List<string> fragments = model.FragmentFile(fragmentSize);
                    view.ShowFiles(fragments);

                    bool openMore=true;
                    while (openMore)
                    {
                        string verifyFile = view.GetFileName();
                        try
                        {
                            string fragmentContent=model.ReadFragment(verifyFile);
                            view.ShowContent(fragmentContent);
                        }
                        catch (Exception ex)
                        {
                            view.ShowMessage(ex.Message);
                        }

                        Console.Write("\nDo you want to open another fragment file(y/n)?");
                        openMore=Console.ReadLine().Trim().ToLower() is "yes" or "y";
                    }

                    Console.Write("\nDo you want to start defragmentation(y/n)?");
                    if (Console.ReadLine().Trim().ToLower() is "yes" or "y")
                    {
                        string reassembled=model.DefragmentFiles(fragments);
                        view.ShowMessage("\nDefragmentation complete!");
                        view.ShowMessage($"Reassembled content stored in '{model.OutputFile}'");
                        view.ShowContent(reassembled);

                        bool success=model.CompareFiles();
                        view.ShowMessage(success?"\nSuccess: Input and Output files match":"\nWarning: Files do not match");
                    }
                    else
                    {
                        view.ShowMessage("\nDefragmentation skipped");
                    }
                    Console.Write("\nDo you want to delete files,restart,orexit?(delete/restart/exit): ");
                    string choice = Console.ReadLine().Trim().ToLower();

                    switch (choice)
                    {
                        case "delete":
                            model.DeleteAllFiles();
                            view.ShowMessage("\nAll generated files have been deleted");
                            restart = false;
                            break;
                        case "restart":
                            view.ShowMessage("\nRestarting\n");
                            restart = true;
                            break;
                        default:
                            view.ShowMessage("\nExiting program");
                            restart = false;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    view.ShowMessage("An error occurred: "+ex.Message);
                    restart = false;
                }
            }
        }
    }
}
