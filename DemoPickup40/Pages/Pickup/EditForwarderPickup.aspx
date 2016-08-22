<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="EditForwarderPickup.aspx.cs" Inherits="DemoPickup40.Pages.Pickup.EditForwarderPickup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="EditForwarderPickup.css" rel="stylesheet" />

    <script>
        function CloseWindow() {
            window.close();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Forwarder Pickup</h1>
    <div class="block">

        <div class="row header">
            <span class="col1">Name</span>
            <span class="col2">Value</span>
        </div>

        <div class="row">
            <asp:Label ID="Label1" class="col1" runat="server" Text="Name"></asp:Label>
            <asp:TextBox ID="XuName" class="col2" runat="server"></asp:TextBox><br />
        </div>

        <div class="row">
            <asp:Label ID="Label2" class="col1" runat="server" Text="Street1"></asp:Label>
            <asp:TextBox ID="XuStreet1" class="col2" runat="server"></asp:TextBox><br />
        </div>

        <div class="row">
            <asp:Label ID="Label3" class="col1" runat="server" Text="Street2"></asp:Label>
            <asp:TextBox ID="XuStreet2" class="col2" runat="server"></asp:TextBox><br />
        </div>

        <div class="row state">
            <asp:Label ID="Label4" class="col1" runat="server" Text="State"></asp:Label>
            <asp:TextBox ID="XuState" class="col2" runat="server"></asp:TextBox><br />
        </div>

        <div class="row">
            <asp:Label ID="Label5" class="col1" runat="server" Text="Country"></asp:Label>
            <asp:TextBox ID="XuCountryCode" class="col2" runat="server"></asp:TextBox><br />
        </div>

        <div class="row">
            <asp:Label ID="Label6" class="col1" runat="server" Text="Zip"></asp:Label>
            <asp:TextBox ID="XuZip" class="col2" runat="server"></asp:TextBox><br />
        </div>

        <div class="row">
            <asp:Label ID="Label7" class="col1" runat="server" Text="Attention"></asp:Label>
            <asp:TextBox ID="XuAttention" class="col2" runat="server"></asp:TextBox><br />
        </div>

        <div class="row">
            <asp:Label ID="Label8" class="col1" runat="server" Text="Email"></asp:Label>
            <asp:TextBox ID="XuEmail" class="col2" runat="server"></asp:TextBox><br />
        </div>

        <div class="row">
            <asp:Label ID="Label9" class="col1" runat="server" Text="Phone"></asp:Label>
            <asp:TextBox ID="XuPhone" class="col2" runat="server"></asp:TextBox><br />
        </div>

        <div class="row">
            <asp:Label ID="Label10" class="col1" runat="server" Text="Note"></asp:Label>
            <asp:TextBox ID="XuNote" class="col2" runat="server"></asp:TextBox><br />
        </div>

        <div class="row">
            <asp:Label ID="Label11" class="col1" runat="server" Text="PickupDate"></asp:Label>
            <asp:TextBox ID="XuPickupDate" class="col2" runat="server"></asp:TextBox><br />
        </div>

        <div class="row">
            <asp:Label ID="Label12" class="col1" runat="server" Text="ReadyOpen"></asp:Label>
            <asp:TextBox ID="XuReadyOpen" class="col2" runat="server"></asp:TextBox><br />
        </div>

        <div class="row">
            <asp:Label ID="Label13" class="col1" runat="server" Text="ReadyClose"></asp:Label>
            <asp:TextBox ID="XuReadyClose" class="col2" runat="server"></asp:TextBox><br />
        </div>

        <div class="row">
            <asp:Label ID="Label14" class="col1" runat="server" Text="Status"></asp:Label>
            <asp:TextBox ID="XuPickupStatus" class="col2" runat="server"></asp:TextBox><br />
            
            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
        </div>


    </div>

    <asp:Button ID="XuSubmit" runat="server" Text="Save" OnClick="XuSubmit_Click" />

    <asp:Label ID="XuError" runat="server" CssClass="error hidden" Text="no error"></asp:Label>

</asp:Content>
