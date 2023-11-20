using Infrastructure;
using WebAPP.Models;

namespace WebAPP.Helper
{
    public interface IFileUploadService
    {
        Response Upload(FileUploadModel request);
    }
}
