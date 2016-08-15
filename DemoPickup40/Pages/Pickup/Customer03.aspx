<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Customer03.aspx.cs" Inherits="DemoPickup40.Pages.Pickup.Customer03" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Customer.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/css/Site.css" rel="stylesheet" />
    <link href="../../assets/css/stylesheet-2016-02-23.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

   <asp:Table 
       ID="Xu001"
       runat="server"
       GridLines="both"
       >
       
       <asp:TableHeaderRow>
           <asp:TableHeaderCell>
               H1
           </asp:TableHeaderCell>
           <asp:TableHeaderCell>
               H2
           </asp:TableHeaderCell>
           <asp:TableHeaderCell>
               H3
           </asp:TableHeaderCell>
           <asp:TableHeaderCell>
               H4
           </asp:TableHeaderCell>
           <asp:TableHeaderCell>
               H5
           </asp:TableHeaderCell>
       </asp:TableHeaderRow>
       
       
       <asp:TableRow>
           <asp:TableCell>
               C1
           </asp:TableCell>
           <asp:TableCell>
               C2
           </asp:TableCell>
           <asp:TableCell>
               C3
           </asp:TableCell>
           <asp:TableCell>
               C4
           </asp:TableCell>
           <asp:TableCell>
               C5
           </asp:TableCell>
       </asp:TableRow>

   </asp:Table>

</asp:Content>
