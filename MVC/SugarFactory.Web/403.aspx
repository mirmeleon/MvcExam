<% Response.StatusCode = 403; %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="403.aspx.cs" Inherits="SugarFactory.Web._403" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>It's magic!</title>
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon">
    <!-- Bootstrap Core CSS -->
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="~/Assets/vendor/metisMenu/metisMenu.min.css" rel="stylesheet">
    <!-- Custom CSS -->
    <link href="~/Assets/dist/css/sb-admin-2.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="~/Assets/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <!-- my css CSS -->
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body style="background-color: #63458f">
    <form id="form1" runat="server">
   </form>
    <div id="wrapper">
    
    <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0">
        <div class="navbar-header">
            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <a class="navbar-brand" href="/home/index">Sugar factory</a>
          
        </div>
        <!-- /.navbar-header -->
        <div class="navbar-collapse collapse">
            <ul class="nav navbar-nav">

                <li class="active"><a href="/home/index">Home</a></li>
                <li>
                    <a href="/home/about">About</a>
                </li>
                <li>
                    <a href="/home/contact">Contact</a>
                </li>
            </ul>

       </div>
    </nav>

   <div class="row bg">
    <div class="col-md-4 col-lg-offset-2">
        <h2 style="color: white">Ooops, <br />you just entered in the land of Unicorns!  <span class="text-warning">Forbidden</span> Forest number <span class="text-warning">403</span>.</h2>
        <h3 style="color: white">Let me take you back <a href="/home/index" style="color:mediumorchid">Home</a></h3>
      </div>
    <div class="col-md-4">
        <img src="/Assets/images/magic.jpg" width="350" />
    </div>

</div>
        </div>
</body>
</html>
