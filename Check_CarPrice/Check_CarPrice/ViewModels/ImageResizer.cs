using Android.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Check_CarPrice.ViewModels
{
    public static class ImageResizer
    {
        static ImageResizer()
        {
        }
        public static async Task<byte[]> ResizeImage(byte[] imageData, float width, float height)
        {

            return ResizeImageAndroid(imageData, width, height);
#if __IOS__
            return ResizeImageIOS(imageData, width, height);
#endif
#if __ANDROID__
			return ResizeImageAndroid ( imageData, width, height );
#endif
#if WINDOWS_UWP
            return await ResizeImageWindows(imageData, width, height);
#endif
        }

        public static byte[] ResizeImageAndroid(byte[] imageData, float width, float height)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }

    }
}
