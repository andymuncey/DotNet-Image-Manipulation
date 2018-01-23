using System;
using System.Drawing.Imaging;
using System.IO;

namespace ImageResizeDemo
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {



                System.Drawing.Image img = System.Drawing.Image.FromStream(imageUploader.PostedFile.InputStream);

                var resizer = new ImageResizer(img);

                System.Drawing.Image output;

                if (rbCrop.Checked)
                {
                    int left = int.Parse(txtLeftCrop.Text);
                    int right = int.Parse(txtRightCrop.Text);
                    int bottom = int.Parse(txtBottomCrop.Text);
                    int top = int.Parse(txtTopCrop.Text);

                    output = resizer.Cropped(left, right, top, bottom);
                }
                else

                if (rbResize.Checked)
                {
                    int width = int.Parse(txtWidth.Text);
                    int height = int.Parse(txtHeight.Text);
                    output = resizer.Resized(width, height, cbAspect.Checked);
                }
                else

                {
                    int newSize;
                    if (int.TryParse(txtSquareSize.Text, out newSize))
                    {
                        output = resizer.Square(newSize);
                    }
                    else
                    {
                        output = resizer.Square();
                    }
                }

                string base64OutputImage = Base64(output);
                imgOutput.Attributes.Add("src", "data:image/png;base64, " + base64OutputImage);

                string base64InputImage = Base64(img);
                imgInput.Attributes.Add("src", "data:image/png;base64, " + base64InputImage);

            }
        }


        private string Base64(System.Drawing.Image image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Jpeg);
                byte[] imageBytes = stream.ToArray();

                return Convert.ToBase64String(imageBytes);
            }
        }

    }
}

