<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getfile.aspx.cs" Inherits="getfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <div>
        <% if (!success)
            {
                Response.Write(errorMessage);
            }
        %>
    </div>
</body>
</html>
