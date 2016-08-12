<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Customer02.aspx.cs" Inherits="DemoPickup40.Pages.Pickup.Customer02" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Customer02.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <table>
        <tr>
            <th>Adresse</th>
            <th>Afhentning</th>
            <th>Note</th>
            <th>Status</th>
            <th>Aktion</th>
        </tr>
        <tr>
            <td title="Global Transport Xpress, Stavnsholt Gydevej 70, DK 3460 Birkerød">Stavnsholt Gydevej 70</td>
            <td>2016-08-12 fra: 12:30 til: 14:30</td>
            <td>Henvendelse port 1</td>
            <td>Afventer bestilling</td>
            <td>
                <input id="Button1" type="button" value="Aktion" title="Bestil, Ændre, Flyt" /></td>
        </tr>
        <tr>
            <td colspan="5" class="nodisplay">
                <table class="subtable">
                    <tr>
                        <th class="col1">Vælg</th>
                        <th class="col2">Fragtbrevsnummer</th>
                        <th class="col3">Wægt(kg)</th>
                        <th class="col4">Dimensioner(cm)</th>
                    </tr>
                    <tr>
                        <td>
                            <input id="Checkbox1" type="checkbox" /></td>
                        <td>9200123456001</td>
                        <td>0.5</td>
                        <td>50 x 40 x 30</td>

                    </tr>
                    <tr>
                        <td>
                            <input id="Checkbox2" type="checkbox" /></td>
                        <td>9200123456001</td>
                        <td>0.5</td>
                        <td>50 x 40 x 30</td>
                    </tr>
                    <tr>
                        <td>
                            <input id="Checkbox3" type="checkbox" /></td>
                        <td>9200123456001</td>
                        <td>0.5</td>
                        <td>50 x 40 x 30</td>
                    </tr>

                </table>
            </td>
        </tr>
        <%--Dynamically append table rows--%>
        <asp:Literal ID="XuDynamicTableRows" runat="server"></asp:Literal>
    </table>


</asp:Content>
