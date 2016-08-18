<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="DemoPickup40.Pages.Pickup.Customer" %>



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

    <asp:UpdatePanel ID="XuUpdatePanel1" runat="server">

        <ContentTemplate>


            <%--First level GridView--%>
            <asp:GridView
                ID="Xu001"
                runat="server"
                AutoGenerateColumns="False"
                ShowHeaderWhenEmpty="True"
                CssClass="FirstGrid"
                OnRowCommand="Xu001_RowCommand">

                <HeaderStyle CssClass="HeaderStyleLevel1" />
                <RowStyle CssClass="RowStyleLevel1" />


                <AlternatingRowStyle CssClass="RowStyleLevel1Alt" />

                <Columns>

                    <%--Column #0--%>
                    <asp:TemplateField HeaderText="Expand">

                        <HeaderTemplate>
                            <asp:Label ID="Xu002" runat="server" Text="&nbsp;" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <%--Switch between glyphicon-triangle-bottom/top--%>
                            <i class="glyphicon glyphicon-triangle-top big"></i>
                        </ItemTemplate>

                    </asp:TemplateField>


                    <%--Column #1--%>
                    <asp:TemplateField HeaderText="Address">

                        <HeaderTemplate>
                            <asp:Label ID="Xu0011" runat="server" Text="Address" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="Xu0012" runat="server" ToolTip='<%# Bind("FullAddress") %>' Text='<%# Bind("Address") %>' />
                        </ItemTemplate>

                    </asp:TemplateField>


                    <%--Column #2--%>
                    <asp:TemplateField HeaderText="Pickup">

                        <HeaderTemplate>
                            <asp:Label ID="Xu0021" runat="server" Text="Pickup" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="Xu0022" runat="server" Text='<%# Bind("Pickup") %>' />
                        </ItemTemplate>

                    </asp:TemplateField>


                    <%--Column #3--%>
                    <asp:TemplateField HeaderText="Note">

                        <HeaderTemplate>
                            <asp:Label ID="Xu0031" runat="server" Text="Note" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="Xu0032" runat="server" Text='<%# Bind("Note") %>' />
                        </ItemTemplate>

                    </asp:TemplateField>


                    <%--Column #4 'Status'--%>
                    <asp:TemplateField HeaderText="Status">

                        <HeaderTemplate>
                            <asp:Label ID="Xu0041" runat="server" Text="Status" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <%--
                                Status field contain 
                                * localized text (default to 
                                * Action
                            --%>
                            <asp:Label
                                ID="Label2"
                                runat="server"
                                Text='<%# GetLocalResourceObject("XuStatus-Text_StatusCode-" + Eval("StatusCodeText") + ".Text") %>' />

                            <%--Text='<%# GetLocalResourceObject("XuStatus-Text_StatusCode-" + Eval("StatusCodeText") + ".Text") %>'--%>
                            <%--ToolTip='<%# GetLocalResourceObject("XuStatus-Text_StatusCode-" + Eval("StatusCodeText") + ".ToolTip") %>'--%>

                            <asp:LinkButton
                                ID="LinkButton12"
                                runat="server"
                                CausesValidation="false"
                                CommandName="XcCmd01"
                                CommandArgument='<%# Eval("StatusCodeText") + "." + Eval("PickupId") %>'>
                                <span 
                                    class='<%# "glyphicon " + GetLocalResourceObject("XuStatus-Icon_StatusCode-" + Eval("StatusCodeText") + ".Glyphicon") %>' 
                                    title='<%# GetLocalResourceObject("XuStatus-Icon_StatusCode-" + Eval("StatusCodeText") + ".ToolTip") %>' 
                                    />
                            </asp:LinkButton>

                        </ItemTemplate>

                    </asp:TemplateField>


                    <%--Column #5 Action  --%>
                    <asp:TemplateField HeaderText="Move">

                        <HeaderTemplate>
                            <asp:Label ID="Xu0051" runat="server" Text="Action" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton13" CommandArgument='<%# Eval("PickupId") %>' runat="server" CausesValidation="false" CommandName="XcCmd02" ToolTip='<%# GetLocalResourceObject("XuImpShipments.Text") %>   '>
                                <span 
                                    class='<%# "glyphicon " + GetLocalResourceObject("XuMove-Icon_StatusCode-" + Eval("StatusCodeText") + ".Glyphicon") %>' 
                                    title='<%# GetLocalResourceObject("XuMove-Icon_StatusCode-" + Eval("StatusCodeText") + ".ToolTip") %>' 
                                    />
                            </asp:LinkButton>
                        </ItemTemplate>


                    </asp:TemplateField>



                    <%--Column #6--%>
                    <%--                    <asp:TemplateField>

                        <HeaderTemplate>
                            <asp:Label ID="Xu002" runat="server" Text="S" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <i title="Bestil afhentning" class="glyphicon glyphicon-send big"></i>
                        </ItemTemplate>

                    </asp:TemplateField>--%>


                    <%--Column #N--%>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="Xu0091" runat="server" Text="&nbsp;" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--NOTE! this Column is mapped to a new row.--%>
                            <tr>
                                <td colspan="100%">
                                    <asp:GridView ID="Xu100" runat="server" AutoGenerateColumns="False" DataSource='<%# Bind("Shipmentlist") %>'>
                                        <HeaderStyle CssClass="HeaderStyleLevel2" />
                                        <RowStyle CssClass="RowStyleLevel2" />
                                        <Columns>
                                            <%--Column #0--%>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="Xu00A1" runat="server" Text="&nbsp;" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <input type="checkbox" runat="server" id="XuSelected" disabled='<%# "ForwSched".Equals( Eval("StatusCodeText")) %>' value='<%# Eval("ShipmentId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--Column #1--%>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="Xu102" runat="server" Text="Waybillnumber" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Xu104" runat="server" Text='<%# Bind("Waybillnumber") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--Column #2--%>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="Xu1022" runat="server" Text="Weight" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Xu1023" runat="server" Text='<%# Bind("Weight") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--Column #2--%>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="Xu1031" runat="server" Text="Dimensions" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Xu1032" runat="server" Text='<%# Bind("Dimensions") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <HeaderStyle CssClass="hidden" />
                        <ItemStyle CssClass="hidden" />
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>




        </ContentTemplate>

    </asp:UpdatePanel>



</asp:Content>
