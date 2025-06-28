using QuanLyCuaHangDoAnNhanh.DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangDoAnNhanh.BLL
{
    class UpLoadImageAndSave
    {
        public string SaveImageAndGetRelativePath(string sourceFilePath)
        {
            string imageDir = Path.Combine(Application.StartupPath, "Images");
            if (!Directory.Exists(imageDir))
                Directory.CreateDirectory(imageDir);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(sourceFilePath);
            string destinationPath = Path.Combine(imageDir, fileName);
            File.Copy(sourceFilePath, destinationPath);
            return Path.Combine("Images", fileName);
        }

        public void UpdateFoodImage(int foodId, string relativeImagePath)
        {
            FoodDAO.Instance.UpdateImageFood(foodId, relativeImagePath);
        }
    }
}
