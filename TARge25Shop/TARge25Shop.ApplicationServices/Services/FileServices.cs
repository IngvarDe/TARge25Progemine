using Microsoft.EntityFrameworkCore;
using TARge25Shop.Core;
using TARge25Shop.Core.Domain;
using TARge25Shop.Core.Dto;
using TARge25Shop.Data;
using Microsoft.Extensions.Hosting;

namespace TARge25Shop.ApplicationServices.Services;

public class FileServices :IFileServices
{
    private readonly TARge25ShopContext _dbContext;
    private  readonly IHostEnvironment _webHost;
    
    public FileServices(TARge25ShopContext dbContext, IHostEnvironment webHost)
    {
        _dbContext = dbContext;
        _webHost = webHost;
    }

    public void ConvertFilesToApi(SpaceshipDto dto, Spaceship spaceship)
    {
        if (dto.Files != null && dto.Files.Count >= 0)
        {
            string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "wwwroot", "multipleFileUpload");
            
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            foreach (var file in dto.Files)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string fullPath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                    
                    FileToApi path = new FileToApi
                    {
                        Id = Guid.NewGuid(),
                        ExistingFilePath = uniqueFileName,
                        SpaceshipId = spaceship.Id
                    };

                    _dbContext.FilesToApis.Add(path);
                }
            }
        }
    }

    public async Task<bool> RemoveImageFromApi(FileToApiDto dto)
    {
        var image = await _dbContext.FilesToApis
            .FirstOrDefaultAsync(x => x.Id == dto.Id);
        
        if (image == null) return false;
        
        var filePath = Path.Combine(_webHost.ContentRootPath, "wwwroot", "multipleFileUpload", image.ExistingFilePath);
        
        if (File.Exists(filePath)) File.Delete(filePath);

        _dbContext.FilesToApis.Remove(image);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }
}