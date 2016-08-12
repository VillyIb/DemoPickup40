<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="DemoPickup40.Pages.Pickup.Customer" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    Alfa<link href="Customer.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="XuUpdatePanel1" runat="server">

        <ContentTemplate>


            <%--First level GridView--%>
            <asp:GridView
                ID="Xu001"
                runat="server"
                AutoGenerateColumns="False"
                ShowHeaderWhenEmpty="True"
                OnDataBinding="Xu001_DataBinding">

                <HeaderStyle CssClass="HeaderStyleLevel1" />
                <RowStyle CssClass="RowStyleLevel1" />
                <AlternatingRowStyle CssClass="RowStyleLevel1Alt" />

                <Columns>
                    <asp:TemplateField>

                        <HeaderTemplate>
                            <asp:Label ID="Xu002" runat="server" Text="Header" />
                        </HeaderTemplate>
                                               
                        <ItemTemplate>

                            <div class="PickupAndShipmentList">
                                <div class="Pickup">
                                    <%-- First level row - line 1 --%>
                                    <asp:Label ID="Xu003" runat="server" Text='<%# Bind("Name") %>' />
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("RowNumber") %>' />
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Name") %>' />
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("RowNumber") %>' />
                                </div>
                        </ItemTemplate>

                        <AlternatingItemTemplate>
                            <%-- First level row - line 2 --%>
                            <%--Second level GridView--%>
                            <div class="ShipmentRow">
                                <asp:GridView
                                    ID="Xu100"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataSource='<%# Bind("Shipmentlist") %>'>

                                    <HeaderStyle CssClass="HeaderStyleLevel2" />
                                    <RowStyle CssClass="RowStyleLevel2" />

                                    <Columns>

                                        <asp:TemplateField>

                                            <HeaderTemplate>
                                                <asp:Label ID="Xu102" runat="server" Text="HEADER" />
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="Xu104" runat="server" Text='<%# Bind("Name") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField>

                                            <HeaderTemplate>
                                                <asp:Label ID="Xu102" runat="server" Text="ROWNUMBER" />
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="Xu104" runat="server" Text='<%# Bind("RowNumber") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>
                            </div>
                            </div>
                        </AlternatingItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField>

                        <HeaderTemplate>
                            <asp:Label ID="Xu002" runat="server" Text="HeaderC2" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="Xu002" runat="server" Text="RowC2" />
                        </ItemTemplate>

                    </asp:TemplateField>


                </Columns>

            </asp:GridView>




        </ContentTemplate>

    </asp:UpdatePanel>



</asp:Content>
