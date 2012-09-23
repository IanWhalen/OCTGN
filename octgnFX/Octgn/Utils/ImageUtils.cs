// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageUtils.cs" company="OCTGN">
//   GNU Stuff
// </copyright>
// <summary>
//   Defines the ImageUtils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Octgn.Utils
{
    using System;
    using System.Reflection;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;

    /// <summary>
    /// The image utilities class.
    /// </summary>
    internal static class ImageUtils
    {
        /// <summary>
        /// The reflection bitmap image.
        /// </summary>
        private static readonly BitmapImage ReflectionBitmapImage;

        /// <summary>
        /// The get image from cache.
        /// </summary>
        private static readonly Func<Uri, BitmapImage> GetImageFromCache;

        /// <summary>
        /// Initializes static members of the <see cref="ImageUtils"/> class.
        /// </summary>
        static ImageUtils()
        {
            ReflectionBitmapImage = new BitmapImage();
            var methodInfo = typeof(BitmapImage).GetMethod(
                "CheckCache", BindingFlags.NonPublic | BindingFlags.Instance);
            GetImageFromCache =
                (Func<Uri, BitmapImage>)
                Delegate.CreateDelegate(typeof(Func<Uri, BitmapImage>), ReflectionBitmapImage, methodInfo);
        }

        /// <summary>
        /// The get card image.
        /// </summary>
        /// <param name="uri">
        /// The uri.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        public static void GetCardImage(Uri uri, Action<BitmapImage> action)
        {
            var bmp = GetImageFromCache(uri);
            if (bmp != null)
            {
                action(bmp);
                return;
            }

            // If the bitmap is not in cache, display the default face up picture and load the correct one async.
            action(Program.Game.CardFrontBitmap);
            Dispatcher.CurrentDispatcher.BeginInvoke(
                new Action(() => action(CreateFrozenBitmap(uri))), DispatcherPriority.ContextIdle);
        }

        /// <summary>
        /// The create frozen bitmap.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <returns>
        /// The <see cref="BitmapImage"/>.
        /// </returns>
        public static BitmapImage CreateFrozenBitmap(string source)
        {
            return CreateFrozenBitmap(new Uri(source));
        }

        /// <summary>
        /// The create frozen bitmap.
        /// </summary>
        /// <param name="uri">
        /// The uri.
        /// </param>
        /// <returns>
        /// The <see cref="BitmapImage"/>.
        /// </returns>
        public static BitmapImage CreateFrozenBitmap(Uri uri)
        {
            var imgsrc = new BitmapImage();
            imgsrc.BeginInit();
            imgsrc.CacheOption = BitmapCacheOption.OnLoad;

            // catch bad Uri's and load Front Bitmap "?"
            try
            {                                               
                imgsrc.UriSource = uri;
                imgsrc.EndInit();                
            }
            catch (Exception)
            {
                imgsrc = new BitmapImage();
                imgsrc.BeginInit();
                imgsrc.CacheOption = BitmapCacheOption.OnLoad;
                imgsrc.UriSource = Program.Game.CardFrontBitmap.UriSource;
                imgsrc.EndInit();              
            }

            imgsrc.Freeze();
            return imgsrc;
        }
    }
}