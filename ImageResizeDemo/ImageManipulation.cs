using System;
using System.Drawing;
using System.Drawing.Drawing2D;

/// <summary>
/// Summary description for ImageManipulation
/// </summary>
public class ImageResizer
{
    public ImageResizer(Image i)
    {
        this.i = i;
    }

    private Image i;


    public Image Resized(int width, int height, bool preserveAspectRatio)
    {
        if (preserveAspectRatio)
        {
            return ResizeImage(width, height);
        }
        else
        {
            return SimpleResize(width, height);
        }
    }


    private Image SimpleResize(int width, int height)
    {
        return SimpleResize(width, height, i);
    }

    /// <summary>
    /// Resizes an image to a new size, distorting it if the aspect ratio changes
    /// </summary>
    /// <param name="i">The image</param>
    /// <param name="width">Desired Width</param>
    /// <param name="height">Desired Height</param>
    /// <returns>The resized image</returns>
    private Image SimpleResize(int width, int height, Image i)
    {
        //create a bitmap image with the required width and height
        Bitmap resized = new Bitmap(width, height);

        //create an instance of the graphics class which we will use to do the resizing work
        Graphics g = Graphics.FromImage(resized);

        ////settings for image quality (better qulity = slower process)
        g.CompositingQuality = CompositingQuality.HighQuality;
        g.SmoothingMode = SmoothingMode.HighQuality;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

        //create a rectangle where the upper left coordinates are 0 and 0, and its width and hight match the required size
        Rectangle NewSizeRectangle = new Rectangle(0, 0, width, height);

        //graw the original image i, into the new rectangle, starting at x=0 and y=0, drawing the full height and width of the initial image, using pixels as the measurement
        g.DrawImage(i, NewSizeRectangle, 0, 0, i.Width, i.Height, GraphicsUnit.Pixel);

        //dispose of our graphics instance
        g.Dispose();

        //return the resized image
        return resized;
    }

    /// <summary>
    /// Resizes an image, preserving aspect ratio within the bounds of a max width and height
    /// </summary>
    /// <param name="MaxWidth">maximum width of resized image</param>
    /// <param name="MaxHeight">maximum height of resized image</param>
    /// <returns>The resized image</returns>
    private  Image ResizeImage(int MaxWidth, int MaxHeight)
    {
        //do some maths to determine the best way of resizing the image
        //e.g. if the max width and height are 100, a tall image will be 100 on the height, a wide one will be 100 on the width
        decimal imageRatio = (decimal)i.Width / (decimal)i.Height;
        decimal optimalRatio = (decimal)MaxWidth / (decimal)MaxHeight;
        int w, h;

        if (imageRatio < optimalRatio)
        {
            //resize on the height
            h = MaxHeight;
            w = i.Width * h / i.Height;
        }

        else
        {
            //resize on the width
            w = MaxWidth;
            h = i.Height * w / i.Width;
        }

        //as above

        return SimpleResize(w, h);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="top"></param>
    /// <param name="bottom"></param>
    /// <returns></returns>
    public Image Cropped(int left, int right, int top, int bottom)
    {
        return Cropped(new Point(left, top), new Point(i.Width - right, i.Height - bottom));
    }

    private Image Cropped(Point TopLeft, Point BottomRight)
    {
        Bitmap btmCropped = new Bitmap((BottomRight.X - TopLeft.X), BottomRight.Y - TopLeft.Y);
        Graphics grpOriginal = Graphics.FromImage(btmCropped);

        grpOriginal.DrawImage(i, new Rectangle(0, 0, btmCropped.Width, btmCropped.Height), TopLeft.X, TopLeft.Y, btmCropped.Width, btmCropped.Height, GraphicsUnit.Pixel);
        grpOriginal.Dispose();

        return btmCropped;

    }

    /// <summary>
    /// Returns the image cropped to a square, maintaining the length of the shorted side
    /// </summary>
    /// <returns>An Image</returns>
    public Image Square()
    {
        return SquareCropImage();
    }

    /// <summary>
    /// Returns the image cropped to a square, and resizes to a given dimension
    /// </summary>
    /// <param name="sideLength"></param>
    /// <returns>An image</returns>
    public Image Square(int sideLength)
    {
        return SimpleResize(sideLength, sideLength, SquareCropImage());
    }

    private Image SquareCropImage()
    {
        //determine whether to crop top or sides
        if (i.Width > i.Height)
        {
            //crop sides
            //calculate difference
            int difference = i.Width - i.Height;

            int leftCrop = difference / 2;
            //if its not an even number crop an extra pixel from the right
            int rightCrop = (difference / 2) + (difference % 2);

            return (Cropped(new Point(leftCrop, 0), new Point(leftCrop + i.Height, i.Height)));
        }
        else if (i.Width < i.Height)
        {
            //crop bottom
            int cropHeight = i.Height - i.Width;

            return (Cropped(new Point(0, 0), new Point(i.Width, i.Height - cropHeight)));
        }
        else
        {
            return i;
        }
    }
}