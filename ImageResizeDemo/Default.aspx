<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ImageResizeDemo.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Image manipulation demo</h1>
            <asp:FileUpload ID="imageUploader" runat="server" accept="image/jpeg" /><asp:RequiredFieldValidator ErrorMessage="Image is required" ControlToValidate="imageUploader" runat="server" />

            <fieldset><legend><asp:RadioButton ID="rbCrop" runat="server" GroupName="image" /><asp:Label ID="lblCrop" runat="server" Text="Crop" AssociatedControlID="rbCrop"></asp:Label></legend>
            
                <asp:Label ID="lblLeft" runat="server" Text="Left" AssociatedControlID="txtLeftCrop"></asp:Label>
                <asp:TextBox ID="txtLeftCrop" runat="server" TextMode="Number"></asp:TextBox>
                <asp:Label ID="lblRight" runat="server" Text="Right" AssociatedControlID="txtRightCrop"></asp:Label>
                <asp:TextBox ID="txtRightCrop" runat="server" TextMode="Number"></asp:TextBox>
                <asp:Label ID="lblTop" runat="server" Text="Top" AssociatedControlID="txtTopCrop"></asp:Label>
                <asp:TextBox ID="txtTopCrop" runat="server" TextMode="Number"></asp:TextBox>
                <asp:Label ID="lblBottom" runat="server" Text="Bottom" AssociatedControlID="txtBottomCrop"></asp:Label>
                <asp:TextBox ID="txtBottomCrop" runat="server" TextMode="Number"></asp:TextBox>
                </fieldset>

            <fieldset><legend><asp:RadioButton ID="rbResize" runat="server" GroupName="image" />
                <asp:Label ID="lblResize" runat="server" Text="Resize"></asp:Label></legend>
                <asp:Label ID="lblMaintainAspect" runat="server" Text="Maintain Aspect Ratio"></asp:Label>
                <asp:CheckBox ID="cbAspect" runat="server" />
                 <asp:Label ID="lblHeight" runat="server" Text="(Max) Height" AssociatedControlID="txtHeight"></asp:Label>
                <asp:TextBox ID="txtHeight" runat="server" TextMode="Number"></asp:TextBox>
                <asp:Label ID="lblWidth" runat="server" Text="(Max) Width" AssociatedControlID="txtWidth"></asp:Label>
                <asp:TextBox ID="txtWidth" runat="server" TextMode="Number"></asp:TextBox>
            </fieldset>
            <fieldset><legend><asp:RadioButton ID="rbsquare" runat="server" GroupName="image" /><asp:Label ID="lblSquareCrop" runat="server" Text="Square Crop"></asp:Label></legend>
                <asp:Label ID="lblSquareSize" runat="server" Text="Size (optional)"></asp:Label>
                 <asp:TextBox ID="txtSquareSize" runat="server" TextMode="Number"></asp:TextBox>
            </fieldset>


            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click"  />
            <h2>Output</h2>
            <asp:Image ID="imgOutput" runat="server" />
            <h2>Input</h2>
            <asp:Image ID="imgInput" runat="server" />
            

        </div>
    </form>
</body>
</html>
