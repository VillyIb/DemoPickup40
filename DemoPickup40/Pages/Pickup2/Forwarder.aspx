<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Forwarder.aspx.cs" Inherits="DemoPickup40.Pages.Pickup2.Forwarder" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../assets/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/css/Site.css" rel="stylesheet" />
    <link href="../../assets/css/stylesheet-2016-02-23.css" rel="stylesheet" />
    <link href="Forwarder.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="XuUpdatePanel1" runat="server">

        <ContentTemplate>

            <div class="css-table">
                <div class="css-tr">
                    <div class="css-td ColWidth01">
                        <asp:LinkButton
                            ID="XuContainerCol1"
                            runat="server"
                            CausesValidation="false"
                            CommandName="XcCmd11"
                            OnClick="XuContainerCol1_Click">
                            <div id="XuContainerCol1Icon" runat="server"
                                class="glyphicon glyphicon-triangle-bottom big"
                                title='<%# GetLocalResourceObject("XuContainerCol1Icon.Text") %>' />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>

            <%--Forwarder Pickup Grid View--%>
            <asp:GridView
                ID="XuForwarderPickup"
                runat="server"
                AutoGenerateColumns="false"
                ShowHeaderWhenEmpty="true"
                CssClass="XuForwarderPickup"
                OnRowCommand="XuGridForwarderPicup_RowCommand">

                <HeaderStyle CssClass="XuForwarderPickupHeader" />
                <RowStyle CssClass="XuForwarderPickupRow" />

                <Columns>

                    <%--Column Expand Customer Pickups below Forwarder Pickup --%>
                    <asp:TemplateField HeaderText="XuFwExpandCustomer">
                        <HeaderTemplate>
                            &nbsp;
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:LinkButton
                                ID="XuFwExpandCustomerItem"
                                runat="server"
                                CausesValidation="false"
                                CommandName="XcCmd09"
                                CommandArgument='<%# Eval("Id") %>'>
                                <span
                                    class ='<%# Eval("CssGlyphiconExpandCustomer") %>'
                                    title='<%# GetLocalResourceObject("XuFwExpandCustomerItem.Text") %>' 
                                    />
                            </asp:LinkButton>
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuFwExpandCustomer" />
                        <ItemStyle CssClass="XuFwExpandCustomer" />

                    </asp:TemplateField>


                    <%--Column Expand all Shipments below Customer Pickups below Forwarder Pickup --%>
                    <asp:TemplateField HeaderText="XuFwExpandShipment">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwExpandShipmentHeader" runat="server" Text="&nbsp;" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:LinkButton
                                ID="XuFwExpandShipmentItem"
                                runat="server"
                                CausesValidation="false"
                                CommandName="XcCmd08"
                                CommandArgument='<%# Eval("Id") %>'>
                                <span 
                                    class ='<%# Eval("CssGlyphiconExpandGroup") %>'
                                    title='<%# GetLocalResourceObject("XuFwExpandShipmentItem.Text") %>' 
                                    />
                            </asp:LinkButton>

                        </ItemTemplate>

                        <HeaderStyle CssClass="XuFwExpand" />
                        <ItemStyle CssClass="XuFwExpand" />

                    </asp:TemplateField>


                    <%--Column Address --%>
                    <asp:TemplateField HeaderText="XuFwAddress">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwAddressHeader" runat="server" Text="XuFwAddressHeader" meta:resourcekey="XuFwAddressHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuFwAddressItem" runat="server" ToolTip='<%# Bind("Address.FullAddress") %>' Text='<%# Bind("Address.Street1") %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuFwAddress" />
                        <ItemStyle CssClass="XuFwAddress" />

                    </asp:TemplateField>

                    <%--Column Pickup Window --%>
                    <asp:TemplateField HeaderText="XuFwPickup">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwPickupHeader" runat="server" Text="XuFwPickupHeader" meta:resourcekey="XuFwPickupHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <%--<asp:Label ID="XuFwPickupItem" runat="server"  Text='<%# Bind("PickupDate") %>'></asp:Label>--%>
                            <asp:Label ID="Label1" runat="server" Text='<%# GetPickupText( (string)GetLocalResourceObject("XuFwPickupItem.Format"), Eval("PickupDate"), Eval("ReadyOpen"), Eval("ReadyClose")) %>'></asp:Label>
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


                    <%--Column Carrier --%>
                    <asp:TemplateField HeaderText="XuFwCarrier">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwCarrierHeader" runat="server" Text="XuFwCarrierHeader" meta:resourcekey="XuFwCarrierHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuFwCarrierItem" runat="server" Text='<%# Bind("CarrierNameList") %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuFwCarrier" />
                        <ItemStyle CssClass="XuFwCarrier" />

                    </asp:TemplateField>


                    <%--Column Counters--%>
                    <asp:TemplateField HeaderText="XuFwCounter">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwCounterHeader" runat="server" Text="XuFwCounterHeader" meta:resourcekey="XuFwCounterHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuFwCounterItem" runat="server" Text='<%# string.Format("{0} ({1})", Eval("CountCustomers") , Eval("CountShipments") ) %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuFwCounter" />
                        <ItemStyle CssClass="XuFwCounter" />

                    </asp:TemplateField>


                    <%--Column Note --%>
                    <asp:TemplateField HeaderText="XuFwNote">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwNoteHeader" runat="server" Text="XuFwNoteHeader" meta:resourcekey="XuFwNoteHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="XuFwNoteItem" runat="server" Text='<%# Bind("Note") %>' />
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuFwNote" />
                        <ItemStyle CssClass="XuFwNote" />

                    </asp:TemplateField>



                    <%--Column Status --%>
                    <asp:TemplateField HeaderText="XuFwStatus">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwStatusHeader" runat="server" Text="XuFwStatusHeader" meta:resourcekey="XuFwStatusHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <%--
                                Status field contain 
                                * localized text (default to 
                                * Action
                            --%>
                            <asp:Label
                                ID="XuFwStatusItem"
                                runat="server"
                                Text='<%# GetLocalResourceObject("XuStatus-Text_StatusCode-" + Eval("PickupStatusForwarder") + ".Text") %>' />

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

                        <HeaderStyle CssClass="XuFwStatus" />
                        <ItemStyle CssClass="XuFwStatus" />

                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="XuFwEdit">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwEditHeader" runat="server" Text="XuFwEditHeader" meta:resourceKey="XuFwEditHeader" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:LinkButton
                                ID="XuFwEditItemEvent"
                                runat="server"
                                target="_blank"
                                CausesValidation="false"
                                CommandName="XcCmd05"
                                CommandArgument='<%# Eval("Id") %>'>
                                <span 
                                    class="glyphicon glyphicon-edit" 
                                    title='<%# GetLocalResourceObject("XuFwEditEventItem.Text") %>' 
                                    />
                            </asp:LinkButton>
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuFwEdit" />
                        <ItemStyle CssClass="XuFwEdit" />

                    </asp:TemplateField>


                    <%--Move Customer Pickup--%>
                    <asp:TemplateField HeaderText="XuFwMove">

                        <HeaderTemplate>
                            <asp:Label ID="XuFwMoveHeader" runat="server" Text='<%# GetLocalResourceObject("XuFwMoveHeader.Text") %>' />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:LinkButton ID="XuFwMoveItem" CommandArgument='<%# Eval("Id") %>' runat="server" CausesValidation="false" CommandName="XcCmd06">
                                <span 
                                    class='<%# Eval("CssGlyphiconMove") %>'
                                    title='<%# GetLocalResourceObject("XuFwMove-Icon.Title") %>' 
                                />
                            </asp:LinkButton>
                        </ItemTemplate>

                        <HeaderStyle CssClass="XuFwMove" />
                        <ItemStyle CssClass="XuFwMove" />

                    </asp:TemplateField>


                    <%--Column Customer  Details--%>
                    <asp:TemplateField HeaderText="XuCustomerDetails">
                        <HeaderTemplate>
                            <asp:Label runat="server" Text="&nbsp;" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--NOTE! this Column is mapped to a new row.--%>
                            <tr>
                                <td colspan="10">

                                    <%--Customer Pickup  GridView--%>
                                    <asp:GridView
                                        ID="XuCustomerPickup"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        ShowHeaderWhenEmpty="True"
                                        CssClass='<%# "XuCustomerPickup" + Eval("CssVisibleCustomer") %>'
                                        DataSource='<%# Bind("CustomerPickupList") %>'
                                        OnRowCommand="XuGridCustomerPickup_RowCommand">

                                        <HeaderStyle CssClass="XuCustomerPickupHeader" />
                                        <RowStyle CssClass="XuCustomerPickupRow" />

                                        <Columns>

                                            <%--Column filler column to allign with parent table--%>
                                            <asp:TemplateField HeaderText="XuCuFiller">
                                                <HeaderTemplate>
                                                    &nbsp;
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    &nbsp;&nbsp;&nbsp;
                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuCuFiller" />
                                                <ItemStyle CssClass="XuCuFiller" />

                                            </asp:TemplateField>


                                            <%--Column Expand --%>
                                            <asp:TemplateField HeaderText="XuCuExpand">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuCuExpandHeader" runat="server" Text="&nbsp;" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:LinkButton ID="XuCuExpandItem" CommandArgument='<%# Eval("Id") %>' runat="server" CausesValidation="false" CommandName="XcCmd07">
                                                        <span 
                                                            class='<%# Eval("CssGlyphiconExpand") %>'
                                                            title='<%# GetLocalResourceObject("XuCuExpand-Icon.Title") %>' 
                                                        />
                                                    </asp:LinkButton>

                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuCuExpand" />
                                                <ItemStyle CssClass="XuCuExpand" />

                                            </asp:TemplateField>


                                            <%--Column Address --%>
                                            <asp:TemplateField HeaderText="XuCuAddress">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuCuAddressHeader" runat="server" Text="XuCuAddressHeader" meta:resourcekey="XuCuAddressHeader" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="XuCuAddressItem" runat="server" ToolTip='<%# Bind("Address.FullAddress") %>' Text='<%# Bind("Address.Street1") %>' />
                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuCuAddress" />
                                                <ItemStyle CssClass="XuCuAddress" />

                                            </asp:TemplateField>


                                            <%--Column Pickup Window --%>
                                            <asp:TemplateField HeaderText="XuCuPickup">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuCuPickupHeader" runat="server" Text="XuCuPickupHeader" meta:resourcekey="XuCuPickupHeader" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <%--<asp:Label ID="XuCuPickupItem" runat="server"  Text='<%# Bind("PickupDate") %>'></asp:Label>--%>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# GetPickupText( (string)GetLocalResourceObject("XuCuPickupItem.Format"), Eval("PickupDate"), Eval("ReadyOpen"), Eval("ReadyClose")) %>'></asp:Label>

                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuCuPickup ColPickup" />
                                                <ItemStyle CssClass="XuCuPickup ColPickup" />

                                            </asp:TemplateField>


                                            <%--Column Carrier --%>
                                            <asp:TemplateField HeaderText="XuCuCarrier">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuCuCarrierHeader" runat="server" Text="XuCuCarrierHeader" meta:resourcekey="XuCuCarrierHeader" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="XuCuCarrierItem" runat="server" Text='<%# Bind("CarrierNameList") %>' />
                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuCuCarrier ColCarrier" />
                                                <ItemStyle CssClass="XuCuCarrier ColCarrier" />

                                            </asp:TemplateField>


                                            <%--Column Counters--%>
                                            <asp:TemplateField HeaderText="XuCuCounter">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuCuCounterHeader" runat="server" Text="XuCuCounterHeader" meta:resourcekey="XuCuCounterHeader" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="XuCuCounterItem" runat="server" Text='<%# Bind("CountShipments")  %>' />
                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuCuCounter ColCounter" />
                                                <ItemStyle CssClass="XuCuCounter ColCounter" />

                                            </asp:TemplateField>


                                            <%--Column Note --%>
                                            <asp:TemplateField HeaderText="XuCuNote">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuCuNoteHeader" runat="server" Text="XuCuNoteHeader" meta:resourcekey="XuCuNoteHeader" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="XuCuNoteItem" runat="server" Text='<%# Bind("Note") %>' />
                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuCuNote" />
                                                <ItemStyle CssClass="XuCuNote" />

                                            </asp:TemplateField>


                                            <%--Column Status --%>
                                            <asp:TemplateField HeaderText="XuCuStatus">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuCuStatusHeader" runat="server" Text="XuCuStatusHeader" meta:resourcekey="XuCuStatusHeader" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <%--
                                                        Status field contain 
                                                        * localized text (default to 
                                                        * Action
                                                    --%>
                                                    <asp:Label
                                                        ID="XuCuStatusItem"
                                                        runat="server"
                                                        Text='<%# GetLocalResourceObject("XuStatus-Text_StatusCode-" + Eval("PickupStatusForwarder") + ".Text") %>' />

                                                    <asp:LinkButton
                                                        ID="LinkButton12"
                                                        runat="server"
                                                        CausesValidation="false"
                                                        CommandName="XcCmd01"
                                                        CommandArgument='<%# Eval("PickupStatusForwarder") + "." + Eval("Id") %>'>
                                                        <span 
                                                            class='<%# "glyphicon " + GetLocalResourceObject("XuStatus-Icon_StatusCode-" + Eval("PickupStatusForwarder") + ".Glyphicon") %>' 
                                                            title='<%# GetLocalResourceObject("XuStatus-Icon_StatusCode-" + Eval("PickupStatusForwarder") + ".ToolTip") %>' 
                                                            />
                                                    </asp:LinkButton>

                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuCuStatus" />
                                                <ItemStyle CssClass="XuCuStatus" />

                                            </asp:TemplateField>


                                            <%--Column Move --%>
                                            <asp:TemplateField HeaderText="XuCuMove">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuCuMoveHeader" runat="server" meta:resourcekey="XuCuMoveHeader" Text="XuCuMoveHeader" />
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:LinkButton ID="XuCuMoveItem" CommandArgument='<%# Eval("Id") %>' runat="server" CausesValidation="false" CommandName="XcCmd02">
                                                    <span 
                                                        class='<%# Eval("CssGlyphiconMove") %>' 
                                                        title='<%# GetLocalResourceObject("XuCuMove-Icon.Title") %>' 
                                                        />
                                                    </asp:LinkButton>

                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuCuMove" />
                                                <ItemStyle CssClass="XuCuMove" />

                                            </asp:TemplateField>


                                            <%--Column Customer Pickup Select--%>
                                            <asp:TemplateField HeaderText="XuCuSelect">

                                                <HeaderTemplate>
                                                    <asp:Label ID="XuCuSelectHeader" runat="server" Text="XuCuSelectHeader" meta:resourcekey="XuCuSelectHeader" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <input id="XuCuSelectItem" type="checkbox" runat="server" disabled='<%# Eval("CssVisibleCheckbox") %>' value='<%# Eval("Id") %>' />
                                                </ItemTemplate>

                                                <HeaderStyle CssClass="XuCuSelect" />
                                                <ItemStyle CssClass="XuCuSelect" />

                                            </asp:TemplateField>


                                            <%--Column Shipment Details--%>
                                            <asp:TemplateField HeaderText="XuShipmentDetails">
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="&nbsp;" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%--NOTE! this Column is mapped to a new row.--%>
                                                    <tr>
                                                        <td colspan="10">

                                                            <asp:GridView
                                                                ID="XuShipmentDetails"
                                                                runat="server"
                                                                AutoGenerateColumns="False"
                                                                DataSource='<%# Bind("Shipmentlist") %>'
                                                                CssClass='<%# "XuShipmentDetails" + Eval("CssVisibleShipments") %>'>

                                                                <HeaderStyle CssClass="XuShipmentDetailsHead" />
                                                                <RowStyle CssClass="XuShipmentDetailsRow" />

                                                                <Columns>

                                                                    <%--filler column to allign with parent table--%>
                                                                    <asp:TemplateField HeaderText="XuCuFillerA">
                                                                        <HeaderTemplate>
                                                                            &nbsp;
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            &nbsp;
                                                                        </ItemTemplate>

                                                                        <HeaderStyle CssClass="XuCuFillerA" />
                                                                        <ItemStyle CssClass="XuCuFillerA" />

                                                                    </asp:TemplateField>


                                                                    <%--filler column to allign with parent table--%>
                                                                    <asp:TemplateField HeaderText="XuCuFillerB">
                                                                        <HeaderTemplate>
                                                                            &nbsp;
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            &nbsp;
                                                                        </ItemTemplate>

                                                                        <HeaderStyle CssClass="XuCuFillerB" />
                                                                        <ItemStyle CssClass="XuCuFillerB" />

                                                                    </asp:TemplateField>


                                                                    <%--Column Waybill number --%>
                                                                    <asp:TemplateField HeaderText="XuShWaybillnumber">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="XuShWaybillnumberHead" runat="server" Text="XuShWaybillnumberHead" meta:resourcekey="XuShWaybillnumberHead" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="XuShWaybillnumberItem" runat="server" Text='<%# Bind("Waybillnumber") %>' ToolTip='<%# Bind("Address.FullAddress") %>' />
                                                                        </ItemTemplate>

                                                                        <HeaderStyle CssClass="XuShWaybillnumber" />
                                                                        <ItemStyle CssClass="XuShWaybillnumber" />

                                                                    </asp:TemplateField>


                                                                    <%--Column Carrier --%>
                                                                    <asp:TemplateField HeaderText="XuShCarrier">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="XuShCarrierHeader" runat="server" Text="XuShCarrierHeader" meta:resourcekey="XuShCarrierHeader" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="XuShCarrierItem" runat="server" Text='<%# Bind("CarrierName") %>' />
                                                                        </ItemTemplate>

                                                                        <HeaderStyle CssClass="XuShCarrier" />
                                                                        <ItemStyle CssClass="XuShCarrier" />

                                                                    </asp:TemplateField>


                                                                    <%--Column Weight --%>
                                                                    <asp:TemplateField HeaderText="XuShWeight">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="XuShWeightHeader" runat="server" Text="XuShWeightHeader" meta:resourcekey="XuShWeightHeader" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="XuShWeightItem" runat="server" Text='<%# Bind("Weight") %>' />
                                                                        </ItemTemplate>

                                                                        <HeaderStyle CssClass="XuShWeight" />
                                                                        <ItemStyle CssClass="XuShWeight" />

                                                                    </asp:TemplateField>


                                                                    <%--Column Dimension --%>
                                                                    <asp:TemplateField HeaderText="XuShDimension">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="XuShDimensionHeader" runat="server" Text="XuShDimensionHeader" meta:resourcekey="XuShDimensionHeader" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="XuShDimensionItem" runat="server" Text='<%# Bind("Dimensions") %>' />
                                                                        </ItemTemplate>

                                                                        <HeaderStyle CssClass="XuShDimension" />
                                                                        <ItemStyle CssClass="XuShDimension" />

                                                                    </asp:TemplateField>


                                                                    <%--Column Select --%>
                                                                    <asp:TemplateField HeaderText="XuShSelect">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="XuShSelectHeader" runat="server" Text="XuShSelectHeader" meta:resourcekey="XuShSelectHeader" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <input id="XuShSelectItem" type="checkbox" runat="server" disabled='<%# Eval("CssVisibleCheckbox") %>' value='<%# Eval("Id") %>' />
                                                                        </ItemTemplate>

                                                                        <HeaderStyle CssClass="XuShSelect" />
                                                                        <ItemStyle CssClass="XuShSelect" />

                                                                    </asp:TemplateField>

                                                                    <%--Column Shipment filler column to allign with parent table --%>
                                                                    <asp:TemplateField HeaderText="XuShFiller">
                                                                        <HeaderTemplate>
                                                                            &nbsp;&nbsp;&nbsp;
                                                                        </HeaderTemplate>
                                                                        <InsertItemTemplate>
                                                                            &nbsp;&nbsp;&nbsp;
                                                                        </InsertItemTemplate>

                                                                        <HeaderStyle CssClass="XuShFiller" />
                                                                        <ItemStyle CssClass="XuShFiller" />

                                                                    </asp:TemplateField>

                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>

                                                <%--Do not show this as the last Column of Customer Pickup row--%>
                                                <HeaderStyle CssClass="hidden" />
                                                <ItemStyle CssClass="hidden" />

                                            </asp:TemplateField>

                                        </Columns>

                                    </asp:GridView>

                                    <%--Customer Pickup  -- END --  GridView--%>
                                </td>
                            </tr>
                        </ItemTemplate>

                        <%--Do not show this as the last Column of Forwarder Pickup row --%>
                        <HeaderStyle CssClass="hidden" />
                        <ItemStyle CssClass="hidden" />

                    </asp:TemplateField>

                </Columns>

            </asp:GridView>


        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
