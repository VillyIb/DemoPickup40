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
    <div class="css-table">

        <div class="css-tr">
            <span class="css-th col1">Name</span>
            <span class="css-th col2">Value</span>
            <div class="css-th col3" >&nbsp;</div>
        </div>

        <div class="css-tr">
            <asp:Label ID="Label1" class="css-td col1" runat="server" Text="Name"></asp:Label>
            <span class="css-td col2"><asp:TextBox ID="XuName"  runat="server"></asp:TextBox></span>
            <div class="css-td col3" >&nbsp;</div>
        </div>

        <div class="css-tr">
            <asp:Label ID="Label2" class="css-td col1" runat="server" Text="Street1"></asp:Label>
            <span class="css-td col2"><asp:TextBox ID="XuStreet1" runat="server"></asp:TextBox></span>
            <div class="css-td col3" >&nbsp;</div>
        </div>

        <div class="css-tr">
            <asp:Label ID="Label3" class="css-td col1" runat="server" Text="Street2"></asp:Label>
            <span class="css-td col2"><asp:TextBox ID="XuStreet2" runat="server"></asp:TextBox></span>
            <div class="css-td col3" >&nbsp;</div>
        </div>

        <div class="css-tr">
            <asp:Label ID="Label4" class="css-td col1" runat="server" Text="State"></asp:Label>
            <span class="css-td col2"><asp:TextBox ID="XuState" runat="server"></asp:TextBox></span>
            <div class="css-td col3" >&nbsp;</div>
        </div>

        <div class="css-tr">
            <asp:Label ID="Label5" class="css-td col1" runat="server" Text="Country"></asp:Label>
            <span class="css-td col2"><asp:TextBox ID="XuCountryCode" runat="server"></asp:TextBox></span>
            <div class="css-td col3" >&nbsp;</div>
        </div>

        <div class="css-tr">
            <asp:Label ID="Label6" class="css-td col1" runat="server" Text="Zip"></asp:Label>
            <span class="css-td col2"><asp:TextBox ID="XuZip" runat="server"></asp:TextBox></span>
            <div class="css-td col3" >&nbsp;</div>
        </div>

        <div class="css-tr">
            <asp:Label ID="Label7" class="css-td col1" runat="server" Text="Attention"></asp:Label>
            <span class="css-td col2"><asp:TextBox ID="XuAttention" runat="server"></asp:TextBox></span>
            <div class="css-td col3" >&nbsp;</div>
        </div>

        <div class="css-tr">
            <asp:Label ID="Label8" class="css-td col1" runat="server" Text="Email"></asp:Label>
            <span class="css-td col2"><asp:TextBox ID="XuEmail" runat="server"></asp:TextBox></span>
            <div class="css-td col3" >&nbsp;</div>
        </div>

        <div class="css-tr">
            <asp:Label ID="Label9" class="css-td col1" runat="server" Text="Phone"></asp:Label>
            <span class="css-td col2"><asp:TextBox ID="XuPhone" runat="server"></asp:TextBox></span>
            <div class="css-td col3" >&nbsp;</div>
        </div>

        <div class="css-tr">
            <asp:Label ID="Label10" class="css-td col1" runat="server" Text="Note"></asp:Label>
            <span class="css-td col2"><asp:TextBox ID="XuNote" runat="server"></asp:TextBox></span>
            <div class="css-td col3" >&nbsp;</div>
        </div>

        <div class="css-tr">
            <asp:Label ID="Label11" class="css-td col1" runat="server" Text="PickupDate"></asp:Label>
            <span class="css-td col2"><asp:TextBox ID="XuPickupDate" runat="server"></asp:TextBox></span>
            <div class="css-td col3" >&nbsp;</div>
        </div>

        <div class="css-tr">
            <asp:Label ID="Label12" class="css-td col1" runat="server" Text="ReadyOpen"></asp:Label>
            <span class="css-td col2"><asp:TextBox ID="XuReadyOpen" runat="server"></asp:TextBox></span>
            <span class="css-td col3"><asp:Button ID="XuReadyOpenCalculated" runat="server" Text="12:00" OnClick="XuReadyOpenCalculated_Click"/></span>
        </div>

        <div class="css-tr">
            <asp:Label ID="Label13" class="css-td col1" runat="server" Text="ReadyClose"></asp:Label>
            <span class="css-td col2"><asp:TextBox ID="XuReadyClose" runat="server"></asp:TextBox></span>
            <span class="css-td col3"><asp:Button ID="XuReadyCloseCalculated" runat="server" Text="15:00" OnClick="XuReadyCloseCalculated_Click"/></span>
        </div>

        <div class="css-tr">
            <asp:Label ID="Label14" class="css-td col1" runat="server" Text="Status"></asp:Label>
            <span class="css-td col2"><asp:DropDownList ID="XuPickupStatus" runat="server"></asp:DropDownList></span>
            <div class="css-td col3" >&nbsp;</div>
        </div>


    </div>

    <asp:Button ID="XuSubmit" runat="server" Text="Save" OnClick="XuSubmit_Click" />

    <asp:Label ID="XuError" runat="server" CssClass="error hidden" Text="no error"></asp:Label>

</asp:Content>
