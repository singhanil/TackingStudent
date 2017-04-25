using StudentTracking.Application.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentTracking.Application.Models.Models;
using StudentTracking.Data;
using System.IO;

namespace StudentTracking.Application.Main
{
    public class GalleryService : IGalleryService
    {
        private StudentTrackingContext _dbContext = null;

        public GalleryService(StudentTrackingContext cntx)
        {
            this._dbContext = cntx;
        }

        public ICollection<ImageDetailModel> Get(int schoolId, int pageNumber, int pageSize, out int count)
        {
            string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileStore", schoolId.ToString(), "Gallery");
            ICollection<ImageDetailModel> response;

            ICollection<ImageDetailModel> galleryList = new List<ImageDetailModel>();

            galleryList.Add(new ImageDetailModel { Name = "Gallery.png", Title = "School Gallery" });
            galleryList.Add(new ImageDetailModel { Name = "Building.jpg", Title = "School Building" });
            galleryList.Add(new ImageDetailModel { Name = "ClassRoom.jpg", Title = "State of art classrooms" });
            galleryList.Add(new ImageDetailModel { Name = "ClassRoom_1.jpg", Title = "Class room" });
            galleryList.Add(new ImageDetailModel { Name = "PlayGround.jpg", Title = "Play Ground" });
            galleryList.Add(new ImageDetailModel { Name = "KarateSession.jpg", Title = "Karate Session" });
            galleryList.Add(new ImageDetailModel { Name = "schoolBus.jpg", Title = "School Bus" });
            galleryList.Add(new ImageDetailModel { Name = "stationary.jpg", Title = "Child freindly stationaries" });

            count = galleryList.Count();
            var totalImages = pageNumber * pageSize;

            response = galleryList.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

            foreach(ImageDetailModel model in response)
            {
                Byte[] bytes = File.ReadAllBytes(Path.Combine(directory, model.Name));
                model.Data = Convert.ToBase64String(bytes);
            }

            return response;
        }
    }
}
