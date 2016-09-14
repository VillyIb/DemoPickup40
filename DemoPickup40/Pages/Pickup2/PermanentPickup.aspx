<%@ Page
    Title=""
    Language="C#"
    MasterPageFile="~/Master.Master"
    AutoEventWireup="True"
    CodeBehind="PermanentPickup.aspx.cs"
    Inherits="DemoPickup40.Pages.Pickup2.PermanentPickup" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../assets/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/css/Site.css" rel="stylesheet" />
    <link href="../../assets/css/stylesheet-2016-02-23.css" rel="stylesheet" />
    <link href="PermanentPickup.css" rel="stylesheet" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="XuUpdatePanel1" runat="server" UpdateMode="Always">

        <ContentTemplate>

            <%--Forwarder Pickup Grid View--%>
            <asp:GridView
                ID="XuPermanentPickup"
                runat="server"
                AutoGenerateColumns="False"
                ShowHeaderWhenEmpty="True"
                CssClass="XuForwarderPickup"
                OnRowCommand="XuPermanentPickup_RowCommand" 
                OnRowEditing="XuPermanentPickup_RowEditing"
               
                >

                <HeaderStyle CssClass="XuForwarderPickupHeader" />
                <RowStyle CssClass="XuForwarderPickupRow" />

                <Columns>

                    <%------------ Account ------------%>

                    <%--Column Customer --%>
                    <asp:TemplateField HeaderText="XuCustomer">

                        <HeaderTemplate>
                            <asp:Label ID="XuCustomerHeader" runat="server" Text="Customer" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuCustomerItem" runat="server"
                                ToolTip='<%# Bind("CustomerNameLong") %>'
                                Text='<%#  Bind("CustomerNameLong") %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuCustomer" />
                        <ItemStyle CssClass="XuCustomer" />

                    </asp:TemplateField>

                    <%-- Column Account --%>
                    <asp:TemplateField HeaderText="XuAccount">

                        <HeaderTemplate>
                            <asp:Label ID="XuAccuntHeader" runat="server" Text="Account" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuAccuntItem" runat="server"
                                ToolTip='<%# Bind("AccountNameLong") %>'
                                Text='<%#  Bind("AccountNameLong") %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuAccunt" />
                        <ItemStyle CssClass="XuAccunt" />

                    </asp:TemplateField>


                    <%--Column Enabled --%>
                    <asp:TemplateField HeaderText="XuEnabledMaster">

                        <EditItemTemplate>
                            xx
                        </EditItemTemplate>

                        <HeaderTemplate>
                            <asp:Label ID="XuEnabledMasterHeader" runat="server" Text="Master Eanbled" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:CheckBox ID="XuEnabledMasterItem" runat="server" Enabled="false" Checked='<%# Bind("EnabledMaster") %>' />

                        </ItemTemplate>

                        <HeaderStyle CssClass="XuEnabledMaster" />
                        <ItemStyle CssClass="XuEnabledMaster" />

                    </asp:TemplateField>


                    <%--Column Enabled weekday --%>
                    <asp:TemplateField HeaderText="XuEnabledWeekday">

                        <HeaderTemplate>
                            <asp:Label ID="XuEnabledWeekdayHeader" runat="server" Text="Monday,...,Sunday" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuEnabledWeekdayItem" runat="server" Text='<%#  Bind("EnabledWeek") %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuEnabledWeekday" />
                        <ItemStyle CssClass="XuEnabledWeekday" />

                    </asp:TemplateField>


                    <%--Column Pickup Operator--%>
                    <asp:TemplateField HeaderText="XuPickpuOperator">

                        <HeaderTemplate>
                            <asp:Label ID="XuPickpuOperatorHeader" runat="server" Text="PickupOperator" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuPickpuOperatorItem" runat="server" Text='<%#  Bind("PickupOperator") %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuPickpuOperator" />
                        <ItemStyle CssClass="XuPickpuOperator" />

                    </asp:TemplateField>


                    <%--Column TimeReady --%>
                    <asp:TemplateField HeaderText="XuTimeReady">

                        <HeaderTemplate>
                            <asp:Label ID="XuTimeReadyHeader" runat="server" Text="TimeReady" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuTimeReadyItem" runat="server" Text='<%# Bind("TimeReady") %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuTimeReady" />
                        <ItemStyle CssClass="XuTimeReady" />

                    </asp:TemplateField>


                    <%--Column TimeClose --%>
                    <asp:TemplateField HeaderText="XuTimeClose">

                        <HeaderTemplate>
                            <asp:Label ID="XuTimeCloseHeader" runat="server" Text="TimeClose" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuTimeCloseItem" runat="server" Text='<%# Bind("TimeClose") %> ' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuTimeClose" />
                        <ItemStyle CssClass="XuTimeClose" />


                    </asp:TemplateField>


                    <%--Column Text --%>
                    <asp:TemplateField HeaderText="XuText">

                        <HeaderTemplate>
                            <asp:Label ID="XuTextHeader" runat="server" Text="Text" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuTextItem" runat="server" Text='<%# Bind("Text") %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuText" />
                        <ItemStyle CssClass="XuText" />

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="XuYYYY" >

                        <HeaderTemplate>
                            <asp:Label ID="XuYYYHeader" runat="server" Text="Edit" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:LinkButton
                                ID="XuYYYYItem"
                                runat="server"
                                CausesValidation="false"
                                CommandName="XuYYYYcmd"
                                CommandArgument="XuYYYYArg"
                                >
                                XXXXXXXXXX
                            </asp:LinkButton>
                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>

            </asp:GridView>


        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
