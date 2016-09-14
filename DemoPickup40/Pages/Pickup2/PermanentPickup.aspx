<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PermanentPickup.aspx.cs" Inherits="DemoPickup40.Pages.Pickup2.PermanentPickup" %>


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
                ID="XuForwarderPickup"
                runat="server"
                AutoGenerateColumns="False"
                ShowHeaderWhenEmpty="True"
                CssClass="XuForwarderPickup"
                OnRowCommand="XuGridForwarderPicup_RowCommand">
        
                <HeaderStyle CssClass="XuForwarderPickupHeader" />
                <RowStyle CssClass="XuForwarderPickupRow" />

                <Columns>

                    <%------------ Account ------------%>

                    <%--Column Customer --%>
                    <asp:TemplateField HeaderText="XuCustomer">

                        <HeaderTemplate>
                            <asp:Label ID="XuCustomerHeader" runat="server" Text="XuCustomerHeader" meta:resourcekey="XuCustomerHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuCustomerItem" runat="server"
                                ToolTip='<%# Bind("Address.CompareInfo") %>'
                                Text='<%#  Bind("Address.Address1") %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuCustomer" />
                        <ItemStyle CssClass="XuCustomer" />

                    </asp:TemplateField>

                    <%-- Column Account --%>
                    <asp:TemplateField HeaderText="XuAccount">

                        <HeaderTemplate>
                            <asp:Label ID="XuAccuntHeader" runat="server" Text="XuAccuntHeader" meta:resourcekey="XuAccuntHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuAccuntItem" runat="server"
                                Text='<%# Bind("Address.Address2") %>' />
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
                            <asp:Label ID="XuFwPickupHeader" runat="server" Text="XuFwPickupHeader" meta:resourcekey="XuFwPickupHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <%--<asp:Label ID="XuFwPickupItem" runat="server"  Text='<%# Bind("PickupDate") %>'></asp:Label>--%>
                            <asp:Label ID="Label1" runat="server" Text='<%# XmGetPickupText( (string)GetLocalResourceObject("XuFwPickupItem.Format"), Eval("PickupDate"), Eval("TimeReady"), Eval("TimeClose")) %>'></asp:Label>
                            <asp:LinkButton
                                ID="XuFwdPickupX1"
                                runat="server"
                                CausesValidation="false"
                                CommandName="XcCmd03"
                                CommandArgument='<%# Eval("Id") %>'>
                                <span
                                    class="glyphicon glyphicon-refresh"
                                    title="Recalculate smallest window for Open/Close"
                                    />
                            </asp:LinkButton>
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuFwPickup" />
                        <ItemStyle CssClass="XuFwPickup" />

                    </asp:TemplateField>


                    <%--Column Enabled weekday --%>
                    <asp:TemplateField HeaderText="XuEnabledWeekday">

                        <HeaderTemplate>
                            <asp:Label ID="XuEnabledWeekdayHeader" runat="server" Text="XuEnabledWeekdayHeader" meta:resourcekey="XuEnabledWeekdayHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuEnabledWeekdayItem" runat="server" Text='<%# Bind("CarrierNameList") %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuEnabledWeekday" />
                        <ItemStyle CssClass="XuEnabledWeekday" />

                    </asp:TemplateField>


                    <%--Column Pickup Operator--%>
                    <asp:TemplateField HeaderText="XuPickpuOperator">

                        <HeaderTemplate>
                            <asp:Label ID="XuPickpuOperatorHeader" runat="server" Text="XuPickpuOperatorHeader" meta:resourcekey="XuPickpuOperatorHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuPickpuOperatorItem" runat="server" Text='<%# string.Format("{0} ({1})", Eval("CountCustomers") , Eval("CountShipments") ) %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuPickpuOperator" />
                        <ItemStyle CssClass="XuPickpuOperator" />

                    </asp:TemplateField>


                    <%--Column TimeReady --%>
                    <asp:TemplateField HeaderText="XuTimeReady">

                        <HeaderTemplate>
                            <asp:Label ID="XuTimeReadyHeader" runat="server" Text="XuTimeReadyHeader" meta:resourcekey="XuTimeReadyHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuTimeReadyItem" runat="server" Text='<%# Bind("Note") %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuTimeReady" />
                        <ItemStyle CssClass="XuTimeReady" />

                    </asp:TemplateField>


                    <%--Column TimeClose --%>
                    <asp:TemplateField HeaderText="XuTimeClose">

                        <HeaderTemplate>
                            <asp:Label ID="XuTimeCloseHeader" runat="server" Text="XuTimeCloseHeader" meta:resourcekey="XuTimeCloseHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuTimeCloseItem" runat="server" Text='<%# Bind("PickupOperator") %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuTimeClose" />
                        <ItemStyle CssClass="XuTimeClose" />


                    </asp:TemplateField>


                    <%--Column Text --%>
                    <asp:TemplateField HeaderText="XuText">

                        <HeaderTemplate>
                            <asp:Label ID="XuTextHeader" runat="server" Text="XuTextHeader" meta:resourcekey="XuTextHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <%--
                                Status field contain 
                                * localized text (default to 
                                * Action
                            --%>
                            <asp:Label
                                ID="XuTextItem"
                                runat="server"
                                Text='<%# GetLocalResourceObject("XuStatus-Text_StatusCode-" + Eval("PickupStatusForwarder") + ".Text") %>'
                                ToolTip='<%# Bind("PickupStatusForwarder") %>' />

                            <asp:LinkButton
                                ID="LinkButton12"
                                runat="server"
                                CausesValidation="false"
                                CommandName="XcCmd04"
                                CommandArgument='<%# Eval("Id") %>'>
                                <span 
                                    class ='<%# Eval("CssGlyphiconStatus") %>'
                                    title='<%# GetLocalResourceObject("XuStatus-Icon_StatusCode-" + Eval("PickupStatusForwarder") + ".ToolTip") %>' 
                                    />
                            </asp:LinkButton>

                        </ItemTemplate>

                        <HeaderStyle CssClass="XuText" />
                        <ItemStyle CssClass="XuText" />

                    </asp:TemplateField>



                </Columns>

            </asp:GridView>


        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
