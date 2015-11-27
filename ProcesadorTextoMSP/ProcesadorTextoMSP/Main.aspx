<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="ProcesadorTextoMSP.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            Procesador de Texto MSP<br />
            <br />
            <br />
            Introduce el numero máximo de caracteres por linea:<br />
            <asp:TextBox ID="Columnas" runat="server" Width="29px" Height="19px"></asp:TextBox>
            <br />
            <br />
            Introduce aquí tu texto:<br />
            <asp:TextBox ID="Texto" runat="server" Height="22px" Width="742px"></asp:TextBox>
            <asp:Button ID="ProcButton" runat="server" OnClick="ProcButton_Click" Text="Procesar" Width="144px" ForeColor="Blue" />
            <br />
            <br />
            <pre>
                 <asp:Label ID="TextoProcesadoLabel" runat="server"></asp:Label>
            </pre>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />

        </div>
    </form>
</body>
</html>
