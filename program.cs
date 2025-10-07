using FileFragmentationProject.Model;
using FileFragmentationProject.View;
using FileFragmentationProject.Controller;
namespace FileFragmentationProject
{
    class Program
    {
        static void Main()
        {
            FileModel model = new FileModel();
            FileView view = new FileView();
            FileController controller = new FileController(model, view);
            controller.Run();
        }
    }
}
