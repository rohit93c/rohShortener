<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shortn.aspx.cs" Inherits="shortn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rohit | URL Shortener</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/normalize/5.0.0/normalize.min.css" />--%>
    <link rel="stylesheet" href="css/normalize.min.css" type="text/css" />
    <link rel="icon" href="images/favLogo.gif"/>
    <link rel="stylesheet" href="css/style.css" type="text/css"/>
    <style>  
        .button {
            border: none;
            color: white;
            padding: 10px 26px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
        }

        #txtLongUrl {
            display: inline-block;
            vertical-align: middle;
            margin: 10px 0;
            padding: 8px;
        }

        .custometextbox {
            display: inline-block;
            vertical-align: middle;
            margin: 10px 0;
            padding: 8px;
        }

        #btnShorten1 {
            display: inline-block;
            vertical-align: middle;
            margin: 10px 0;
            background-color: #008CBA;
        } 

        #btnShorten2 {
            background-color: #008CBA; 
            display: inline-block;
            vertical-align: middle;
            margin: 10px 0;
        }

        #lnkShowCustom {
             background:none!important;
             color:inherit;
             border:none; 
             padding:0!important;
             font: inherit; 
             border-bottom:1px solid #444; 
             cursor: pointer;
        } 
        .copyButton {border-radius: 3px;
    border: 0px solid #ccc;
    background: #fff;
    font-size: 12px !important;
    font-weight: bold;
    padding: 5px 10px;
    color: #008cba;
    position: relative;
    top: -4px;}
    </style>
    <script src="js/jquery-3.3.1.js"></script>
    <%--<script src="//cdnjs.cloudflare.com/ajax/libs/clipboard.js/1.4.0/clipboard.min.js"></script>--%>
    <script src="js/clipboard.js"></script>

    <script type="text/javascript">

        $(document).ready(function () { 
            $('#lnkShowCustom').click(function () {
                $("#dvCustom").slideToggle();
                $("#btnShorten1").slideToggle();
            }); 
        }); 
    </script>
    <script> 
        $(document).ready(function () {
            var clipboard = new Clipboard('#btnCopy'); 
        });
    </script>

</head>
<body>
    <div class="container">
        <form id="form1" runat="server" > 
                    <div id="dvMain" style="margin:0px auto;">
                        <h1>Enter your long link here:</h1>
                        <asp:TextBox ID="txtLongUrl" CssClass="custometextbox" runat="server" ValidationGroup="main" BorderColor="Gray" BorderStyle="Solid" Width="379px" placeholder="Your original URL here"></asp:TextBox>
                        <asp:Button ID="btnShorten1" runat="server" Text="Shorten" OnClick="btnShorten2_Click" CssClass="button" ValidationGroup="main" />
                        <asp:RequiredFieldValidator ID="rfvTxtLongUrl" ControlToValidate="txtLongUrl" runat="server" ErrorMessage="Please insert a url!" SetFocusOnError="true" ForeColor="Red" ValidationGroup="main"></asp:RequiredFieldValidator>
                    </div>
                    <br />
                    <input type="button" id="lnkShowCustom" value="Custom short URL" runat="server" text="Custom short URL" style="color: #523210; font-weight: bold" />
                    <br />
                    <div id="dvCustom" style="display: none;">
                        <%--Your custom short url : --%> 
            <label style="font-size: 20px; font-weight: bold;">http://roh.it/</label>
                        <asp:TextBox ID="txtShortCustom" CssClass="custometextbox" runat="server" MaxLength="6" ValidationGroup="custom" BorderColor="Gray" BorderStyle="Solid" placeholder="cutom url" style="width:262px; "></asp:TextBox>
                        <asp:Button ID="btnShorten2" runat="server" Text="Shorten" OnClick="btnShorten3_Click" CssClass="button" ValidationGroup="custom"  />
                        <br />
                        <label style="color: black; text-align: center">(Enter only alphabets and numbers. 6 characters maximum
                            <br />
                            e.g: if you enter 'rohit' your url will be http://roh.it/rohit)</label> 
                    </div> 
            <br /><br />
                    <label id="lblShortenedUrl" runat="server" style="font-size: 20px; font-weight: bold;"></label>
                    <button runat="server" class="copyButton" id="btnCopy" data-clipboard-target="#lblShortenedUrl" onclick="return false">Copy</button>
        </form>
        <div class="bird-container bird-container--one">
            <div class="bird bird--one"></div>
        </div>

        <div class="bird-container bird-container--two">
            <div class="bird bird--two"></div>
        </div>

        <div class="bird-container bird-container--three">
            <div class="bird bird--three"></div>
        </div>

        <div class="bird-container bird-container--four">
            <div class="bird bird--four"></div>
        </div>
    </div>


    <%--    <form id="form1" runat="server" style="margin-left:25%;">
        <br /><br />
        <div id="dvMain"> 
            <h1>Enter your long link here:</h1>
                <asp:TextBox id="txtLongUrl" runat="server" ValidationGroup="main" BorderColor="Gray" BorderStyle="Solid" Width="340px" placeholder="Your original URL here" ></asp:TextBox>
                <asp:Button ID="btnShorten1" runat="server" Text="Shorten" OnClick="btnShorten2_Click" CssClass="button" ValidationGroup="main"/>
                <asp:RequiredFieldValidator ID="rfvTxtLongUrl" ControlToValidate="txtLongUrl" runat="server" ErrorMessage="Please insert a url!" SetFocusOnError="true" ForeColor="Red" ValidationGroup="main"></asp:RequiredFieldValidator>
                <label id="lblShortenedUrl" runat="server"></label> 
        </div>
        <br />
        <br />
        <input type="button" id="lnkShowCustom" value="Custom short URL" runat="server" text="Custom short URL" style="color:dodgerblue"/> 
        <br />
        <br />

        <div id="dvCustom" style="display:none;">
            Your custom short url :   <label style="font-size:20px; font-weight:bold;">http://roh.it/</label>  <asp:TextBox ID="txtShortCustom" runat="server" MaxLength="6" ValidationGroup="custom" BorderColor="Gray" BorderStyle="Solid" Width="80px" placeholder="cutom url"></asp:TextBox>
            <asp:Button ID="btnShorten2" runat="server" Text="Shorten" OnClick="btnShorten3_Click" CssClass="button" ValidationGroup="custom"/>
            <br /><label style="color:brown; text-align:center">(Enter only alphabets and numbers. 6 characters maximum </BR>e.g: if you enter 'rohit' your url will be http://roh.it/rohit)</label>
            <label id="lblCustomShortenedUrl" runat="server"></label>
        </div>
    </form> --%>
</body>
</html>
